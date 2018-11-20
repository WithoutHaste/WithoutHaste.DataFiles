# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentParameter

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md) → [DotNetCommentGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentGroup.md) → [DotNetCommentLinkedGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentLinkedGroup.md)  

Represents a parameter description in the comments.  

# Examples

## Example A:

`<param name="myParam">nested comments</param>`  

# Properties

## [WithoutHaste.DataFiles.DotNet.DotNetCommentParameterLink](WithoutHaste.DataFiles.DotNet.DotNetCommentParameterLink.md) ParameterLink { get; }

Strongly-typed link.  

# Constructors

## DotNetCommentParameter([WithoutHaste.DataFiles.DotNet.DotNetCommentParameterLink](WithoutHaste.DataFiles.DotNet.DotNetCommentParameterLink.md) link, [WithoutHaste.DataFiles.DotNet.DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md) comment)

## DotNetCommentParameter([WithoutHaste.DataFiles.DotNet.DotNetCommentParameterLink](WithoutHaste.DataFiles.DotNet.DotNetCommentParameterLink.md) link, WithoutHaste.DataFiles.DotNet.DotNetComment[] comments)

## DotNetCommentParameter([WithoutHaste.DataFiles.DotNet.DotNetCommentParameterLink](WithoutHaste.DataFiles.DotNet.DotNetCommentParameterLink.md) link, [List&lt;WithoutHaste.DataFiles.DotNet.DotNetComment&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) comments)

# Static Methods

## static DotNetCommentParameter FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element)

Parses .Net XML documentation for param.  

