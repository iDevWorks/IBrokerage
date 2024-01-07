using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebTutor.TagHelpers
{
    [HtmlTargetElement("card"), RestrictChildren("header", "body", "footer")]
    public class CardTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";    
            output.AppendClassAttribute("card", true);

            output.Content.SetHtmlContent(await output.GetChildContentAsync());
        }
    }

    [HtmlTargetElement("header", ParentTag= "card")]
    public class CardHeaderTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "header";
            output.AppendClassAttribute("card-header", true);

            output.Content.SetHtmlContent(await output.GetChildContentAsync());
        }
    }

    [HtmlTargetElement("body", ParentTag = "card")]
    public class CardBodyTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.AppendClassAttribute("card-body", true);

            output.Content.SetHtmlContent(await output.GetChildContentAsync());
        }
    }

    [HtmlTargetElement("footer", ParentTag = "card")]
    public class CardFooterTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "footer";
            output.AppendClassAttribute("card-footer", true);

            output.Content.SetHtmlContent(await output.GetChildContentAsync());
        }
    }

}
