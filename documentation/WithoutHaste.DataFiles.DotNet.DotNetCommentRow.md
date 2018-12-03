# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentRow

**Inheritance:** object  

Represents a row of data in a .Net XML documentation table.  

# Fields

## Cells

**[List&lt;DotNetCommentCell&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

# Properties

## this[int columnIndex]

**[DotNetCommentCell](WithoutHaste.DataFiles.DotNet.DotNetCommentCell.md) { public get; }**  

Returns the selected cell of the row. Returns an empty cell if no cell is found.  

**Remarks:**  
Returns an empty cell because Row does not know the number of columns in the Table, just how many cells are filled on this row.  

**Exceptions:**  
* **[IndexOutOfRangeException](https://docs.microsoft.com/en-us/dotnet/api/system.indexoutofrangeexception)**: Column index is negative.  

**Parameters:**  
* **int columnIndex**: 0-based index of table column.  

## ColumnCount

**int { public get; }**  

Number of columns (cells) in the row.  

## IsHeader

**bool { public get; protected set; }**  

# Constructors

## DotNetCommentRow([List&lt;DotNetCommentCell&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) cells, bool isHeader = False)

# Static Methods

## FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element)

**static DotNetCommentRow**  

Parses .Net XML documentation "listheader" or "item", expecting one "term" per cell.  

**Example A:**  
`<listheader><term>Header 1</term><term>Header 2</term></listheader>`  

**Example B:**  
`<item><term>Cell 1</term><term>Cell 2</term></item>`  

