# [WithoutHaste.DataFiles.Markdown](TableOfContents.WithoutHaste.DataFiles.Markdown.md).MarkdownTable

**Inheritance:** object  
**Implements:** [IMarkdownIsBlock](WithoutHaste.DataFiles.Markdown.IMarkdownIsBlock.md), [IMarkdownInSection](WithoutHaste.DataFiles.Markdown.IMarkdownInSection.md)  

Represents a markdown table.  

**Remarks:**  
Markdown requires each table to have exactly 1 header row, so the first row is assumed to be the header.  

# Fields

## MINIMUM_COLUMN_WIDTH

**const int**  

Minimum column width is 3 to allow for minimum "---" contents indicating header/data divider.  

## Rows

**[List&lt;MarkdownTableRow&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

# Constructors

## MarkdownTable([List&lt;MarkdownTableRow&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) rows)

## MarkdownTable([MarkdownTableRow[]](WithoutHaste.DataFiles.Markdown.MarkdownTableRow.md) rows)

# Methods

## Add([MarkdownTableRow](WithoutHaste.DataFiles.Markdown.MarkdownTableRow.md) row)

**void**  

Add a row to the end of the table.  

## ToMarkdown(string previousText)

**string**  

Return markdown-formatted text.  

