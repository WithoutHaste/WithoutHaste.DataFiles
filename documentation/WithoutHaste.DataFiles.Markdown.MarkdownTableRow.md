# [WithoutHaste.DataFiles.Markdown](TableOfContents.WithoutHaste.DataFiles.Markdown.md).MarkdownTableRow

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object)  

Represents one row in a Markdown table.  

# Fields

## [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) Cells

List of the cells in the row.  

# Constructors

## MarkdownTableRow()

## MarkdownTableRow([List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) cells)

## MarkdownTableRow([String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string[]) cells)

# Methods

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) Add([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) cell)

Add a cell to the end of the row.  

**Parameters:**  
* **[string](https://docs.microsoft.com/en-us/dotnet/api/system.string) cell**:   

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) ToMarkdown([List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) columnWidths)

Return markdown-formatted text.  

**Remarks:**  
Column widths are padded an additional 1 space on left and right, per Markdown formatting.  

Line feed, new line, and tab characters are removed.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: ColumnWidths cannot be null or shorter than the row.  

