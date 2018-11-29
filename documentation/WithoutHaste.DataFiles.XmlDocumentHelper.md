# [WithoutHaste.DataFiles](TableOfContents.WithoutHaste.DataFiles.md).XmlDocumentHelper

**Static**  
**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object)  

Generic System.Xml.XmlDocument utilities.  

# Static Methods

## static [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) Validate([System.Xml.XmlNode](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlnode) node, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) localName)

Throw exception if XmlNode does not exist, or does not have the expected LocalName.  

**Exceptions:**  
* **[XmlNodeException](WithoutHaste.DataFiles.XmlNodeException.md)**:   

## static [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) XmlToString([System.Xml.XmlDocument](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmldocument) xmlDocument)

Returns a string containing the entire contents of the XmlDocument.  

## static [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) XmlToString([System.Xml.XmlNode](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlnode) xmlNode)

Returns a string containing the entire contents of the XmlNode.  

