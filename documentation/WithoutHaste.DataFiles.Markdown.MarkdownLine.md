# [WithoutHaste.DataFiles.Markdown](TableOfContents.WithoutHaste.DataFiles.Markdown.md).MarkdownLine

**Inheritance:** object  
**Implements:** [IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md), [IMarkdownInList](WithoutHaste.DataFiles.Markdown.IMarkdownInList.md)  

Represents one line of text that will end in a line break.  

**Remarks:**  
Do not include the trailing white space or endline character.  

# Examples

## Example A:

Displays as: The quick brown fox.  \\n  

# Fields

## protected [List&lt;IMarkdownInLine&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) elements

Ordered inline elements that make up this line.  

# Properties

## [IMarkdownInLine[]](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md) Elements { get; }

Ordered inline elements that make up this line.  

**Remarks:**  
Expect mostly one plain text element.  

## bool IsEmpty { get; }

True when there are no elements in the line.  

# Constructors

## MarkdownLine([IMarkdownInLine[]](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md) elements)

Initialize line with any number of elements.  

## MarkdownLine([List&lt;IMarkdownInLine&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) elements)

Initialize line with any number of elements.  

## MarkdownLine(string text)

Initialize line with one MarkdownText element.  

# Methods

## void Add(string text)

Add a new MarkdownText containing the text to the end of the line.  

## void Add([IMarkdownInLine](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md) element)

Add an element to the end of the line.  

## void Add([List&lt;IMarkdownInLine&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) elements)

Add elements to the end of the line.  

## void Add([IMarkdownInLine[]](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md) elements)

Add elements to the end of the line.  

## void Concat([MarkdownLine](WithoutHaste.DataFiles.Markdown.MarkdownLine.md) other)

Appends the contents of the second line to end of this line.  

## void Prepend(string text)

Add a new MarkdownText containing the text to the beginning of the line.  

## void Prepend([IMarkdownInLine](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md) element)

Add an element to the beginning of the line.  

## virtual string ToMarkdown(string previousText)

Return markdown-formatted text.  

