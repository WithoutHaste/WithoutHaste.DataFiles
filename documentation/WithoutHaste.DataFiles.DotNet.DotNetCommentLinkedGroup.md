# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentLinkedGroup

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md) → [DotNetCommentGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentGroup.md)  

Represents an ordered collection of comments that is linked to something in the assembly which it is documenting.  

# Properties

## [IDotNetCommentLink](WithoutHaste.DataFiles.DotNet.IDotNetCommentLink.md) Link { get; protected set; }

Reference link from comments to something in the assembly.  

# Constructors

## DotNetCommentLinkedGroup()

Empty constructor  

## DotNetCommentLinkedGroup([IDotNetCommentLink](WithoutHaste.DataFiles.DotNet.IDotNetCommentLink.md) link, [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md) comment)

## DotNetCommentLinkedGroup([IDotNetCommentLink](WithoutHaste.DataFiles.DotNet.IDotNetCommentLink.md) link, [DotNetComment[]](WithoutHaste.DataFiles.DotNet.DotNetComment.md) comments)

## DotNetCommentLinkedGroup([IDotNetCommentLink](WithoutHaste.DataFiles.DotNet.IDotNetCommentLink.md) link, [List&lt;DotNetComment&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) comments)

# Derived By

[WithoutHaste.DataFiles.DotNet.DotNetCommentParameter](WithoutHaste.DataFiles.DotNet.DotNetCommentParameter.md)  
Represents a parameter description in the comments.  

[WithoutHaste.DataFiles.DotNet.DotNetCommentQualifiedLinkedGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentQualifiedLinkedGroup.md)  
Represents a section of comments that is linked to a fully qualified type or member.  

