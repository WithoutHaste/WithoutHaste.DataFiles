# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetComment

**Abstract**  
**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object)  

Represents a section of documentation, such as the contents of a `<summary></summary>` tag.  

# Properties

## [CommentTag](WithoutHaste.DataFiles.DotNet.CommentTag.md) Tag { get; protected set; }

The type of xml tag that the comment came from.  

# Methods

## virtual [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) ToString()

Defaults to the CommentTag text.  

# Static Methods

## static DotNetComment FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element)

Parses top-level .Net XML documentation comments. Returns null if no comments are found.  

## static [DotNetCommentText](WithoutHaste.DataFiles.DotNet.DotNetCommentText.md) FromVisualStudioXml([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text)

Creates a plain text comment.  

## static [CommentTag](WithoutHaste.DataFiles.DotNet.CommentTag.md) GetTag([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element)

Returns the CommentTag value that corresponds to the XElement.  

## static [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) IsXmlTag([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) localName)

Returns false on unexpected xml formats.  

## static [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) IsXmlTag([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element, [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.array) localNames)

Returns false on unexpected xml formats.  

## static [List&lt;DotNetComment&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) ParseSection([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element)

Parses inner .Net XML documentation comments.  

## static [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) ValidateXmlTag([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) localName)

Throws exception on unexpected xml formats.  

**Exceptions:**  
* **[XmlFormatException](WithoutHaste.DataFiles.XmlFormatException.md)**: XML tag does not have the expected local name, or is null  

## static [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) ValidateXmlTag([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element, [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.array) localNames)

Throws exception on unexpected xml formats.  

**Exceptions:**  
* **[XmlFormatException](WithoutHaste.DataFiles.XmlFormatException.md)**: XML tag does not have any of the expected local names, or is null  

# Derived By

[WithoutHaste.DataFiles.DotNet.DotNetCommentGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentGroup.md)  
Represents an ordered collection of comments.  

[WithoutHaste.DataFiles.DotNet.DotNetCommentInherit](WithoutHaste.DataFiles.DotNet.DotNetCommentInherit.md)  
Represents a the `<inheritdoc />` tag, which means that documentation should be inherited for the base class, interface, struct, or member.  

[WithoutHaste.DataFiles.DotNet.DotNetCommentList](WithoutHaste.DataFiles.DotNet.DotNetCommentList.md)  
Represents a list in the comments.  

[WithoutHaste.DataFiles.DotNet.DotNetCommentParameterLink](WithoutHaste.DataFiles.DotNet.DotNetCommentParameterLink.md)  
Represents a link in the comments to an internal parameter name.  

[WithoutHaste.DataFiles.DotNet.DotNetCommentQualifiedLink](WithoutHaste.DataFiles.DotNet.DotNetCommentQualifiedLink.md)  
Represents a link in the comments to an internal or extenal type or type.method().  

[WithoutHaste.DataFiles.DotNet.DotNetCommentTable](WithoutHaste.DataFiles.DotNet.DotNetCommentTable.md)  
Represents a table in the comments.  

[WithoutHaste.DataFiles.DotNet.DotNetCommentText](WithoutHaste.DataFiles.DotNet.DotNetCommentText.md)  
Represents a plain text segment of comments.  

