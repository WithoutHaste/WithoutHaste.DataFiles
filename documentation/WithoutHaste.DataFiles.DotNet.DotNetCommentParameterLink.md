# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentParameterLink

**Inheritance:** object → [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md)  
**Implements:** [IDotNetCommentLink](WithoutHaste.DataFiles.DotNet.IDotNetCommentLink.md)  

Represents a link in the comments to an internal parameter name.  

# Properties

## string FullName { get; }

Return the fully qualified name of the referenced assembly element.  

## string Name { get; protected set; }

Name of the parameter in local method.  

# Constructors

## DotNetCommentParameterLink(string name)

## DotNetCommentParameterLink(string name, [CommentTag](WithoutHaste.DataFiles.DotNet.CommentTag.md) tag)

# Methods

## virtual string ToString()

Name  

# Static Methods

## static DotNetCommentParameterLink FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element)

Parses .Net XML documentation for paramref.  

**Example A:**  
`<paramref name="paramName" />`  

# Derived By

[WithoutHaste.DataFiles.DotNet.DotNetCommentTypeParameterLink](WithoutHaste.DataFiles.DotNet.DotNetCommentTypeParameterLink.md)  
Represents a link in the comments to an internal generic-type parameter.  

