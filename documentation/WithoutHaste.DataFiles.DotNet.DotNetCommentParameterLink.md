# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentParameterLink

**Inheritance:** object → [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md)  
**Implements:** [IDotNetCommentLink](WithoutHaste.DataFiles.DotNet.IDotNetCommentLink.md)  

Represents a link in the comments to an internal parameter name.  

# Properties

## FullName

**string { public get; }**  

Return the fully qualified name of the referenced assembly element.  

## Name

**string { public get; protected set; }**  

Name of the parameter in local method.  

# Constructors

## DotNetCommentParameterLink(string name)

## DotNetCommentParameterLink(string name, [CommentTag](WithoutHaste.DataFiles.DotNet.CommentTag.md) tag)

# Methods

## ToString()

**virtual string**  

Name  

# Static Methods

## FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element)

**static DotNetCommentParameterLink**  

Parses .Net XML documentation for paramref.  

**Example A:**  
`<paramref name="paramName" />`  

# Derived By

[WithoutHaste.DataFiles.DotNet.DotNetCommentTypeParameterLink](WithoutHaste.DataFiles.DotNet.DotNetCommentTypeParameterLink.md)  
Represents a link in the comments to an internal generic-type parameter.  

