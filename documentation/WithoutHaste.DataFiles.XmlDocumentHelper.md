# [WithoutHaste.DataFiles](TableOfContents.WithoutHaste.DataFiles.md).XmlDocumentHelper

**Static**  
**Inheritance:** object  

Generic System.Xml.XmlDocument utilities.  

# Static Methods

## static void Validate([System.Xml.XmlNode](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlnode) node, string localName)

Throw exception if XmlNode does not exist, or does not have the expected LocalName.  

**Exceptions:**  
* **[XmlNodeException](WithoutHaste.DataFiles.XmlNodeException.md)**:   

## static string XmlToString([System.Xml.XmlDocument](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmldocument) xmlDocument)

Returns a string containing the entire contents of the XmlDocument.  

## static string XmlToString([System.Xml.XmlNode](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlnode) xmlNode)

Returns a string containing the entire contents of the XmlNode.  

