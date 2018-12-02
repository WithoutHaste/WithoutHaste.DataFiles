# [WithoutHaste.DataFiles.Markdown](TableOfContents.WithoutHaste.DataFiles.Markdown.md).MarkdownTable

**Inheritance:** object  
**Implements:** [IMarkdownIsBlock](WithoutHaste.DataFiles.Markdown.IMarkdownIsBlock.md), [IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md)  

Represents a markdown table.  

**Remarks:**  
Markdown requires each table to have exactly 1 header row, so the first row is assumed to be the header.  

# Fields

## const int MINIMUM_COLUMN_WIDTH

Minimum column width is 3 to allow for minimum "---" contents indicating header/data divider.  

## [List&lt;MarkdownTableRow&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) Rows

# Constructors

## MarkdownTable([List&lt;MarkdownTableRow&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) rows)

## MarkdownTable([MarkdownTableRow[]](WithoutHaste.DataFiles.Markdown.MarkdownTableRow.md) rows)

# Methods

## void Add([MarkdownTableRow](WithoutHaste.DataFiles.Markdown.MarkdownTableRow.md) row)

Add a row to the end of the table.  

## string ToMarkdown(string previousText)

Return markdown-formatted text.  

