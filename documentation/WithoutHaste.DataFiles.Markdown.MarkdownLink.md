# [WithoutHaste.DataFiles.Markdown](TableOfContents.WithoutHaste.DataFiles.Markdown.md).MarkdownLink

**Abstract**  
**Inheritance:** object  
**Implements:** [IMarkdownInLine](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md), [IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md)  

# Fields

## text

**protected [MarkdownText](WithoutHaste.DataFiles.Markdown.MarkdownText.md)**  

Styled text of link.  

# Properties

## Text

**string { public get; }**  

Plain text of link.  

## Url

**string { public get; protected set; }**  

Url of target.  

# Constructors

## MarkdownLink(string text)

Link text and url are the same.  

## MarkdownLink(string text, string url)

## MarkdownLink([MarkdownText](WithoutHaste.DataFiles.Markdown.MarkdownText.md) text, string url)

# Methods

## ToMarkdown(string previousText)

**abstract string**  

Outputs markdown-formatted text.  

# Derived By

[WithoutHaste.DataFiles.Markdown.MarkdownInlineLink](WithoutHaste.DataFiles.Markdown.MarkdownInlineLink.md)  
Represents markdown inline-style link.  

