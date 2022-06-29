using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Http;

namespace ProEShop.Common.Helpers;

public static class FileHelpers
{
    public static bool IsFileUploaded(this IFormFile file)
    {
        return file is { Length: > 0 };
    }

    public static string GenerateBarcode(string barcode, string productTitle, bool isVariantColor, string variantValue)
    {
        #region MyRegion

        var rectangleBitmap = new Bitmap(300, 131);
        using var rectangleGraphic = Graphics.FromImage(rectangleBitmap);
        {
            var rectangle = new Rectangle(0, 0, 300, 131);
            rectangleGraphic.FillRectangle(Brushes.OrangeRed, rectangle);
        }

        using var rectangleStream = new MemoryStream();
        rectangleBitmap.Save(rectangleStream, ImageFormat.Png);
        return Convert.ToBase64String(rectangleStream.ToArray());

        #endregion
    }
}
