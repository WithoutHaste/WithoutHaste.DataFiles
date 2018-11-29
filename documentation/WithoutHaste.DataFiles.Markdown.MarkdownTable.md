# [WithoutHaste.DataFiles.Markdown](TableOfContents.WithoutHaste.DataFiles.Markdown.md).MarkdownTable

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object)  
**Implements:** [IMarkdownIsBlock](WithoutHaste.DataFiles.Markdown.IMarkdownIsBlock.md), [IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md)  

Represents a markdown table.  

**Remarks:**  
Markdown requires each table to have exactly 1 header row, so the first row is assumed to be the header.  

# Fields

## const [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) MINIMUM_COLUMN_WIDTH

Minimum column width is 3 to allow for minimum "---" contents indicating header/data divider.  

## [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) Rows

# Constructors

## MarkdownTable([List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) rows)

## MarkdownTable(MarkdownTableRow[] rows)

# Methods

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) Add([MarkdownTableRow](WithoutHaste.DataFiles.Markdown.MarkdownTableRow.md) row)

Add a row to the end of the table.  

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) ToMarkdown([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) previousText)

Return markdown-formatted text.  

