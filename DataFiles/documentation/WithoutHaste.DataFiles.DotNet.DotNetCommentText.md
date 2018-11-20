# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentText

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md)  

Represents a plain text segment of comments.  

**Remarks:**  
Endline characters (\n) are preserved.  
  
A multiline block of text will have leading white-space removed as a block.  
So if each line starts with two tabs, two tabs will be removed from the beginning of each line.  

# Properties

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) IsEmpty { get; }

True if text is null or empty string.  

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) Text { get; protected set; }

# Constructors

## DotNetCommentText([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text)

# Methods

## virtual [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) ToString()

Returns full text.  

