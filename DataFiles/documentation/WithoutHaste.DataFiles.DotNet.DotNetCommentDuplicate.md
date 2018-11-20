# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentDuplicate

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md) → [DotNetCommentQualifiedLink](WithoutHaste.DataFiles.DotNet.DotNetCommentQualifiedLink.md)  
**Implements:** [IDotNetCommentLink](WithoutHaste.DataFiles.DotNet.IDotNetCommentLink.md)  

Represents a the `<duplicate cref="" />` tag, which means that documentation should be copied from the specified (cref) class, interface, struct, or member.  

**Remarks:**  
This is just a marker class. If the cref can be resolved, the duplicated comments will automatically replace this comment.  

# Constructors

## DotNetCommentDuplicate([WithoutHaste.DataFiles.DotNet.DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

## DotNetCommentDuplicate([WithoutHaste.DataFiles.DotNet.DotNetCommentQualifiedLink](WithoutHaste.DataFiles.DotNet.DotNetCommentQualifiedLink.md) link)

