# [WithoutHaste.DataFiles.Markdown](TableOfContents.WithoutHaste.DataFiles.Markdown.md).MarkdownLink

**Abstract**  
**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object)  
**Implements:** [IMarkdownInLine](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md), [IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md)  

# Fields

## protected [MarkdownText](WithoutHaste.DataFiles.Markdown.MarkdownText.md) text

Styled text of link.  

# Properties

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) Text { get; }

Plain text of link.  

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) Url { get; protected set; }

Url of target.  

# Constructors

## MarkdownLink([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text)

Link text and url are the same.  

## MarkdownLink([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) url)

## MarkdownLink([MarkdownText](WithoutHaste.DataFiles.Markdown.MarkdownText.md) text, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) url)

# Methods

## abstract [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) ToMarkdown([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) previousText)

Outputs markdown-formatted text.  

# Derived By

[WithoutHaste.DataFiles.Markdown.MarkdownInlineLink](WithoutHaste.DataFiles.Markdown.MarkdownInlineLink.md)  
Represents markdown inline-style link.  

