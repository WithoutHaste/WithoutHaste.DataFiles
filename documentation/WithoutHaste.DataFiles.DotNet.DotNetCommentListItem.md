# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentListItem

**Inheritance:** object  

Represents a listheader or item in a .Net XML documentation list.  

# Properties

## [DotNetCommentGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentGroup.md) Description { get; protected set; }

## bool IsHeader { get; protected set; }

## [DotNetCommentGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentGroup.md) Term { get; protected set; }

# Constructors

## DotNetCommentListItem()

## DotNetCommentListItem(string term, string description = null, bool isHeader = False)

Plain text _term_ and _description_.  

## DotNetCommentListItem([DotNetCommentGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentGroup.md) term, [DotNetCommentGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentGroup.md) description = null, bool isHeader = False)

_term_ and _description_ containing more than plain text, such as a `see` tag.  

# Static Methods

## static DotNetCommentListItem FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) element)

Parses .Net XML documentation listheader or item.  

**Example A:**  
Format options:
```xml
<listheader>
  plain text
</listheader>
<listheader>
  <term>Term</term>
</listheader>
<listheader>
  <description>Description</description>
</listheader>
<listheader>
  <term>Term</term>
  <description>Description</description>
</listheader>
```  

**Example B:**  
Format options:
```xml
<item>
  plain text
</item>
<item>
  <term>Term</term>
</item>
<item>
  <description>Description</description>
</item>
<item>
  <term>Term</term>
  <description>Description</description>
</item>
```  

