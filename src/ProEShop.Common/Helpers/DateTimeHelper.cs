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
}