# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetField

**Inheritance:** object → [DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md)  

Represents a type's field.  

# Properties

## AccessModifier

**[AccessModifier](WithoutHaste.DataFiles.DotNet.AccessModifier.md) { public get; protected set; }**  

## Category

**[FieldCategory](WithoutHaste.DataFiles.DotNet.FieldCategory.md) { public get; protected set; }**  

## ConstantValue

**object { public get; protected set; }**  

For constant fields, the value of the constant. Null otherwise.  

## FullTypeName

**string { public get; }**  

The "FullName" of the field data type.  

## IsStatic

**bool { public get; protected set; }**  

False means unknown or is not static.  

## TypeName

**[DotNetQualifiedTypeName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName.md) { public get; protected set; }**  

Fully qualified name of data type, if known. Null if not known.  

# Constructors

## DotNetField([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

# Methods

## AddAssemblyInfo([System.Reflection.FieldInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.fieldinfo) fieldInfo)

**virtual void**  

Load additional documentation information from the assembly itself.  

## PushGenericTypes([String[]](https://docs.microsoft.com/en-us/dotnet/api/system.array))

Update field type with the class's generic-type aliases.  

**Parameters:**  

# Static Methods

## FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) memberElement)

**static DotNetField**  

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

