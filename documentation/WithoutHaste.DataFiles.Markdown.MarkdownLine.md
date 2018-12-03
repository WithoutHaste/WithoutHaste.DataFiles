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

## elements

**protected [List&lt;IMarkdownInLine&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

Ordered inline elements that make up this line.  

# Properties

## Elements

**[IMarkdownInLine[]](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md) { public get; }**  

Ordered inline elements that make up this line.  

**Remarks:**  
Expect mostly one plain text element.  

## IsEmpty

**bool { public get; }**  

True when there are no elements in the line.  

# Constructors

## MarkdownLine([IMarkdownInLine[]](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md) elements)

Initialize line with any number of elements.  

## MarkdownLine([List&lt;IMarkdownInLine&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) elements)

Initialize line with any number of elements.  

## MarkdownLine(string text)

Initialize line with one MarkdownText element.  

# Methods

## Add(string text)

**void**  

Add a new MarkdownText containing the text to the end of the line.  

## Add([IMarkdownInLine](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md) element)

**void**  

Add an element to the end of the line.  

## Add([List&lt;IMarkdownInLine&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) elements)

**void**  

Add elements to the end of the line.  

## Add([IMarkdownInLine[]](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md) elements)

**void**  

Add elements to the end of the line.  

## Concat([MarkdownLine](WithoutHaste.DataFiles.Markdown.MarkdownLine.md) other)

**void**  

Appends the contents of the second line to end of this line.  

## Prepend(string text)

**void**  

Add a new MarkdownText containing the text to the beginning of the line.  

## Prepend([IMarkdownInLine](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md) element)

**void**  

Add an element to the beginning of the line.  

## ToMarkdown(string previousText)

**virtual string**  

Return markdown-formatted text.  

