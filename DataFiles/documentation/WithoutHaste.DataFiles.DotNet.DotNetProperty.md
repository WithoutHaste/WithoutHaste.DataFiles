# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetProperty

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md) → [DotNetField](WithoutHaste.DataFiles.DotNet.DotNetField.md)  

Represents a type's property.  

# Properties

## [WithoutHaste.DataFiles.DotNet.DotNetPropertyMethod](WithoutHaste.DataFiles.DotNet.DotNetPropertyMethod.md) GetterMethod { get; protected set; }

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) HasGetterMethod { get; }

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) HasSetterMethod { get; }

## [WithoutHaste.DataFiles.DotNet.DotNetPropertyMethod](WithoutHaste.DataFiles.DotNet.DotNetPropertyMethod.md) SetterMethod { get; protected set; }

# Constructors

## DotNetProperty([WithoutHaste.DataFiles.DotNet.DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

# Methods

## virtual [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) AddAssemblyInfo([System.Reflection.PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo) propertyInfo)

Load additional documentation information from the assembly itself.  

# Static Methods

## static DotNetProperty FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) memberElement)

Parse .Net XML documentation for Property data.  

**Parameters:**  
* **[System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) memberElement**: Expects tag name "member".  

