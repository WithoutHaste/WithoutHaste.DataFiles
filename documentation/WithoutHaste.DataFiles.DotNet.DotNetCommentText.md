# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentText

**Inheritance:** object → [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md)  

Represents a plain text segment of comments.  

**Remarks:**  
Endline characters (\n) are preserved.  
  
A multiline block of text will have leading white-space removed as a block.  
So if each line starts with two tabs, two tabs will be removed from the beginning of each line.  

# Properties

## IsEmpty

**bool { public get; }**  

True if text is null or empty string.  

## Text

**string { public get; protected set; }**  

# Constructors

## DotNetCommentText(string text)

# Methods

## ToString()

**virtual string**  

Returns full text.  

# Derived By

[WithoutHaste.DataFiles.DotNet.DotNetCommentCode](WithoutHaste.DataFiles.DotNet.DotNetCommentCode.md)  
Represents an inline section of code in the comments.  

