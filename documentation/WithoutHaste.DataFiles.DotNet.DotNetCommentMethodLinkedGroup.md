# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentMethodLinkedGroup

**Inheritance:** object → [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md) → [DotNetCommentGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentGroup.md) → [DotNetCommentLinkedGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentLinkedGroup.md) → [DotNetCommentQualifiedLinkedGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentQualifiedLinkedGroup.md)  

Represents a section of comments that is linked to a method.  

# Examples

## Example A:

`<permission cref="Namespace.Type.Method()">nested comments</permission>`  

# Properties

## MethodLink

**[DotNetCommentMethodLink](WithoutHaste.DataFiles.DotNet.DotNetCommentMethodLink.md) { public get; }**  

Strongly-typed link.  

# Constructors

## DotNetCommentMethodLinkedGroup([DotNetCommentMethodLink](WithoutHaste.DataFiles.DotNet.DotNetCommentMethodLink.md) link, [CommentTag](WithoutHaste.DataFiles.DotNet.CommentTag.md) tag, [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md) comment)

## DotNetCommentMethodLinkedGroup([DotNetCommentMethodLink](WithoutHaste.DataFiles.DotNet.DotNetCommentMethodLink.md) link, [CommentTag](WithoutHaste.DataFiles.DotNet.CommentTag.md) tag, [DotNetComment[]](WithoutHaste.DataFiles.DotNet.DotNetComment.md) comments)

## DotNetCommentMethodLinkedGroup([DotNetCommentMethodLink](WithoutHaste.DataFiles.DotNet.DotNetCommentMethodLink.md) link, [CommentTag](WithoutHaste.DataFiles.DotNet.CommentTag.md) tag, [List&lt;DotNetComment&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) comments)

