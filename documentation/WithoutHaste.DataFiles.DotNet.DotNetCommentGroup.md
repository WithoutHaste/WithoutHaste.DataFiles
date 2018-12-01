# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentGroup

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md)  

Represents an ordered collection of comments.  

**Remarks:**  
Groups may include a link to something in the assembly which these comments are documenting.  

# Examples

## Example A:

`<summary>nested comments and/or plain text</summary>`  

## Example B:

`<remarks>nested comments and/or plain text</remarks>`  

## Example C:

`<example>nested comments and/or plain text</example>`  

## Example D:

`<para>nested comments and/or plain text</para>`  

## Example E:

`<returns>nested comments and/or plain text</returns>`  

## Example F:

`<value>nested comments and/or plain text</value>`  

# Fields

## [List&lt;DotNetComment&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) Comments

# Properties

## [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md) this[[int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) index] { get; }

Access comments by zero-based index.  

## [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) Count { get; }

The number of comments in the group. Does not count nested comments.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) IsEmpty { get; }

# Constructors

## DotNetCommentGroup()

Empty constructor  

## DotNetCommentGroup([CommentTag](WithoutHaste.DataFiles.DotNet.CommentTag.md) tag)

## DotNetCommentGroup([DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md) comment)

## DotNetCommentGroup([CommentTag](WithoutHaste.DataFiles.DotNet.CommentTag.md) tag, [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md) comment)

## DotNetCommentGroup([DotNetComment[]](WithoutHaste.DataFiles.DotNet.DotNetComment.md) comments)

## DotNetCommentGroup([CommentTag](WithoutHaste.DataFiles.DotNet.CommentTag.md) tag, [DotNetComment[]](WithoutHaste.DataFiles.DotNet.DotNetComment.md) comments)

## DotNetCommentGroup([List&lt;DotNetComment&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) comments)

## DotNetCommentGroup([CommentTag](WithoutHaste.DataFiles.DotNet.CommentTag.md) tag, [List&lt;DotNetComment&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) comments)

# Methods

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) Add([DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md) comment)

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) Add([DotNetComment[]](WithoutHaste.DataFiles.DotNet.DotNetComment.md) comments)

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) Add([List&lt;DotNetComment&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) comments)

# Static Methods

## static DotNetCommentGroup FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element)

Parses .Net XML documentation for any "grouping" tag.  

# Derived By

[WithoutHaste.DataFiles.DotNet.DotNetCommentLinkedGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentLinkedGroup.md)  
Represents an ordered collection of comments that is linked to something in the assembly which it is documenting.  

