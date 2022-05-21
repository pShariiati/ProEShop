using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProEShop.Common.Helpers;

/// <summary>
/// https://stackoverflow.com/questions/2920744/url-slugify-algorithm-in-c
/// https://www.dntips.ir/post/1774
/// https://www.dntips.ir/post/1529
/// http://alirezasaberi.ir/it/c/ Recognize Farsi letters
/// https://en.wikipedia.org/wiki/Zero-width_non-joiner
/// </summary>
public static class GenerateSlug
{
    // SEO
    private const int MaxLengthSlug = 50;
    /// <summary>
    /// ZeroWithNonJoiner
    /// ‌
    /// https://www.codetable.net/decimal/8204
    /// </summary>
    private const char ZeroWithNonJoiner = (char)(8204);
    public static string ToUrlSlug(this string phrase)
    {
        var str = phrase.RemoveDiacritics()
            .RemoveAccent()
            .ToLower();
        // invalid chars
        str = Regex.Replace(str, @"[^\u0600-\u06FF\uFB8A\u067E\u0686\u06AF\u200C\u200Fa-z0-9\s-]",
            string.Empty);
        // convert multiple spaces into one space
        str = Regex.Replace(str, @"\s+", " ").Trim();
        // cut and trim
        str = str.Substring(0, str.Length <= MaxLengthSlug ? str.Length : MaxLengthSlug).Trim();
        // add hyphens
        str = Regex.Replace(str, @"\s", "-");
        // replace half space
        str = Regex.Replace(str, ZeroWithNonJoiner.ToString(), "-");
        // convert multiple dashes into one dash
        str = Regex.Replace(str, @"-+", "-");
        return str.Trim('-');
    }

    private static string RemoveAccent(this string text)
    {
        var bytes = Encoding.GetEncoding("UTF-8").GetBytes(text);
        return Encoding.UTF8.GetString(bytes);
    }

    /// <summary>
    /// حذف اعراب حروف
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    private static string RemoveDiacritics(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;

        // Why we use FromKC
        // https://www.dntips.ir/post/1774/%d8%ad%d8%b0%d9%81-%d8%a7%d8%b9%d8%b1%d8%a7%d8%a8-%d8%a7%d8%b2-%d8%ad%d8%b1%d9%88%d9%81-%d9%88-%da%a9%d9%84%d9%85%d8%a7%d8%aa#comment-13508
        var normalizedString = text.Normalize(NormalizationForm.FormKC);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }
}