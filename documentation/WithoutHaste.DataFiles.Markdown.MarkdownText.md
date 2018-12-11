# [WithoutHaste.DataFiles.Markdown](TableOfContents.WithoutHaste.DataFiles.Markdown.md).MarkdownText

**Inheritance:** object  
**Implements:** [IMarkdownInLine](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md), [IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md)  

Represents plain text.  

# Properties

## Style

**[TextStyle](WithoutHaste.DataFiles.Markdown.TextStyle.md) { public get; protected set; }**  

**Remarks:**  
Supports multiple selections such as `TextStype.Bold | TextStyle.Italic`.  

**Misc:**  
  

## Text

**string { public get; protected set; }**  

# Constructors

## MarkdownText(string text)

## MarkdownText(string text, [TextStyle](WithoutHaste.DataFiles.Markdown.TextStyle.md) style)

# Methods

## ToMarkdown(string previousText = null)

**string**  

Return markdown-formatted text.  

# Static Methods

## Bold(string text)

**static MarkdownText**  

Generate bold text.  

**Remarks:**  
_text_ is trimmed to conform to Markdown formatting requirements.  

## BoldItalic(string text)

**static MarkdownText**  

Generate bold-italic text.  

**Remarks:**  
_text_ is trimmed to conform to Markdown formatting requirements.  

## EscapeControlCharacters(string text)

**static string**  

Replaces Markdown control characters with HTML encoded equivalents.  

**Remarks:**  
Handles backtic (&#96;), open angle brace (`<`), and close angle brace (`>`).  

## Italic(string text)

**static MarkdownText**  

Generate italic text.  

**Remarks:**  
_text_ is trimmed to conform to Markdown formatting requirements.  

