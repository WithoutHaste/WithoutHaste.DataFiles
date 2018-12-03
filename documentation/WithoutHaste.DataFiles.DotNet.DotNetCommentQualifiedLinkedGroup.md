# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentQualifiedLinkedGroup

**Inheritance:** object → [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md) → [DotNetCommentGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentGroup.md) → [DotNetCommentLinkedGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentLinkedGroup.md)  

Represents a section of comments that is linked to a fully qualified type or member.  

# Properties

## QualifiedLink

**[DotNetCommentQualifiedLink](WithoutHaste.DataFiles.DotNet.DotNetCommentQualifiedLink.md) { public get; }**  

Strongly-typed link.  

# Constructors

## DotNetCommentQualifiedLinkedGroup([DotNetCommentQualifiedLink](WithoutHaste.DataFiles.DotNet.DotNetCommentQualifiedLink.md) link, [CommentTag](WithoutHaste.DataFiles.DotNet.CommentTag.md) tag, [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md) comment)

## DotNetCommentQualifiedLinkedGroup([DotNetCommentQualifiedLink](WithoutHaste.DataFiles.DotNet.DotNetCommentQualifiedLink.md) link, [CommentTag](WithoutHaste.DataFiles.DotNet.CommentTag.md) tag, [DotNetComment[]](WithoutHaste.DataFiles.DotNet.DotNetComment.md) comments)

## DotNetCommentQualifiedLinkedGroup([DotNetCommentQualifiedLink](WithoutHaste.DataFiles.DotNet.DotNetCommentQualifiedLink.md) link, [CommentTag](WithoutHaste.DataFiles.DotNet.CommentTag.md) tag, [List&lt;DotNetComment&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) comments)

# Methods

## Matches([DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md) member)

**bool**  

Returns true if link name matches the member name.  

# Static Methods

## FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element)

**static DotNetCommentQualifiedLinkedGroup**  

Parses .Net XML documentation for permission or exception.  

**Example A:**  
`<permission cref="Namespace.Type.Member">nested comments</permission>`  

**Example B:**  
`<exception cref="Namespace.ExceptionType">nested comments</exception>`  

# Derived By

[WithoutHaste.DataFiles.DotNet.DotNetCommentMethodLinkedGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentMethodLinkedGroup.md)  
Represents a section of comments that is linked to a method.  

