# [WithoutHaste.DataFiles.Markdown](TableOfContents.WithoutHaste.DataFiles.Markdown.md).MarkdownList

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object)  
**Implements:** [IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md), [IMarkdownInList](WithoutHaste.DataFiles.Markdown.IMarkdownInList.md), [IMarkdownIsBlock](WithoutHaste.DataFiles.Markdown.IMarkdownIsBlock.md)  

Represents a markdown list.  

# Properties

## [WithoutHaste.DataFiles.Markdown.IMarkdownInList](WithoutHaste.DataFiles.Markdown.IMarkdownInList.md) this[[int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) i] { get; }

Get an element from the list by 0-based index.  

## [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) Depth { get; protected set; }

0-indexed nesting depth of list.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) IsNumbered { get; protected set; }

True means the list will be numbered.   
False means the list will be bulleted.  

## [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) Length { get; }

The length of the list. Nested lists count as 1 each.  

# Constructors

## MarkdownList([bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) isNumbered = False)

Creates an empty list.  

## MarkdownList([bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) isNumbered = False, [IMarkdownInList[]](WithoutHaste.DataFiles.Markdown.IMarkdownInList.md) inList)

Creates a list of the specified MarkdownLines.  

## MarkdownList([bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) isNumbered = False, [IMarkdownInLine[]](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md) lines)

Creates a list MarkdownLines containing the specified IMarkdownInline elements.  

# Methods

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) Add([IMarkdownInList](WithoutHaste.DataFiles.Markdown.IMarkdownInList.md) line)

Adds element to the end of the list.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) Add([IMarkdownInLine](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md) element)

Adds new MarkdownLine containing the specified element to the end of the list.  

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) ToMarkdown([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) previousText)

Return markdown-formatted text.  

