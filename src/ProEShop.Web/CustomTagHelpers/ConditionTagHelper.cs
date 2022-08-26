using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ProEShop.Web.CustomTagHelpers;

/// <summary>
/// برای چک کردن ایف و الس های یک خطی
/// </summary>
[HtmlTargetElement(Attributes = nameof(Condition))]
public class ConditionTagHelper : TagHelper
{
    public bool Condition { get; set; }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (!Condition)
        {
            output.SuppressOutput();
        }
        return base.ProcessAsync(context, output);
    }
}
