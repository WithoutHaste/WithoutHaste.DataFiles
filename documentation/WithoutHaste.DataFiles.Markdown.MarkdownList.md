# [WithoutHaste.DataFiles.Markdown](TableOfContents.WithoutHaste.DataFiles.Markdown.md).MarkdownList

**Inheritance:** object  
**Implements:** [IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md), [IMarkdownInList](WithoutHaste.DataFiles.Markdown.IMarkdownInList.md), [IMarkdownIsBlock](WithoutHaste.DataFiles.Markdown.IMarkdownIsBlock.md)  

Represents a markdown list.  

# Properties

## [IMarkdownInList](WithoutHaste.DataFiles.Markdown.IMarkdownInList.md) this[int i] { get; }

Get an element from the list by 0-based index.  

## int Depth { get; protected set; }

0-indexed nesting depth of list.  

## bool IsNumbered { get; protected set; }

True means the list will be numbered.   
False means the list will be bulleted.  

## int Length { get; }

The length of the list. Nested lists count as 1 each.  

# Constructors

## MarkdownList(bool isNumbered = False)

Creates an empty list.  

## MarkdownList(bool isNumbered = False, [IMarkdownInList[]](WithoutHaste.DataFiles.Markdown.IMarkdownInList.md) elements)

Creates a list of the specified _elements_.  

## MarkdownList(bool isNumbered = False, [IMarkdownInLine[]](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md) inLineElements)

Creates a list of MarkdownLines, each containing one of the _inLineElements_.  

# Methods

## void Add([IMarkdownInList](WithoutHaste.DataFiles.Markdown.IMarkdownInList.md) element)

Adds _element_ to the end of the list.  

## void Add([IMarkdownInLine](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md) element)

Adds new MarkdownLine containing the _element_ to the end of the list.  

## string ToMarkdown(string previousText)

Return markdown-formatted text.  

