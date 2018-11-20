# [WithoutHaste.DataFiles.Markdown](TableOfContents.WithoutHaste.DataFiles.Markdown.md).MarkdownParagraph

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object)  
**Implements:** [IMarkdownIsBlock](WithoutHaste.DataFiles.Markdown.IMarkdownIsBlock.md), [IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md)  

Represents a grouping of elements that will end in a single double-line-break.  

**Remarks:**  
Nesting paragraphs inside paragraphs will still result in just one double-line-break at the end.  

# Examples

## Example A:

Displays as: The quick brown fox.\\n\\n  

# Fields

## protected [List&lt;WithoutHaste.DataFiles.Markdown.IMarkdownInSection&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) elements

Ordered inline elements that make up this line.  

# Properties

## WithoutHaste.DataFiles.Markdown.IMarkdownInSection[] Elements { get; }

Ordered elements that make up this paragraph.  

**Remarks:**  
Expect mostly one plain text element.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) IsEmpty { get; }

True when there are no elements in the line.  

# Constructors

## MarkdownParagraph(WithoutHaste.DataFiles.Markdown.IMarkdownInSection[] elements)

Initialize paragraph with any number of elements.  

## MarkdownParagraph([List&lt;WithoutHaste.DataFiles.Markdown.IMarkdownInSection&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) elements)

Initialize paragraph with any number of elements.  

## MarkdownParagraph([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text)

Initialize paragraph with one MarkdownText element.  

# Methods

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) Add([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text)

Add a new MarkdownText containing the text to the end of the paragraph.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) Add([WithoutHaste.DataFiles.Markdown.IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md) element)

Add an element to the end of the paragraph.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) Add([List&lt;WithoutHaste.DataFiles.Markdown.IMarkdownInSection&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) elements)

Add elements to the end of the paragraph.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) Add(WithoutHaste.DataFiles.Markdown.IMarkdownInSection[] elements)

Add elements to the end of the paragraph.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) Prepend([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text)

Add a new MarkdownText containing the text to the beginning of the paragraph.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) Prepend([WithoutHaste.DataFiles.Markdown.IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md) element)

Add an element to the beginning of the paragraph.  

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) ToMarkdown([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) previousText)

Convert the paragraph to markdown-formatted text.  

