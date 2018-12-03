# [WithoutHaste.DataFiles.Markdown](TableOfContents.WithoutHaste.DataFiles.Markdown.md).MarkdownTableRow

**Inheritance:** object  

Represents one row in a Markdown table.  

# Fields

## Cells

**[List&lt;string&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

List of the cells in the row.  

# Constructors

## MarkdownTableRow()

## MarkdownTableRow([List&lt;string&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) cells)

## MarkdownTableRow([String[]](https://docs.microsoft.com/en-us/dotnet/api/system.array) cells)

# Methods

## Add(string cell)

**void**  

Add a cell to the end of the row.  

**Parameters:**  
* **string cell**: Contents of cell.  

## ToMarkdown([List&lt;int&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) columnWidths)

**string**  

Return markdown-formatted text.  

**Remarks:**  
Column widths are padded an additional 1 space on left and right, per Markdown formatting.  

Line feed, new line, and tab characters are removed.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: ColumnWidths cannot be null or shorter than the row.  

