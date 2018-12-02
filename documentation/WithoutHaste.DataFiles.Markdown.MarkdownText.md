# [WithoutHaste.DataFiles.Markdown](TableOfContents.WithoutHaste.DataFiles.Markdown.md).MarkdownText

**Inheritance:** object  
**Implements:** [IMarkdownInLine](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md), [IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md)  

Represents plain text.  

# Properties

## [TextStyle](WithoutHaste.DataFiles.Markdown.TextStyle.md) Style { get; protected set; }

**Remarks:**  
Supports multiple selections such as `TextStype.Bold | TextStyle.Italic`.  

**Misc:**  
  

## string Text { get; protected set; }

# Constructors

## MarkdownText(string text)

## MarkdownText(string text, [TextStyle](WithoutHaste.DataFiles.Markdown.TextStyle.md) style)

# Methods

## string ToMarkdown(string previousText = null)

Return markdown-formatted text.  

# Static Methods

## static MarkdownText Bold(string text)

Generate bold text.  

**Remarks:**  
_text_ is trimmed to conform to Markdown formatting requirements.  

## static MarkdownText BoldItalic(string text)

Generate bold-italic text.  

**Remarks:**  
_text_ is trimmed to conform to Markdown formatting requirements.  

## static MarkdownText Italic(string text)

Generate italic text.  

**Remarks:**  
_text_ is trimmed to conform to Markdown formatting requirements.  

