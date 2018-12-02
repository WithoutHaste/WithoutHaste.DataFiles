# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentCode

**Inheritance:** object → [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md) → [DotNetCommentText](WithoutHaste.DataFiles.DotNet.DotNetCommentText.md)  

Represents an inline section of code in the comments.  

# Constructors

## DotNetCommentCode(string text)

# Static Methods

## static DotNetCommentCode FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element)

Parses .Net XML documentation c tag.  

**Example A:**  
`<c>code fragment</c>`  

## static DotNetCommentCode FromVisualStudioXml([System.Xml.Linq.XCData](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xcdata) element)

Parses .Net XML documentation CDATA tag.  

# Derived By

[WithoutHaste.DataFiles.DotNet.DotNetCommentCodeBlock](WithoutHaste.DataFiles.DotNet.DotNetCommentCodeBlock.md)  
Represents a section of code in the comments.  

