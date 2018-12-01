# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetProperty

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md) → [DotNetField](WithoutHaste.DataFiles.DotNet.DotNetField.md)  

Represents a type&#96;s property.  

# Properties

## [DotNetPropertyMethod](WithoutHaste.DataFiles.DotNet.DotNetPropertyMethod.md) GetterMethod { get; protected set; }

The "get" method of the property.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) HasGetterMethod { get; }

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) HasSetterMethod { get; }

## [DotNetPropertyMethod](WithoutHaste.DataFiles.DotNet.DotNetPropertyMethod.md) SetterMethod { get; protected set; }

The "set" method of the property.  

# Constructors

## DotNetProperty([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

# Methods

## virtual [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) AddAssemblyInfo([System.Reflection.PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo) propertyInfo)

Load additional documentation information from the assembly itself.  

# Static Methods

## static DotNetProperty FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) memberElement)

Parse .Net XML documentation for Property data.  

**Example A:**  
`<member name="P:Namespace.Type.PropertyName"></member>`  

**Parameters:**  
* **[System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) memberElement**: Expects tag name "member".  

# Derived By

[WithoutHaste.DataFiles.DotNet.DotNetIndexer](WithoutHaste.DataFiles.DotNet.DotNetIndexer.md)  
Represents an indexer property.  

