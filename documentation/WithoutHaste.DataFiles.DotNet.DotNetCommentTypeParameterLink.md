# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentTypeParameterLink

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md) → [DotNetCommentParameterLink](WithoutHaste.DataFiles.DotNet.DotNetCommentParameterLink.md)  
**Implements:** [IDotNetCommentLink](WithoutHaste.DataFiles.DotNet.IDotNetCommentLink.md)  

Represents a link in the comments to an internal generic-type parameter.  

# Examples

## Example A:

`<typeparamref name="T" />`  

# Constructors

## DotNetCommentTypeParameterLink([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) name)

## DotNetCommentTypeParameterLink([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) name, [CommentTag](WithoutHaste.DataFiles.DotNet.CommentTag.md) tag)

# Static Methods

## static DotNetCommentTypeParameterLink FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element)

Parses .Net XML documentation for typeparamref.  

