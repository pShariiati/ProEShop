using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using BarcodeLib;
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
        #region MainBitmap

        var mainBitmap = new Bitmap(300, 131);
        using var rectangleGraphics = Graphics.FromImage(mainBitmap);
        {
            var rectangle = new Rectangle(0, 0, 300, 131);
            rectangleGraphics.FillRectangle(Brushes.OrangeRed, rectangle);
        }

        using var rectangleStream = new MemoryStream();
        {
            mainBitmap.Save(rectangleStream, ImageFormat.Png);
        }
        //return Convert.ToBase64String(rectangleStream.ToArray());

        #endregion

        #region Barcode

        // BarcodeLib package
        var barcodeInstance = new Barcode();
        var barcodeImage = barcodeInstance.Encode(BarcodeLib.TYPE.CODE39, barcode, Color.Black,
            Color.OrangeRed, 300, 50);
        using var barcodeStream = new MemoryStream();
        {
            barcodeImage.Save(barcodeStream, ImageFormat.Png);
        }

        //return Convert.ToBase64String(barcodeStream.ToArray());

        #endregion

        #region MergedRectangleAndBarcode

        var newMainBitmap = (Bitmap)Image.FromStream(rectangleStream);
        var newBarcodeBitmap = (Bitmap)Image.FromStream(barcodeStream);
        using var newRectangleGraphics = Graphics.FromImage(newMainBitmap);
        {
            newRectangleGraphics.DrawImage(newBarcodeBitmap, 0, 0);
        }

        using var mergedRectangleAndBarcodeStream = new MemoryStream();
        {
            newMainBitmap.Save(mergedRectangleAndBarcodeStream, ImageFormat.Png);
        }

        //return Convert.ToBase64String(mergedRectangleAndBarcodeStream.ToArray());

        #endregion

        #region WriteProductTitle

        var barcodeBitmap = (Bitmap)Image.FromStream(mergedRectangleAndBarcodeStream);
        using var graphics = Graphics.FromImage(barcodeBitmap);
        {
            using var font = new Font("Tahoma", 10);
            {
                var rect = new Rectangle(0, 55, 300, 52);
                var sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.Trimming = StringTrimming.EllipsisCharacter;
                sf.FormatFlags = StringFormatFlags.DirectionRightToLeft;
                graphics.DrawString(productTitle, font, Brushes.Black, rect, sf);
                //graphics.DrawRectangle(Pens.Green, rect);
            }
        }

        using var productTitleStream = new MemoryStream();
        {
            barcodeBitmap.Save(productTitleStream, ImageFormat.Png);
        }
        //return Convert.ToBase64String(productTitleStream.ToArray());

        #endregion

        #region WriteVariant

        var variantText = isVariantColor ? "رنگ" : "اندازه";
        variantText += $": {variantValue}";

        var barcodeWithProductTitleBitmap = (Bitmap)Image.FromStream(productTitleStream);
        using var graphics2 = Graphics.FromImage(barcodeWithProductTitleBitmap);
        {
            using var font = new Font("Tahoma", 10);
            {
                var rect = new Rectangle(0, 112, 300, 19);
                var sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.Trimming = StringTrimming.EllipsisCharacter;
                sf.FormatFlags = StringFormatFlags.DirectionRightToLeft;
                graphics2.DrawString(variantText, font, Brushes.Black, rect, sf);
                //graphics2.DrawRectangle(Pens.Green, rect);
            }
        }

        using var finalStream = new MemoryStream();
        {
            barcodeWithProductTitleBitmap.Save(finalStream, ImageFormat.Png);
        }

        return Convert.ToBase64String(finalStream.ToArray());

        #endregion
    }
}
