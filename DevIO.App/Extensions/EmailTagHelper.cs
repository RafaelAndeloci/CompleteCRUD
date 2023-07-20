using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DevIO.App.Extensions;

public abstract class EmailTagHelper : TagHelper
{
    private string EmailDomain { get; set; } = "gmail.com";

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "a";
        var content = await output.GetChildContentAsync();
        var target = content.GetContent() + "@" + EmailDomain;
        output.Attributes.SetAttribute("href", "mailto:" + target);
        output.Content.SetContent(target);
    }
}