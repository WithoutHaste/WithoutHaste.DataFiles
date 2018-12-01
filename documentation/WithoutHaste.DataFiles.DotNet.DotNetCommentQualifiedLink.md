# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentQualifiedLink

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md)  
**Implements:** [IDotNetCommentLink](WithoutHaste.DataFiles.DotNet.IDotNetCommentLink.md)  

Represents a link in the comments to an internal or extenal type or type.method().  

# Properties

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) FullName { get; }

Return the fully qualified name of the referenced assembly element.  

## [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) Name { get; protected set; }

Name of type or member.  

# Constructors

## DotNetCommentQualifiedLink([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

## DotNetCommentQualifiedLink([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name, [CommentTag](WithoutHaste.DataFiles.DotNet.CommentTag.md) tag)

# Methods

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) Matches([DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md) member)

Returns true if link name matches the member name.  

# Static Methods

## static DotNetCommentQualifiedLink FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element)

Parses .Net XML documentation tag that contains attribute cref.  

**Example A:**  
`<exception cref="Namespace.ExceptionType">nested comments and/or plain text</exception>`  

**Example B:**  
`<permission cref="Namespace.Type">nested comments and/or plain text</permission>`  

## static DotNetCommentQualifiedLink FromVisualStudioXml([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) cref)

Parses .Net XML documentation cref.  

# Derived By

[WithoutHaste.DataFiles.DotNet.DotNetCommentDuplicate](WithoutHaste.DataFiles.DotNet.DotNetCommentDuplicate.md)  
Represents a the `<duplicate cref="" />` tag, which means that documentation should be copied from the specified (cref) class, interface, struct, or member.  

[WithoutHaste.DataFiles.DotNet.DotNetCommentMethodLink](WithoutHaste.DataFiles.DotNet.DotNetCommentMethodLink.md)  
Represents a link in the comments to an internal or extenal method.  

