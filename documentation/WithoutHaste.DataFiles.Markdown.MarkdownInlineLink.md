# [WithoutHaste.DataFiles.Markdown](TableOfContents.WithoutHaste.DataFiles.Markdown.md).MarkdownInlineLink

**Inheritance:** object → [MarkdownLink](WithoutHaste.DataFiles.Markdown.MarkdownLink.md)  
**Implements:** [IMarkdownInLine](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md), [IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md)  

Represents markdown inline-style link.  

# Examples

## Example A:

`new MarkdownInlineLink("google", "www.google.com")` is converted to string `[google](www.google.com)`.  

# Constructors

## MarkdownInlineLink(string text)

Link text and url are the same.  

## MarkdownInlineLink(string text, string url)

## MarkdownInlineLink([MarkdownText](WithoutHaste.DataFiles.Markdown.MarkdownText.md) text, string url)

Accepts formatted text.  

# Methods

## virtual string ToMarkdown(string previousText)

Outputs markdown-formatted text.  

