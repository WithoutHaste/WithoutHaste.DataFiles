# [WithoutHaste.DataFiles.Markdown](TableOfContents.WithoutHaste.DataFiles.Markdown.md).MarkdownLink

**Abstract**  
**Inheritance:** object  
**Implements:** [IMarkdownInLine](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md), [IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md)  

# Fields

## protected [MarkdownText](WithoutHaste.DataFiles.Markdown.MarkdownText.md) text

Styled text of link.  

# Properties

## string Text { get; }

Plain text of link.  

## string Url { get; protected set; }

Url of target.  

# Constructors

## MarkdownLink(string text)

Link text and url are the same.  

## MarkdownLink(string text, string url)

## MarkdownLink([MarkdownText](WithoutHaste.DataFiles.Markdown.MarkdownText.md) text, string url)

# Methods

## abstract string ToMarkdown(string previousText)

Outputs markdown-formatted text.  

# Derived By

[WithoutHaste.DataFiles.Markdown.MarkdownInlineLink](WithoutHaste.DataFiles.Markdown.MarkdownInlineLink.md)  
Represents markdown inline-style link.  

