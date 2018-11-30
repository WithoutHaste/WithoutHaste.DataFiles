# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentList

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md)  

Represents a list in the comments.  

# Examples

## Example A:


```xml
<list type="bullet"> <!-- type can also be "number" -->
 <listheader>
  <term>Term</term>
  <description>Description</description>
 </listheader>
 <item>
  <term>Term</term>
  <description>Description</description>
 </item>
</list>
```  

# Fields

## [List&lt;DotNetCommentListItem&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) Items

Items in the list.  

# Properties

## [DotNetCommentListItem](WithoutHaste.DataFiles.DotNet.DotNetCommentListItem.md) this[[int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) index] { get; }

Access list items by 0-based index.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) IsNumbered { get; protected set; }

True for numbered lists (numbering starts at 1).  
False for bulleted lists.  

## [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) Length { get; }

Number of items in the list. Includes headers.  

# Constructors

## DotNetCommentList([List&lt;DotNetCommentListItem&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) items, [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) isNumbered = False)

# Static Methods

## static [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md) FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element)

Parses .Net XML documentation list (which may actually be a table).  

