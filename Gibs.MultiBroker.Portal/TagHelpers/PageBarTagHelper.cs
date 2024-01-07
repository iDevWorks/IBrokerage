using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebTutor.TagHelpers
{
    [HtmlTargetElement("top-bar"), RestrictChildren("left", "right")]
    public class PageBarTagHelper : TagHelper
    {
        public bool Visible { get; set; } = true;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (!Visible)
            {
                output.SuppressOutput();
                return;
            }

            output.TagName = "header";    
            output.Attributes.SetAttribute("class", "d-flex flex-column flex-md-row gap-3 mb-4");

            output.Content.SetHtmlContent(await output.GetChildContentAsync());
        }
    }

    [HtmlTargetElement("left", ParentTag= "top-bar")]
    public class PageBarLeftTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "me-md-auto");

            output.Content.SetHtmlContent(await output.GetChildContentAsync());
        }
    }

    [HtmlTargetElement("right", ParentTag = "top-bar")]
    public class PageBarRightTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "d-flex gap-3");

            output.Content.SetHtmlContent(await output.GetChildContentAsync());
        }
    }

}
