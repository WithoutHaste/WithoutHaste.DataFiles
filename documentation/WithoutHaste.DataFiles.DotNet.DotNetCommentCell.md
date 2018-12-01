# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentCell

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object)  

Represents a cell in a table in .Net XML documentation.  

**Remarks:**  
Does not inherit from DotNetCommentText because a cell cannot appear everywhere text can.  

# Fields

## const DotNetCommentCell EmptyCell

Default empty cell.  

# Properties

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) Text { get; protected set; }

Cell contents.  

# Constructors

## DotNetCommentCell([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text)

# Static Methods

## static DotNetCommentCell FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element)

Parses .Net XML documentation term tag.  

**Example A:**  
`<term>plain text</term>`  

