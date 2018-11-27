# [WithoutHaste.DataFiles.Markdown](TableOfContents.WithoutHaste.DataFiles.Markdown.md).MarkdownText

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object)  
**Implements:** [IMarkdownInLine](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md), [IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md)  

Represents plain text.  

# Properties

## [WithoutHaste.DataFiles.Markdown.TextStyle](WithoutHaste.DataFiles.Markdown.TextStyle.md) Style { get; protected set; }

**Remarks:**  
Supports multiple selections such as `TextStype.Bold | TextStyle.Italic`.  

**Misc:**  
  

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) Text { get; protected set; }

# Constructors

## MarkdownText([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text)

## MarkdownText([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text, [WithoutHaste.DataFiles.Markdown.TextStyle](WithoutHaste.DataFiles.Markdown.TextStyle.md) style)

# Methods

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) ToMarkdown([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) previousText = null)

Return markdown-formatted text.  

# Static Methods

## static MarkdownText Bold([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text)

Generate bold text.  

**Remarks:**  
_text_ is trimmed to conform to Markdown formatting requirements.  

## static MarkdownText BoldItalic([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text)

Generate bold-italic text.  

**Remarks:**  
_text_ is trimmed to conform to Markdown formatting requirements.  

## static MarkdownText Italic([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text)

Generate italic text.  

**Remarks:**  
_text_ is trimmed to conform to Markdown formatting requirements.  

