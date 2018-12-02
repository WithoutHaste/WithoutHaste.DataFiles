# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentTable

**Inheritance:** object → [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md)  

Represents a table in the comments.  

# Fields

## [List&lt;DotNetCommentRow&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) Rows

# Properties

## [DotNetCommentCell](WithoutHaste.DataFiles.DotNet.DotNetCommentCell.md) this[int rowIndex,int columnIndex] { get; }

Returns the selected [DotNetCommentCell](WithoutHaste.DataFiles.DotNet.DotNetCommentCell.md) of the table. Will return a [DotNetCommentCell.EmptyCell](WithoutHaste.DataFiles.DotNet.DotNetCommentCell.md) if the cell is within range but does not actually exist.  

**Exceptions:**  
* **[IndexOutOfRangeException](https://docs.microsoft.com/en-us/dotnet/api/system.indexoutofrangeexception)**: Either the row or column index is out of range.  

**Parameters:**  
* **int rowIndex**: 0-based index of table row.  
* **int columnIndex**: 0-based index of table column.  

## int ColumnCount { get; }

Maximum number of columns in the table.  

## int DataRowCount { get; }

Number of data (non-header) rows in the table.  

## int HeaderRowCount { get; }

Number of header rows in the table.  

## int RowCount { get; }

Number of rows in the table. Includes header rows and data rows.  

# Constructors

## DotNetCommentTable([List&lt;DotNetCommentRow&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) rows)

# Static Methods

## static DotNetCommentTable FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element)

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

