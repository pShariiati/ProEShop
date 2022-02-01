using System.Globalization;

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
}