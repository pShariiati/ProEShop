using System.Text;
using Microsoft.AspNetCore.Identity;

namespace ProEShop.Common.IdentityToolkit
{
    public static class IdentityExtensions
    {
        /// <summary>
        /// IdentityResult errors list to string
        /// </summary>
        public static string DumpErrors(this IdentityResult result, bool useHtmlNewLine = false)
        {
            var results = new StringBuilder();
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    var errorDescription = error.Description;
                    if (string.IsNullOrWhiteSpace(errorDescription))
                    {
                        continue;
                    }

                    if (!useHtmlNewLine)
                    {
                        results.AppendLine(errorDescription);//"\n"
                    }
                    else
                    {
                        results.Append(errorDescription).AppendLine("<br/>");//"\n"
                    }
                }
            }
            return results.ToString();
        }
    }
}
