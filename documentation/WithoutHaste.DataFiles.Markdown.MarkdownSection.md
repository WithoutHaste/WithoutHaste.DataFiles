# [WithoutHaste.DataFiles.Markdown](TableOfContents.WithoutHaste.DataFiles.Markdown.md).MarkdownSection

**Inheritance:** object  
**Implements:** [IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md), [IMarkdownIsBlock](WithoutHaste.DataFiles.Markdown.IMarkdownIsBlock.md)  

Represents a header and all contents until the next header of the same depth.  

# Properties

## int Depth { get; protected set; }

0-indexed nesting depth of section.  

**Example A:**  
`# Header` is depth 1  

**Example B:**  
`## Header` is depth 2  

## [IMarkdownInSection[]](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md) Elements { get; }

All markdown elements within section.  

## string Header { get; set; }

Displayed header text.  

## bool IsEmpty { get; }

True if the section contains no elements.  

# Constructors

## MarkdownSection(string header, int depth = 1)

# Methods

## void Add([IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md) element)

Adds the element to the end of this section.  

## void Add([IMarkdownInSection[]](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md) elements)

Adds all the elements to the end of this section.  

## void Add([List&lt;IMarkdownInSection&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) elements)

Adds all the elements to the end of this section.  

## void AddInLine([IMarkdownInLine](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md) element)

Adds the element in a new MarkdownLine at the end of this section.  

## void AddInLine(string text)

Adds the text in a new MarkdownLine at the end of this section.  

## void AddInLine([IMarkdownInLine[]](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md) elements)

Adds the elements in a new MarkdownLine at the end of this section.  

## void AddInLine([List&lt;IMarkdownInLine&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) elements)

Adds the elements in a new MarkdownLine at the end of this section.  

## void AddInParagraph([IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md) element)

Adds the element in a new MarkdownParagraph at the end of this section.  

## void AddInParagraph([IMarkdownInSection[]](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md) elements)

Adds the elements in a new MarkdownParagraph at the end of this section.  

## void AddInParagraph([List&lt;IMarkdownInSection&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) elements)

Adds the elements in a new MarkdownParagraph at the end of this section.  

## void AddInParagraph(string text)

Adds the text in a new MarkdownParagraph at the end of this section.  

## MarkdownSection AddSection(string header)

Creates new section and adds it to the end of this section. Defaults to depth of parent + 1.  

**Returns:**  
The new section  

**Parameters:**  
* **string header**: Section header  

## void AddSection([MarkdownSection](WithoutHaste.DataFiles.Markdown.MarkdownSection.md) section)

Adds existing section to the end of this section. Depths are updated.  

**Parameters:**  
* **MarkdownSection section**: Existing section.  

## bool EndsWith([Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) type)

Returns true if the last element in the section has the specified type.  

## string ToMarkdown()

Return markdown-formatted text, taking the previous text of the file into account.  

## string ToMarkdown(string previousText)

Return markdown-formatted text, taking the previous text of the file into account.  

