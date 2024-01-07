using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebTutor.TagHelpers
{
    [HtmlTargetElement("modal"), RestrictChildren("header", "body", "footer")]
    public class ModalTagHelper : TagHelper
    {
        public string Id { get; set; } = "modal";
        public string? Size { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var contentDiv = new TagBuilder("div");
            contentDiv.Attributes.Add("class", "modal-content");
            contentDiv.InnerHtml.SetHtmlContent(await output.GetChildContentAsync());

            var dialogDiv = new TagBuilder("div");
            dialogDiv.Attributes.Add("class", $"modal-dialog modal-dialog-centered {Size}");
            dialogDiv.InnerHtml.SetHtmlContent(contentDiv);

            output.TagName = "div";
            output.Attributes.Add("id", Id);
            output.Attributes.Add("class", "modal fade");
            output.Attributes.Add("tabindex", "-1");

            output.Content.SetHtmlContent(dialogDiv);
        }
    }

    [HtmlTargetElement("header", ParentTag= "modal")]
    public class ModalHeaderTagHelper : TagHelper
    {
        public string? Title { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "header";
            output.AppendClassAttribute("modal-header", true);

            if (Title != null)
            {
                var h = new TagBuilder("h5");
                h.Attributes.Add("class", "modal-title");
                h.InnerHtml.AppendHtml(Title);

                output.Content.AppendHtml(h);
            }

            output.Content.AppendHtml(await output.GetChildContentAsync());

            var button = new TagBuilder("button");
            button.Attributes.Add("type", "button");
            button.Attributes.Add("class", "btn-close");
            button.Attributes.Add("data-bs-dismiss", "modal");

            output.Content.AppendHtml(button);
        }
    }

    [HtmlTargetElement("body", ParentTag = "modal")]
    public class ModalBodyTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.AppendClassAttribute("modal-body", true);

            output.Content.SetHtmlContent(await output.GetChildContentAsync());
        }
    }

    [HtmlTargetElement("footer", ParentTag = "modal")]
    public class ModalFooterTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "footer";
            output.AppendClassAttribute("modal-footer", true);

            output.Content.SetHtmlContent(await output.GetChildContentAsync());
        }
    }

}
