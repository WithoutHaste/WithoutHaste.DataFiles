# [WithoutHaste.DataFiles](TableOfContents.WithoutHaste.DataFiles.md).XmlDocumentHelper

**Static**  
**Inheritance:** object  

Generic System.Xml.XmlDocument utilities.  

# Static Methods

## Validate([System.Xml.XmlNode](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlnode) node, string localName)

**static void**  

Throw exception if XmlNode does not exist, or does not have the expected LocalName.  

**Exceptions:**  
* **[XmlNodeException](WithoutHaste.DataFiles.XmlNodeException.md)**:   

## XmlToString([System.Xml.XmlDocument](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmldocument) xmlDocument)

**static string**  

Returns a string containing the entire contents of the XmlDocument.  

## XmlToString([System.Xml.XmlNode](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlnode) xmlNode)

**static string**  

Returns a string containing the entire contents of the XmlNode.  

