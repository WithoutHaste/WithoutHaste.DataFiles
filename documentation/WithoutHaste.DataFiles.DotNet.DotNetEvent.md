# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetEvent

**Inheritance:** object → [DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md) → [DotNetField](WithoutHaste.DataFiles.DotNet.DotNetField.md)  

Represents a type's event.  

# Constructors

## DotNetEvent([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

# Methods

## AddAssemblyInfo([System.Reflection.EventInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.eventinfo) eventInfo)

**void**  

Load additional documentation information from the assembly itself.  

# Static Methods

## FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) memberElement)

**static DotNetEvent**  

Parse .Net XML documentation for Event data.  

**Example A:**  
`<member name="E:Namespace.Type.EventName"></member>`  

**Parameters:**  
* **[System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) memberElement**: Expects tag name "member".  

