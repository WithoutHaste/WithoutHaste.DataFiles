# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentTypeParameter

**Inheritance:** object → [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md) → [DotNetCommentGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentGroup.md) → [DotNetCommentLinkedGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentLinkedGroup.md) → [DotNetCommentParameter](WithoutHaste.DataFiles.DotNet.DotNetCommentParameter.md)  

Represents a generic-type parameter description in the comments.  

# Constructors

## DotNetCommentTypeParameter([DotNetCommentTypeParameterLink](WithoutHaste.DataFiles.DotNet.DotNetCommentTypeParameterLink.md) link, [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md) comment)

## DotNetCommentTypeParameter([DotNetCommentTypeParameterLink](WithoutHaste.DataFiles.DotNet.DotNetCommentTypeParameterLink.md) link, [DotNetComment[]](WithoutHaste.DataFiles.DotNet.DotNetComment.md) comments)

## DotNetCommentTypeParameter([DotNetCommentTypeParameterLink](WithoutHaste.DataFiles.DotNet.DotNetCommentTypeParameterLink.md) link, [List&lt;DotNetComment&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) comments)

# Static Methods

## static DotNetCommentTypeParameter FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element)

Parses .Net XML documentation for typeparam.  

**Example A:**  
`<typeparam name="T">nested comments</typeparam>`  

