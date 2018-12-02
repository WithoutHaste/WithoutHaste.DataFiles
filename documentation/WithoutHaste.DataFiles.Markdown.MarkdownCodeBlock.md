# [WithoutHaste.DataFiles.Markdown](TableOfContents.WithoutHaste.DataFiles.Markdown.md).MarkdownCodeBlock

**Inheritance:** object  
**Implements:** [IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md), [IMarkdownIsBlock](WithoutHaste.DataFiles.Markdown.IMarkdownIsBlock.md)  

Represents a code block or CDATA block.  

# Fields

## readonly string Language

Language tag supported by highlight.js for syntax highlighting.  

## readonly string Text

Full text of code, with endline characters between lines.  

# Constructors

## MarkdownCodeBlock(string text)

## MarkdownCodeBlock(string text, string language)

# Methods

## string ToMarkdown(string previousText)

Return markdown-formatted text.  

