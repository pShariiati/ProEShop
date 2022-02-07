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

    public static string ToShortPersianDate(this DateTime dateTime)
    {
        var pc = new PersianCalendar();
        var day = pc.GetDayOfMonth(dateTime).ToString("00");
        var month = pc.GetMonth(dateTime).ToString("00/");
        var year = pc.GetYear(dateTime).ToString("0000/");
        return $"{year}{month}{day}";
    }

    public static ConvertDateForCreateSeller ToGregorianDateForCreateSeller(this string input)
    {
        input = input.ToEnglishNumbers();

        var splitInput = input.Split('/');

        var year = int.Parse(splitInput[0]);
        var month = int.Parse(splitInput[1]);
        var day = int.Parse(splitInput[2]);

        try
        {
            var convertedDateTime = new DateTime(year, month, day, new PersianCalendar());
            var age = convertedDateTime.GetAge();
            if (age is < 18 or > 100)
            {
                return new(true, false);
            }

            return new(true, true, convertedDateTime);

        }
        catch
        {
            return new(false);
        }
    }
}