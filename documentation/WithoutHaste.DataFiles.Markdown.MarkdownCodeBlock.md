# [WithoutHaste.DataFiles.Markdown](TableOfContents.WithoutHaste.DataFiles.Markdown.md).MarkdownCodeBlock

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object)  
**Implements:** [IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md), [IMarkdownIsBlock](WithoutHaste.DataFiles.Markdown.IMarkdownIsBlock.md)  

Represents a code block or CDATA block.  

# Fields

## readonly [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) Language

Language tag supported by highlight.js for syntax highlighting.  

## readonly [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) Text

Full text of code, with endline characters between lines.  

# Constructors

## MarkdownCodeBlock([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text)

## MarkdownCodeBlock([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) language)

# Methods

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) ToMarkdown([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) previousText)

Return markdown-formatted text.  

