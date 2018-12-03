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

## elements

**protected [List&lt;IMarkdownInSection&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

Ordered inline elements that make up this line.  

# Properties

## Elements

**[IMarkdownInSection[]](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md) { public get; }**  

Ordered elements that make up this paragraph.  

**Remarks:**  
Expect mostly one plain text element.  

## IsEmpty

**bool { public get; }**  

True when there are no elements in the line.  

# Constructors

## MarkdownParagraph([IMarkdownInSection[]](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md) elements)

Initialize paragraph with any number of elements.  

## MarkdownParagraph([List&lt;IMarkdownInSection&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) elements)

Initialize paragraph with any number of elements.  

## MarkdownParagraph(string text)

Initialize paragraph with one MarkdownText element.  

# Methods

## Add(string text)

**void**  

Add a new MarkdownText containing the text to the end of the paragraph.  

## Add([IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md) element)

**void**  

Add an element to the end of the paragraph.  

## Add([List&lt;IMarkdownInSection&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) elements)

**void**  

Add elements to the end of the paragraph.  

## Add([IMarkdownInSection[]](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md) elements)

**void**  

Add elements to the end of the paragraph.  

## Prepend(string text)

**void**  

Add a new MarkdownText containing the text to the beginning of the paragraph.  

## Prepend([IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md) element)

**void**  

Add an element to the beginning of the paragraph.  

## ToMarkdown(string previousText)

**string**  

Convert the paragraph to markdown-formatted text.  

