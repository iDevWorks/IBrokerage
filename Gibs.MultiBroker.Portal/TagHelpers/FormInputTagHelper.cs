using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebTutor.TagHelpers
{
    [HtmlTargetElement("input", Attributes ="asp-label")]
    public class FormInputTagHelper : TagHelper
    {
        public int? LabelSize { get; set; } = 4;


        //<div class="row mb-3 mt-3">
        //    <label class="form-label col-4" asp-for="@Model.OldPassword">Old Password</label>
        //    <div class="col-8">
        //        <input class="form-control" asp-for="@Model.OldPassword" type="password">
        //    </div>                      
        //</div>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (LabelSize > 11 || LabelSize < 2) LabelSize = 4;

            string inputId = output.GetAttribute("id");
            string labelContent = output.GetAttribute("asp-label");

            var input = new TagBuilder("input") { TagRenderMode = TagRenderMode.SelfClosing };
            input.Attributes.Add("class", "form-control");
            input.Attributes.Copy(output.Attributes);

            var label = new TagBuilder("label");
            label.Attributes.Add("for", inputId);
            label.Attributes.Add("class", $"col-{LabelSize} form-label");
            label.InnerHtml.AppendHtml(labelContent);

            var div = new TagBuilder("div");
            div.Attributes.Add("class", $"col-{12 - LabelSize}");
            div.InnerHtml.AppendHtml(input);

            //write new element
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Clear();
            output.Attributes.Add("class", "row mb-3 mt-3");

            output.Content.AppendHtml(label);
            output.Content.AppendHtml(div);
        }
    }

}
