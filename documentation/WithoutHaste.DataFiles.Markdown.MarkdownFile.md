# [WithoutHaste.DataFiles.Markdown](TableOfContents.WithoutHaste.DataFiles.Markdown.md).MarkdownFile

**Inheritance:** object  

Represents a markdown file.  

# Fields

## Extensions

**const [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.array)**  

Accepted markdown file extensions.  

# Constructors

## MarkdownFile()

Create an empty markdown file.  

# Methods

## AddSection(string header)

**[MarkdownSection](WithoutHaste.DataFiles.Markdown.MarkdownSection.md)**  

Creates new section and adds it to the end of the file. Defaults to depth 1.  

**Returns:**  
The new section  

**Parameters:**  
* **string header**: Section header  

## AddSection([MarkdownSection](WithoutHaste.DataFiles.Markdown.MarkdownSection.md) section)

**void**  

Adds existing section to the end of this file. Depths are not updated.  

**Parameters:**  
* **[MarkdownSection](WithoutHaste.DataFiles.Markdown.MarkdownSection.md) section**: Existing section.  

## ToMarkdown()

**string**  

Returns full markdown text for file, formatted for legibility.  

