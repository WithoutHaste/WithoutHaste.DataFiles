# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentCodeBlock

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md) → [DotNetCommentText](WithoutHaste.DataFiles.DotNet.DotNetCommentText.md) → [DotNetCommentCode](WithoutHaste.DataFiles.DotNet.DotNetCommentCode.md)  

Represents a section of code in the comments.  

# Properties

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) Language { get; protected set; }

Specify the language of the code block. Null if not known.  

# Constructors

## DotNetCommentCodeBlock([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text)

## DotNetCommentCodeBlock([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) language)

# Static Methods

## static [DotNetCommentCode](WithoutHaste.DataFiles.DotNet.DotNetCommentCode.md) FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element)

Parses .Net XML documentation code tag.  

**Example A:**  
`<code>code statements</code>`  

## static [DotNetCommentCode](WithoutHaste.DataFiles.DotNet.DotNetCommentCode.md) FromVisualStudioXml([System.Xml.Linq.XCData](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xcdata) element)

Parses .Net XML documentation CDATA tag.  

**Remarks:**  
Sets language to "xml".  

**Example A:**  
`<![CDATA[xml statements\]\]>`  

