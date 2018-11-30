# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentRow

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object)  

Represents a row of data in a .Net XML documentation table.  

# Examples

## Example A:

`<listheader><term>Header 1</term><term>Header 2</term></listheader>`  

## Example B:

`<item><term>Cell 1</term><term>Cell 2</term></item>`  

# Fields

## [List&lt;DotNetCommentCell&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) Cells

# Properties

## [DotNetCommentCell](WithoutHaste.DataFiles.DotNet.DotNetCommentCell.md) this[[int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) columnIndex] { get; }

Returns the selected cell of the row. Returns an empty cell if no cell is found.  

**Remarks:**  
Returns an empty cell because Row does not know the number of columns in the Table, just how many cells are filled on this row.  

## [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) ColumnCount { get; }

Number of columns (cells) in the row.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) IsHeader { get; protected set; }

# Constructors

## DotNetCommentRow([List&lt;DotNetCommentCell&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) cells, [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) isHeader = False)

# Static Methods

## static DotNetCommentRow FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element)

Parses .Net XML documentation "listheader" or "item", expecting one "term" per cell.  

