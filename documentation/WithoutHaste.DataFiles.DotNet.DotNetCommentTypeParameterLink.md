# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentTypeParameterLink

**Inheritance:** object → [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md) → [DotNetCommentParameterLink](WithoutHaste.DataFiles.DotNet.DotNetCommentParameterLink.md)  
**Implements:** [IDotNetCommentLink](WithoutHaste.DataFiles.DotNet.IDotNetCommentLink.md)  

Represents a link in the comments to an internal generic-type parameter.  

# Constructors

## DotNetCommentTypeParameterLink(string name)

## DotNetCommentTypeParameterLink(string name, [CommentTag](WithoutHaste.DataFiles.DotNet.CommentTag.md) tag)

# Static Methods

## FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element)

**static DotNetCommentTypeParameterLink**  

Parses .Net XML documentation for typeparamref.  

**Example A:**  
`<typeparamref name="T" />`  

