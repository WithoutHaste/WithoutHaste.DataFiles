# [WithoutHaste.DataFiles.Markdown](TableOfContents.WithoutHaste.DataFiles.Markdown.md).MarkdownInlineLink

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [MarkdownLink](WithoutHaste.DataFiles.Markdown.MarkdownLink.md)  
**Implements:** [IMarkdownInLine](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md), [IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md)  

Represents markdown inline-style link.  

# Examples

## Example A:

`new MarkdownInlineLink("google", "www.google.com")` is converted to string `[google](www.google.com)`.  

# Constructors

## MarkdownInlineLink([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text)

Link text and url are the same.  

## MarkdownInlineLink([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) url)

## MarkdownInlineLink([MarkdownText](WithoutHaste.DataFiles.Markdown.MarkdownText.md) text, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) url)

Accepts formatted text.  

# Methods

## virtual [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) ToMarkdown([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) previousText)

Outputs markdown-formatted text.  

