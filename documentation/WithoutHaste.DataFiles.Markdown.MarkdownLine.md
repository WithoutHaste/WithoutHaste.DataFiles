# [WithoutHaste.DataFiles.Markdown](TableOfContents.WithoutHaste.DataFiles.Markdown.md).MarkdownLine

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object)  
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

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) IsEmpty { get; }

True when there are no elements in the line.  

# Constructors

## MarkdownLine([IMarkdownInLine[]](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md) elements)

Initialize line with any number of elements.  

## MarkdownLine([List&lt;IMarkdownInLine&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) elements)

Initialize line with any number of elements.  

## MarkdownLine([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text)

Initialize line with one MarkdownText element.  

# Methods

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) Add([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text)

Add a new MarkdownText containing the text to the end of the line.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) Add([IMarkdownInLine](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md) element)

Add an element to the end of the line.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) Add([List&lt;IMarkdownInLine&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) elements)

Add elements to the end of the line.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) Add([IMarkdownInLine[]](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md) elements)

Add elements to the end of the line.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) Concat([MarkdownLine](WithoutHaste.DataFiles.Markdown.MarkdownLine.md) other)

Appends the contents of the second line to end of this line.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) Prepend([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text)

Add a new MarkdownText containing the text to the beginning of the line.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) Prepend([IMarkdownInLine](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md) element)

Add an element to the beginning of the line.  

## virtual [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) ToMarkdown([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) previousText)

Return markdown-formatted text.  

