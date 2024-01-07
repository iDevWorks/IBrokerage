using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace WebTutor.TagHelpers
{
    [HtmlTargetElement("rating")]
    public class RatingTagHelper : TagHelper
    {
        public int Value { get; set; } = 3;
        public int MaxValue { get; set; } = 5;
        public bool ShowValue { get; set; } = false;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";    
            output.Attributes.Add("class", "text-nowrap");

            if (Value < 0) Value = 0;
            if (Value > MaxValue) Value = MaxValue;

            for (int i = 0; i < Value; i++)
            {
                var span = new TagBuilder("span");
                span.Attributes.Add("class", "bi bi-star-fill text-primary");
                output.Content.AppendHtml(span);
            }
            for (int i = Value; i < 5; i++)
            {
                var span = new TagBuilder("span");
                span.Attributes.Add("class", "bi bi-star text-primary");
                output.Content.AppendHtml(span);
            }

            if (ShowValue)
            {
                output.Content.AppendHtml(" " + Value.ToString());
            }
        }
    }
}
//<span class="bi bi-star-fill text-primary"></span>
