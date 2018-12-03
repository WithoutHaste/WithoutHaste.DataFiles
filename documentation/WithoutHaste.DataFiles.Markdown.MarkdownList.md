# [WithoutHaste.DataFiles.Markdown](TableOfContents.WithoutHaste.DataFiles.Markdown.md).MarkdownList

**Inheritance:** object  
**Implements:** [IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md), [IMarkdownInList](WithoutHaste.DataFiles.Markdown.IMarkdownInList.md), [IMarkdownIsBlock](WithoutHaste.DataFiles.Markdown.IMarkdownIsBlock.md)  

Represents a markdown list.  

# Properties

## this[int i]

**[IMarkdownInList](WithoutHaste.DataFiles.Markdown.IMarkdownInList.md) { public get; }**  

Get an element from the list by 0-based index.  

## Depth

**int { public get; protected set; }**  

0-indexed nesting depth of list.  

## IsNumbered

**bool { public get; protected set; }**  

True means the list will be numbered.   
False means the list will be bulleted.  

## Length

**int { public get; }**  

The length of the list. Nested lists count as 1 each.  

# Constructors

## MarkdownList(bool isNumbered = False)

Creates an empty list.  

## MarkdownList(bool isNumbered = False, [IMarkdownInList[]](WithoutHaste.DataFiles.Markdown.IMarkdownInList.md) elements)

Creates a list of the specified _elements_.  

## MarkdownList(bool isNumbered = False, [IMarkdownInLine[]](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md) inLineElements)

Creates a list of MarkdownLines, each containing one of the _inLineElements_.  

# Methods

## Add([IMarkdownInList](WithoutHaste.DataFiles.Markdown.IMarkdownInList.md) element)

**void**  

Adds _element_ to the end of the list.  

## Add([IMarkdownInLine](WithoutHaste.DataFiles.Markdown.IMarkdownInLine.md) element)

**void**  

Adds new MarkdownLine containing the _element_ to the end of the list.  

## ToMarkdown(string previousText)

**string**  

Return markdown-formatted text.  

