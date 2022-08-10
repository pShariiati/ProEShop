using System.Globalization;
using DNTPersianUtils.Core;

namespace ProEShop.Common.Helpers;

public static class DateTimeHelper
{
    public static (byte, byte) GetMinuteAndSecondForLoginWithPhoneNumberPage(this DateTime input)
    {
        var now = DateTime.Now;
        var inputPlusThreeMinutes = input.AddMinutes(3);
        if (now >= inputPlusThreeMinutes)
            return (0, 0);
        var result = inputPlusThreeMinutes - now;
        var min = result.Minutes;
        var sec = result.Seconds;
        return ((byte)min, (byte)sec);
    }

    public static string ToLongPersianDate(this DateTime dateTime)
    {
        var monthsNames = new[]
        {
            "فروردین",
            "اردیبهشت",
            "خرداد",
            "تیر",
            "مرداد",
            "شهریور",
            "مهر",
            "آبان",
            "آذر",
            "دی",
            "بهمن",
            "اسفند",
        };
        var pc = new PersianCalendar();
        var day = pc.GetDayOfMonth(dateTime).ToString("00");
        var month = monthsNames[pc.GetMonth(dateTime) - 1];
        var year = pc.GetYear(dateTime).ToString("0000");
        return $"{day} {month} {year}".ToPersianNumbers();
    }

    public static string ToShortPersianDate(this DateTime dateTime)
    {
        var pc = new PersianCalendar();
        var day = pc.GetDayOfMonth(dateTime).ToString("00");
        var month = pc.GetMonth(dateTime).ToString("00/");
        var year = pc.GetYear(dateTime).ToString("0000/");
        return $"{year}{month}{day}";
    }

    public static string ToShortPersianDateTime(this DateTime dateTime)
    {
        var pc = new PersianCalendar();
        var day = pc.GetDayOfMonth(dateTime).ToString("00");
        var month = pc.GetMonth(dateTime).ToString("00/");
        var year = pc.GetYear(dateTime).ToString("0000/");
        var hour = pc.GetHour(dateTime).ToString("00:");
        var minute = pc.GetMinute(dateTime).ToString("00");
        return $"{year}{month}{day} {hour}{minute}";
    }

    public static ConvertDateForCreateSeller ToGregorianDateForCreateSeller(this string input)
    {
        var convertedDateTime = ToGregorianDate(input);
        if (!convertedDateTime.IsSuccessful)
            return new(false);
        var age = convertedDateTime.Result.GetAge();
        if (age is < 18 or > 100)
        {
            return new(true, false);
        }

        return new(true, true, convertedDateTime.Result);
    }

    public static (bool IsSuccessful, bool IsStartDateTimeGreatherOrEqualEndDateTime, bool IsTimeSpanLowerThan3Hour, DateTime StartDate, DateTime EndDate)
        ConvertDateTimeForAddEditDiscount(string startDatTime, string endDateTime)
    {
        var convertedStartDateTime = ToGregorianDateTime(startDatTime);
        var convertedEndDateTime = ToGregorianDateTime(endDateTime);

        if (!convertedStartDateTime.IsSuccessful || !convertedEndDateTime.IsSuccessful)
        {
            return (false, default, default, default, default);
        }

        if (convertedStartDateTime.Result >= convertedEndDateTime.Result)
        {
            return (true, true, default, default, default);
        }

        var subtract = convertedEndDateTime.Result - convertedStartDateTime.Result;
        if (subtract.TotalHours < 3)
        {
            return (true, false, true, default, default);
        }

        return (true, false, false, convertedStartDateTime.Result, convertedEndDateTime.Result);
    }

    public static (bool IsSuccessful, DateTime Result) ToGregorianDate(this string input)
    {
        try
        {
            input = input.ToEnglishNumbers();

            var splitInput = input.Split('/');

            var year = int.Parse(splitInput[0]);
            var month = int.Parse(splitInput[1]);
            var day = int.Parse(splitInput[2]);

            return (true, new DateTime(year, month, day, new PersianCalendar()));

        }
        catch
        {
            return (false, new DateTime());
        }
    }
    public static (bool IsSuccessful, DateTime Result) ToGregorianDateTime(this string input)
    {
        try
        {
            // 1399/09/09 21:22

            input = input.ToEnglishNumbers();

            var splitInput = input.Split(' ');
            var date = splitInput[0].Split('/');
            var time = splitInput[1].Split(':');

            var year = int.Parse(date[0]);
            var month = int.Parse(date[1]);
            var day = int.Parse(date[2]);

            var hour = int.Parse(time[0]);
            var minute = int.Parse(time[1]);

            return (true, new DateTime(year, month, day, hour, minute, 0, new PersianCalendar()));

        }
        catch
        {
            return (false, new DateTime());
        }
    }

}