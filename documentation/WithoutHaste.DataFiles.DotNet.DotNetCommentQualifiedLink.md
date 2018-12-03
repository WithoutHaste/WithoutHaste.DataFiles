# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentQualifiedLink

**Inheritance:** object → [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md)  
**Implements:** [IDotNetCommentLink](WithoutHaste.DataFiles.DotNet.IDotNetCommentLink.md)  

Represents a link in the comments to an internal or extenal type or type.method().  

# Properties

## FullName

**string { public get; }**  

Return the fully qualified name of the referenced assembly element.  

## Name

**[DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) { public get; protected set; }**  

Name of type or member.  

# Constructors

## DotNetCommentQualifiedLink([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

## DotNetCommentQualifiedLink([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name, [CommentTag](WithoutHaste.DataFiles.DotNet.CommentTag.md) tag)

# Methods

## Matches([DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md) member)

**bool**  

Returns true if link name matches the member name.  

# Static Methods

## FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element)

**static DotNetCommentQualifiedLink**  

Parses .Net XML documentation tag that contains attribute cref.  

**Example A:**  
`<exception cref="Namespace.ExceptionType">nested comments and/or plain text</exception>`  

**Example B:**  
`<permission cref="Namespace.Type">nested comments and/or plain text</permission>`  

## FromVisualStudioXml(string cref)

**static DotNetCommentQualifiedLink**  

Parses .Net XML documentation cref.  

# Derived By

[WithoutHaste.DataFiles.DotNet.DotNetCommentDuplicate](WithoutHaste.DataFiles.DotNet.DotNetCommentDuplicate.md)  
Represents a the `<duplicate cref="" />` tag, which means that documentation should be copied from the specified (cref) class, interface, struct, or member.  

[WithoutHaste.DataFiles.DotNet.DotNetCommentMethodLink](WithoutHaste.DataFiles.DotNet.DotNetCommentMethodLink.md)  
Represents a link in the comments to an internal or extenal method.  

