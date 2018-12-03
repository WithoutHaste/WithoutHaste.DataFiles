# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentList

**Inheritance:** object → [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md)  

Represents a list in the comments.  

# Fields

## Items

**[List&lt;DotNetCommentListItem&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

Items in the list.  

# Properties

## this[int index]

**[DotNetCommentListItem](WithoutHaste.DataFiles.DotNet.DotNetCommentListItem.md) { public get; }**  

Access list items by 0-based index.  

**Exceptions:**  
* **[IndexOutOfRangeException](https://docs.microsoft.com/en-us/dotnet/api/system.indexoutofrangeexception)**:   

## IsNumbered

**bool { public get; protected set; }**  

True for numbered lists.  
False for bulleted lists.  

## Length

**int { public get; }**  

Number of items in the list. Includes headers.  

# Constructors

## DotNetCommentList([List&lt;DotNetCommentListItem&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) items, bool isNumbered = False)

# Static Methods

## FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element)

**static [DotNetComment](WithoutHaste.DataFiles.DotNet.DotNetComment.md)**  

Parses .Net XML documentation list (which may actually be a table).  

**Example A:**  

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

