# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentCell

**Inheritance:** object  

Represents a cell in a table in .Net XML documentation.  

**Remarks:**  
Does not inherit from DotNetCommentText because a cell cannot appear everywhere text can.  

# Fields

## EmptyCell

**const DotNetCommentCell**  

Default empty cell.  

# Properties

## Text

**string { public get; protected set; }**  

Cell contents.  

# Constructors

## DotNetCommentCell(string text)

# Static Methods

## FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element)

**static DotNetCommentCell**  

Parses .Net XML documentation term tag.  

**Example A:**  
`<term>plain text</term>`  

