# [WithoutHaste.DataFiles.Markdown](TableOfContents.WithoutHaste.DataFiles.Markdown.md).MarkdownSection

**Inheritance:** object  
**Implements:** [IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md), [IMarkdownIsBlock](WithoutHaste.DataFiles.Markdown.IMarkdownIsBlock.md)  

Represents a header and all contents until the next header of the same depth.  

# Properties

## Depth

**int { public get; protected set; }**  

0-indexed nesting depth of section.  

**Example A:**  
`# Header` is depth 1  

**Example B:**  
`## Header` is depth 2  

## Elements

**[IMarkdownInSection[]](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md) { public get; }**  

All markdown elements within section.  

## Header

**string { public get; public set; }**  

Displayed header text.  

## IsEmpty

**bool { public get; }**  

True if the section contains no elements.  

# Constructors

## MarkdownSection(string header, int depth = 1)

# Methods

## Add([IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md) element)

**void**  

Adds the element to the end of this section.  

## Add([IMarkdownInSection[]](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md) elements)

**void**  

Adds all the elements to the end of this section.  

## Add([List&lt;IMarkdownInSection&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) elements)

**void**  

Adds all the elements to the end of this section.  

## AddInLine([IMarkdownInLine](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md) element)

**void**  

Adds the element in a new MarkdownLine at the end of this section.  

## AddInLine(string text)

**void**  

Adds the text in a new MarkdownLine at the end of this section.  

## AddInLine([IMarkdownInLine[]](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md) elements)

**void**  

Adds the elements in a new MarkdownLine at the end of this section.  

## AddInLine([List&lt;IMarkdownInLine&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) elements)

**void**  

Adds the elements in a new MarkdownLine at the end of this section.  

## AddInParagraph([IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md) element)

**void**  

Adds the element in a new MarkdownParagraph at the end of this section.  

## AddInParagraph([IMarkdownInSection[]](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md) elements)

**void**  

Adds the elements in a new MarkdownParagraph at the end of this section.  

## AddInParagraph([List&lt;IMarkdownInSection&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) elements)

**void**  

Adds the elements in a new MarkdownParagraph at the end of this section.  

## AddInParagraph(string text)

**void**  

Adds the text in a new MarkdownParagraph at the end of this section.  

## AddSection(string header)

**MarkdownSection**  

Creates new section and adds it to the end of this section. Defaults to depth of parent + 1.  

**Returns:**  
The new section  

**Parameters:**  
* **string header**: Section header  

## AddSection([MarkdownSection](WithoutHaste.DataFiles.Markdown.MarkdownSection.md) section)

**void**  

Adds existing section to the end of this section. Depths are updated.  

**Parameters:**  
* **MarkdownSection section**: Existing section.  

## EndsWith([Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) type)

**bool**  

Returns true if the last element in the section has the specified type.  

## ToMarkdown()

**string**  

Return markdown-formatted text, taking the previous text of the file into account.  

## ToMarkdown(string previousText)

**string**  

Return markdown-formatted text, taking the previous text of the file into account.  

