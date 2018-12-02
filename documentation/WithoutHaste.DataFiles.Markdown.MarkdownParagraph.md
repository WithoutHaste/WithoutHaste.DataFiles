# [WithoutHaste.DataFiles.Markdown](TableOfContents.WithoutHaste.DataFiles.Markdown.md).MarkdownParagraph

**Inheritance:** object  
**Implements:** [IMarkdownIsBlock](WithoutHaste.DataFiles.Markdown.IMarkdownIsBlock.md), [IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md)  

Represents a grouping of elements that will end in a single double-line-break.  

**Remarks:**  
Nesting paragraphs inside paragraphs will still result in just one double-line-break at the end.  

# Examples

## Example A:

Displays as: The quick brown fox.\\n\\n  

# Fields

## protected [List&lt;IMarkdownInSection&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) elements

Ordered inline elements that make up this line.  

# Properties

## [IMarkdownInSection[]](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md) Elements { get; }

Ordered elements that make up this paragraph.  

**Remarks:**  
Expect mostly one plain text element.  

## bool IsEmpty { get; }

True when there are no elements in the line.  

# Constructors

## MarkdownParagraph([IMarkdownInSection[]](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md) elements)

Initialize paragraph with any number of elements.  

## MarkdownParagraph([List&lt;IMarkdownInSection&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) elements)

Initialize paragraph with any number of elements.  

## MarkdownParagraph(string text)

Initialize paragraph with one MarkdownText element.  

# Methods

## void Add(string text)

Add a new MarkdownText containing the text to the end of the paragraph.  

## void Add([IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md) element)

Add an element to the end of the paragraph.  

## void Add([List&lt;IMarkdownInSection&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) elements)

Add elements to the end of the paragraph.  

## void Add([IMarkdownInSection[]](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md) elements)

Add elements to the end of the paragraph.  

## void Prepend(string text)

Add a new MarkdownText containing the text to the beginning of the paragraph.  

## void Prepend([IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md) element)

Add an element to the beginning of the paragraph.  

## string ToMarkdown(string previousText)

Convert the paragraph to markdown-formatted text.  

