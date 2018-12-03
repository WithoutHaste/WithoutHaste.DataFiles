# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentCodeBlock

**Inheritance:** object → [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md) → [DotNetCommentText](WithoutHaste.DataFiles.DotNet.DotNetCommentText.md) → [DotNetCommentCode](WithoutHaste.DataFiles.DotNet.DotNetCommentCode.md)  

Represents a section of code in the comments.  

# Properties

## Language

**string { public get; protected set; }**  

Specify the language of the code block. Null if not known.  

# Constructors

## DotNetCommentCodeBlock(string text)

## DotNetCommentCodeBlock(string text, string language)

# Static Methods

## FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element)

**static [DotNetCommentCode](WithoutHaste.DataFiles.DotNet.DotNetCommentCode.md)**  

Parses .Net XML documentation code tag.  

**Example A:**  
`<code>code statements</code>`  

## FromVisualStudioXml([System.Xml.Linq.XCData](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xcdata) element)

**static [DotNetCommentCode](WithoutHaste.DataFiles.DotNet.DotNetCommentCode.md)**  

Parses .Net XML documentation CDATA tag.  

**Remarks:**  
Sets language to "xml".  

**Example A:**  
`<![CDATA[xml statements\]\]>`  

