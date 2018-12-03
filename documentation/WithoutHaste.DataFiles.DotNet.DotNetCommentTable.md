# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentTable

**Inheritance:** object → [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md)  

Represents a table in the comments.  

# Fields

## Rows

**[List&lt;DotNetCommentRow&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

# Properties

## this[int rowIndex,int columnIndex]

**[DotNetCommentCell](WithoutHaste.DataFiles.DotNet.DotNetCommentCell.md) { public get; }**  

Returns the selected [DotNetCommentCell](WithoutHaste.DataFiles.DotNet.DotNetCommentCell.md) of the table. Will return a [DotNetCommentCell.EmptyCell](WithoutHaste.DataFiles.DotNet.DotNetCommentCell.md) if the cell is within range but does not actually exist.  

**Exceptions:**  
* **[IndexOutOfRangeException](https://docs.microsoft.com/en-us/dotnet/api/system.indexoutofrangeexception)**: Either the row or column index is out of range.  

**Parameters:**  
* **int rowIndex**: 0-based index of table row.  
* **int columnIndex**: 0-based index of table column.  

## ColumnCount

**int { public get; }**  

Maximum number of columns in the table.  

## DataRowCount

**int { public get; }**  

Number of data (non-header) rows in the table.  

## HeaderRowCount

**int { public get; }**  

Number of header rows in the table.  

## RowCount

**int { public get; }**  

Number of rows in the table. Includes header rows and data rows.  

# Constructors

## DotNetCommentTable([List&lt;DotNetCommentRow&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) rows)

# Static Methods

## FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element)

**static DotNetCommentTable**  

Parses .Net XML documentation table.  

**Example A:**  

```xml
<list type="table">
 <listheader>
  <term>Column 1</term>
  <term>Column 2</term>
  <term>Column 3</term>
 </listheader>
 <item>
  <term>Row 1, Cell 1</term>
  <term>Row 1, Cell 2</term>
  <term>Row 1, Cell 3</term>
 </item>
</list>
```  

