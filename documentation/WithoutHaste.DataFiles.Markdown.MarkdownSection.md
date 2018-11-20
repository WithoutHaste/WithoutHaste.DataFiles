# [WithoutHaste.DataFiles.Markdown](TableOfContents.WithoutHaste.DataFiles.Markdown.md).MarkdownSection

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object)  
**Implements:** [IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md), [IMarkdownIsBlock](WithoutHaste.DataFiles.Markdown.IMarkdownIsBlock.md)  

Represents a header and all contents until the next header of the same depth.  

# Properties

## [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) Depth { get; protected set; }

0-indexed nesting depth of section.  

**Example A:**  
"# Header" is depth 1  

**Example B:**  
"## Header" is depth 2  

## WithoutHaste.DataFiles.Markdown.IMarkdownInSection[] Elements { get; }

All markdown elements within section.  

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) Header { get; set; }

Displayed header text.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) IsEmpty { get; }

True if the section contains no elements.  

# Constructors

## MarkdownSection([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) header, [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) depth = 1)

# Methods

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) Add([WithoutHaste.DataFiles.Markdown.IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md) element)

Adds the element to the end of this section.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) Add(WithoutHaste.DataFiles.Markdown.IMarkdownInSection[] elements)

Adds all the elements to the end of this section.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) Add([List&lt;WithoutHaste.DataFiles.Markdown.IMarkdownInSection&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) elements)

Adds all the elements to the end of this section.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) AddInLine([WithoutHaste.DataFiles.Markdown.IMarkdownInLine](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md) element)

Adds the element in a new MarkdownLine at the end of this section.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) AddInLine([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text)

Adds the text in a new MarkdownLine at the end of this section.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) AddInLine(WithoutHaste.DataFiles.Markdown.IMarkdownInLine[] elements)

Adds the elements in a new MarkdownLine at the end of this section.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) AddInLine([List&lt;WithoutHaste.DataFiles.Markdown.IMarkdownInLine&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) elements)

Adds the elements in a new MarkdownLine at the end of this section.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) AddInParagraph([WithoutHaste.DataFiles.Markdown.IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md) element)

Adds the element in a new MarkdownParagraph at the end of this section.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) AddInParagraph(WithoutHaste.DataFiles.Markdown.IMarkdownInSection[] elements)

Adds the elements in a new MarkdownParagraph at the end of this section.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) AddInParagraph([List&lt;WithoutHaste.DataFiles.Markdown.IMarkdownInSection&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) elements)

Adds the elements in a new MarkdownParagraph at the end of this section.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) AddInParagraph([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text)

Adds the text in a new MarkdownParagraph at the end of this section.  

## MarkdownSection AddSection([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) header)

Creates new section and adds it to the end of this section. Defaults to depth of parent + 1.  

**Returns:**  
The new section  

**Parameters:**  
* **[string](https://docs.microsoft.com/en-us/dotnet/api/system.string) header**: Section header  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) AddSection(MarkdownSection section)

Adds existing section to the end of this section. Depths are updated.  

**Parameters:**  
* **MarkdownSection section**: Existing section.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) EndsWith([Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) type)

Returns true if the last element in the section has the specified type.  

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) ToMarkdown()

**Misc:**  
  

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) ToMarkdown([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) previousText)

Return markdown-formatted text, taking the previous text of the file into account.  

