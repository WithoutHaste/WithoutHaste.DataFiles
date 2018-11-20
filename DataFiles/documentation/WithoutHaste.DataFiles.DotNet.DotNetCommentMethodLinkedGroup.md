# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentMethodLinkedGroup

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md) → [DotNetCommentGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentGroup.md) → [DotNetCommentLinkedGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentLinkedGroup.md) → [DotNetCommentQualifiedLinkedGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentQualifiedLinkedGroup.md)  

Represents a section of comments that is linked to a fully qualified type or member.  

# Examples

## Example A:

`<permission cref="Namespace.Type.Member">nested comments</permission>`  

## Example B:

`<exception cref="Namespace.ExceptionType">nested comments</exception>`  

# Properties

## [WithoutHaste.DataFiles.DotNet.DotNetCommentMethodLink](WithoutHaste.DataFiles.DotNet.DotNetCommentMethodLink.md) MethodLink { get; }

Strongly-typed link.  

# Constructors

## DotNetCommentMethodLinkedGroup([WithoutHaste.DataFiles.DotNet.DotNetCommentMethodLink](WithoutHaste.DataFiles.DotNet.DotNetCommentMethodLink.md) link, [WithoutHaste.DataFiles.DotNet.CommentTag](WithoutHaste.DataFiles.DotNet.CommentTag.md) tag, [WithoutHaste.DataFiles.DotNet.DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md) comment)

## DotNetCommentMethodLinkedGroup([WithoutHaste.DataFiles.DotNet.DotNetCommentMethodLink](WithoutHaste.DataFiles.DotNet.DotNetCommentMethodLink.md) link, [WithoutHaste.DataFiles.DotNet.CommentTag](WithoutHaste.DataFiles.DotNet.CommentTag.md) tag, WithoutHaste.DataFiles.DotNet.DotNetComment[] comments)

## DotNetCommentMethodLinkedGroup([WithoutHaste.DataFiles.DotNet.DotNetCommentMethodLink](WithoutHaste.DataFiles.DotNet.DotNetCommentMethodLink.md) link, [WithoutHaste.DataFiles.DotNet.CommentTag](WithoutHaste.DataFiles.DotNet.CommentTag.md) tag, [List&lt;WithoutHaste.DataFiles.DotNet.DotNetComment&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) comments)

