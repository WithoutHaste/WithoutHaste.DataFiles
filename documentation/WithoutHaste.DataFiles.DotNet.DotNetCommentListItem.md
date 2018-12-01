# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetCommentListItem

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object)  

Represents a listheader or item in a .Net XML documentation list.  

# Properties

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) Description { get; protected set; }

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) IsHeader { get; protected set; }

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) Term { get; protected set; }

# Constructors

## DotNetCommentListItem()

## DotNetCommentListItem([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) term, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) description = null, [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) isHeader = False)

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

