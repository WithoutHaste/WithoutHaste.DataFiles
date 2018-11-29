# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentTable

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md)  

Represents a table in the comments.  

# Examples

## Example A:


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

# Fields

## [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) Rows

# Properties

## [WithoutHaste.DataFiles.DotNet.DotNetCommentCell](WithoutHaste.DataFiles.DotNet.DotNetCommentCell.md) this[[int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) rowIndex,[int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) columnIndex] { get; }

Returns the selected [WithoutHaste.DataFiles.DotNet.DotNetCommentCell](WithoutHaste.DataFiles.DotNet.DotNetCommentCell.md) of the table. Will return an empty [WithoutHaste.DataFiles.DotNet.DotNetCommentCell](WithoutHaste.DataFiles.DotNet.DotNetCommentCell.md) if the cell within range but does not actually exist.  

## [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) ColumnCount { get; }

Maximum number of columns in the table.  

## [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) DataRowCount { get; }

Number of data (non-header) rows in the table.  

## [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) HeaderRowCount { get; }

Number of header rows in the table.  

## [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) RowCount { get; }

Number of rows in the table. Includes header rows and normal rows.  

# Constructors

## DotNetCommentTable([List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) rows)

# Static Methods

## static DotNetCommentTable FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element)

Parses .Net XML documentation table.  

