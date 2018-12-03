# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentGroup

**Inheritance:** object → [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md)  

Represents an ordered collection of comments.  

**Remarks:**  
Groups may include a link to something in the assembly which these comments are documenting.  

# Fields

## Comments

**[List&lt;DotNetComment&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

# Properties

## this[int index]

**[DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md) { public get; }**  

Access comments by zero-based index.  

## Count

**int { public get; }**  

The number of comments in the group. Does not count nested comments.  

## IsEmpty

**bool { public get; }**  

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

## Add([DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md) comment)

**void**  

## Add([DotNetComment[]](WithoutHaste.DataFiles.DotNet.DotNetComment.md) comments)

**void**  

## Add([List&lt;DotNetComment&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) comments)

**void**  

# Static Methods

## FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element)

**static DotNetCommentGroup**  

Parses .Net XML documentation for any "grouping" tag.  

**Example A:**  
`<summary>nested comments and/or plain text</summary>`  

**Example B:**  
`<remarks>nested comments and/or plain text</remarks>`  

**Example C:**  
`<example>nested comments and/or plain text</example>`  

**Example D:**  
`<para>nested comments and/or plain text</para>`  

**Example E:**  
`<returns>nested comments and/or plain text</returns>`  

**Example F:**  
`<value>nested comments and/or plain text</value>`  

# Derived By

[WithoutHaste.DataFiles.DotNet.DotNetCommentLinkedGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentLinkedGroup.md)  
Represents an ordered collection of comments that is linked to something in the assembly which it is documenting.  

