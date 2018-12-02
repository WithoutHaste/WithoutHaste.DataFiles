# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentParameter

**Inheritance:** object → [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md) → [DotNetCommentGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentGroup.md) → [DotNetCommentLinkedGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentLinkedGroup.md)  

Represents a parameter description in the comments.  

# Properties

## [DotNetCommentParameterLink](WithoutHaste.DataFiles.DotNet.DotNetCommentParameterLink.md) ParameterLink { get; }

Strongly-typed link.  

# Constructors

## DotNetCommentParameter([DotNetCommentParameterLink](WithoutHaste.DataFiles.DotNet.DotNetCommentParameterLink.md) link, [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md) comment)

## DotNetCommentParameter([DotNetCommentParameterLink](WithoutHaste.DataFiles.DotNet.DotNetCommentParameterLink.md) link, [DotNetComment[]](WithoutHaste.DataFiles.DotNet.DotNetComment.md) comments)

## DotNetCommentParameter([DotNetCommentParameterLink](WithoutHaste.DataFiles.DotNet.DotNetCommentParameterLink.md) link, [List&lt;DotNetComment&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) comments)

# Static Methods

## static DotNetCommentParameter FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element)

Parses .Net XML documentation for param.  

**Example A:**  
`<param name="myParam">nested comments</param>`  

# Derived By

[WithoutHaste.DataFiles.DotNet.DotNetCommentTypeParameter](WithoutHaste.DataFiles.DotNet.DotNetCommentTypeParameter.md)  
Represents a generic-type parameter description in the comments.  

