# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetField

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md)  

Represents a type's field.  

# Properties

## [AccessModifier](WithoutHaste.DataFiles.DotNet.AccessModifier.md) AccessModifier { get; protected set; }

## [FieldCategory](WithoutHaste.DataFiles.DotNet.FieldCategory.md) Category { get; protected set; }

## [object](https://docs.microsoft.com/en-us/dotnet/api/system.object) ConstantValue { get; protected set; }

For constant fields, the value of the constant. Null otherwise.  

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) FullTypeName { get; }

The "FullName" of the field data type.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) IsStatic { get; protected set; }

False means unknown or is not static.  

## [DotNetQualifiedTypeName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName.md) TypeName { get; protected set; }

Fully qualified name of data type, if known. Null if not known.  

# Constructors

## DotNetField([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

# Methods

## virtual [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) AddAssemblyInfo([System.Reflection.FieldInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.fieldinfo) fieldInfo)

Load additional documentation information from the assembly itself.  

# Static Methods

## static DotNetField FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) memberElement)

Parse .Net XML documentation for Field data.  

**Example A:**  
`<member name="F:Namespace.Type.FieldName"></member>`  

**Parameters:**  
* **[System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) memberElement**: Expects tag name "member".  

# Derived By

[WithoutHaste.DataFiles.DotNet.DotNetEvent](WithoutHaste.DataFiles.DotNet.DotNetEvent.md)  
Represents a type's event.  

[WithoutHaste.DataFiles.DotNet.DotNetProperty](WithoutHaste.DataFiles.DotNet.DotNetProperty.md)  
Represents a type's property.  

