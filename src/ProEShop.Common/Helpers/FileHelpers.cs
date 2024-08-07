﻿using System.Drawing;
using System.Drawing.Imaging;
using BarcodeStandard;
using Microsoft.AspNetCore.Http;
using SkiaSharp;

namespace ProEShop.Common.Helpers;

public static class FileHelpers
{
    public static bool IsFileUploaded(this IFormFile file)
    {
        return file is { Length: > 0 };
    }

    public static string GenerateConsignmentItemBarcode(string barcode, string productTitle, bool? isVariantColor, string variantValue)
    {
        // barcode 50
        // margin top 5
        // product title 52 or 34
        // margin top 5
        // variant 19
        // sum = 131 or 113

        var resultWidth = 250;
        var resultHeight = productTitle.Length > 70 ? 131 : 113;
        if (isVariantColor is null)
        {
            resultHeight -= 19;
        }
        var barcodeHeight = 50;
        var productTitleHeight = resultHeight == 131 ? 52 : 34;
        var productTitleY = barcodeHeight + 5;
        var variantY = productTitleY + productTitleHeight + 5;

        #region MainBitmap

        var mainBitmap = new Bitmap(resultWidth, resultHeight);
        using var rectangleGraphics = Graphics.FromImage(mainBitmap);
        {
            var rectangle = new Rectangle(0, 0, resultWidth, 131);
            rectangleGraphics.FillRectangle(Brushes.OrangeRed, rectangle);
        }

        using var rectangleStream = new MemoryStream();
        {
            mainBitmap.Save(rectangleStream, ImageFormat.Png);
        }

        #endregion

        #region Barcode

        var barcodeImage = GenerateBarcodeImage(barcode, resultWidth, barcodeHeight);

        #endregion

        #region MergedRectangleAndBarcode

        var newMainBitmap = (Bitmap)Image.FromStream(rectangleStream);
        var newBarcodeBitmap = barcodeImage;
        using var newRectangleGraphics = Graphics.FromImage(newMainBitmap);
        {
            newRectangleGraphics.DrawImage(newBarcodeBitmap, 0, 0);
        }

        using var mergedRectangleAndBarcodeStream = new MemoryStream();
        {
            newMainBitmap.Save(mergedRectangleAndBarcodeStream, ImageFormat.Png);
        }

        #endregion

        #region WriteProductTitle

        var barcodeBitmap = (Bitmap)Image.FromStream(mergedRectangleAndBarcodeStream);
        using var graphics = Graphics.FromImage(barcodeBitmap);
        {
            using var font = new Font("Tahoma", 10);
            {
                var rect = new Rectangle(0, productTitleY, resultWidth, productTitleHeight);
                var sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.Trimming = StringTrimming.EllipsisCharacter;
                sf.FormatFlags = StringFormatFlags.DirectionRightToLeft;
                sf.LineAlignment = StringAlignment.Center;
                if (isVariantColor is null)
                {
                    productTitle += $" ({barcode.Split("--")[0]})";
                }
                graphics.DrawString(productTitle, font, Brushes.Black, rect, sf);
                //graphics.DrawRectangle(Pens.Green, rect);
            }
        }

        using var productTitleStream = new MemoryStream();
        {
            barcodeBitmap.Save(productTitleStream, ImageFormat.Png);
        }

        #endregion

        #region WriteVariant

        if (isVariantColor != null)
        {
            var variantText = isVariantColor.Value ? "رنگ" : "اندازه";
            variantText += $": {variantValue}";
            // 1--4
            variantText += $" ({barcode.Split("--")[0]})";

            var barcodeWithProductTitleBitmap = (Bitmap)Image.FromStream(productTitleStream);
            using var graphics2 = Graphics.FromImage(barcodeWithProductTitleBitmap);
            {
                using var font = new Font("Tahoma", 10);
                {
                    var rect = new Rectangle(0, variantY, resultWidth, 19);
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
        }

        #endregion

        return Convert.ToBase64String(productTitleStream.ToArray());
    }

    public static string GenerateConsignmentBarcode(string barcode, string shopName, string deliveryDate)
    {
        var resultWidth = 250;
        // barcode = 50
        // margin top = 5
        // shop name = 19
        // margin top = 5
        // delivery date = 19
        // Sum = 50 + 5 + 19 + 5 + 19 = 98
        var resultHeight = 98;
        var barcodeHeight = 50;
        var shopNameY = barcodeHeight + 5;
        var deliveryDateY = shopNameY + 19 + 5;

        #region MainBitmap

        var mainBitmap = new Bitmap(resultWidth, resultHeight);
        using var rectangleGraphics = Graphics.FromImage(mainBitmap);
        {
            var rectangle = new Rectangle(0, 0, resultWidth, 131);
            rectangleGraphics.FillRectangle(Brushes.OrangeRed, rectangle);
        }

        using var rectangleStream = new MemoryStream();
        {
            mainBitmap.Save(rectangleStream, ImageFormat.Png);
        }

        #endregion

        #region Barcode

        var barcodeImage = GenerateBarcodeImage(barcode, resultWidth, barcodeHeight);

        #endregion

        #region MergedRectangleAndBarcode

        var newMainBitmap = (Bitmap)Image.FromStream(rectangleStream);
        var newBarcodeBitmap = barcodeImage;
        using var newRectangleGraphics = Graphics.FromImage(newMainBitmap);
        {
            newRectangleGraphics.DrawImage(newBarcodeBitmap, 0, 0);
        }

        using var mergedRectangleAndBarcodeStream = new MemoryStream();
        {
            newMainBitmap.Save(mergedRectangleAndBarcodeStream, ImageFormat.Png);
        }

        #endregion

        #region WriteShopName

        var barcodeBitmap = (Bitmap)Image.FromStream(mergedRectangleAndBarcodeStream);
        using var graphics = Graphics.FromImage(barcodeBitmap);
        {
            using var font = new Font("Tahoma", 10);
            {
                var rect = new Rectangle(0, shopNameY, resultWidth, 19);
                var sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.Trimming = StringTrimming.EllipsisCharacter;
                sf.FormatFlags = StringFormatFlags.DirectionRightToLeft;
                sf.LineAlignment = StringAlignment.Center;
                graphics.DrawString(shopName, font, Brushes.Black, rect, sf);
                //graphics.DrawRectangle(Pens.Green, rect);
            }
        }

        using var shopNameStream = new MemoryStream();
        {
            barcodeBitmap.Save(shopNameStream, ImageFormat.Png);
        }

        #endregion

        #region WriteVariant

        var barcodeWithShopNameBitmap = (Bitmap)Image.FromStream(shopNameStream);
        using var graphics2 = Graphics.FromImage(barcodeWithShopNameBitmap);
        {
            using var font = new Font("Tahoma", 10);
            {
                var rect = new Rectangle(0, deliveryDateY, resultWidth, 19);
                var sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.Trimming = StringTrimming.EllipsisCharacter;
                sf.FormatFlags = StringFormatFlags.DirectionRightToLeft;
                graphics2.DrawString(deliveryDate, font, Brushes.Black, rect, sf);
                //graphics2.DrawRectangle(Pens.Green, rect);
            }
        }

        using var finalStream = new MemoryStream();
        {
            barcodeWithShopNameBitmap.Save(finalStream, ImageFormat.Png);
        }

        return Convert.ToBase64String(finalStream.ToArray());

        #endregion
    }

    private static Bitmap GenerateBarcodeImage(string input, int width, int height)
    {
        // BarcodeLib package
        var barcodeInstance = new Barcode();
        var encodedBarcode = barcodeInstance.Encode(BarcodeStandard.Type.Code128, input, SKColors.Black,
            SKColors.OrangeRed, width, height);
        using var barcodeStream = new MemoryStream();
        {
            var imageToBarcode = Image.FromStream(encodedBarcode.Encode().AsStream());
            imageToBarcode.Save(barcodeStream, ImageFormat.Png);
        }
        return (Bitmap)Image.FromStream(barcodeStream);
    }
}
