# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetProperty

**Inheritance:** object → [DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md) → [DotNetField](WithoutHaste.DataFiles.DotNet.DotNetField.md)  

Represents a type's property.  

# Properties

## GetterMethod

**[DotNetPropertyMethod](WithoutHaste.DataFiles.DotNet.DotNetPropertyMethod.md) { public get; protected set; }**  

The "get" method of the property.  

## HasGetterMethod

**bool { public get; }**  

## HasSetterMethod

**bool { public get; }**  

## SetterMethod

**[DotNetPropertyMethod](WithoutHaste.DataFiles.DotNet.DotNetPropertyMethod.md) { public get; protected set; }**  

The "set" method of the property.  

# Constructors

## DotNetProperty([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

# Methods

## AddAssemblyInfo([System.Reflection.PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo) propertyInfo)

**virtual void**  

Load additional documentation information from the assembly itself.  

# Static Methods

## FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) memberElement)

**static DotNetProperty**  

Parse .Net XML documentation for Property data.  

**Example A:**  
`<member name="P:Namespace.Type.PropertyName"></member>`  

**Parameters:**  
* **[System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) memberElement**: Expects tag name "member".  

# Derived By

[WithoutHaste.DataFiles.DotNet.DotNetIndexer](WithoutHaste.DataFiles.DotNet.DotNetIndexer.md)  
Represents an indexer property.  

