using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebTutor.TagHelpers
{
    public static class Extensions
    {
        public static string GetAttribute(this TagHelperOutput output, string name)
        {
            if (output.Attributes.TryGetAttribute(name, out TagHelperAttribute attribute))
            {
                return attribute.Value.ToString()!;
            }

            return string.Empty;
        }

        public static void AppendClassAttribute(this TagHelperOutput output, string value, bool prepend = false)
        {
            if (output.Attributes.TryGetAttribute("class", out TagHelperAttribute attribute))
            {
                string oldValue = attribute.Value.ToString()!;
                value = prepend ? $"{value} {oldValue}" : $"{oldValue} {value}";
            }

            output.Attributes.SetAttribute("class", value);
        }

        public static void Copy(this AttributeDictionary to, TagHelperAttributeList from)
        {
            foreach (var attr in from)
            {
                to.Remove(attr.Name);
                to.Add(attr.Name, attr.Value.ToString());
            }
        }

    }
}
