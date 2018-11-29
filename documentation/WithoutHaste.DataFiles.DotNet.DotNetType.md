# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetType

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md)  

Represents a data type.  

# Fields

## [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) Delegates

## [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) Events

## [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) Fields

## [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) ImplementedInterfaces

Interfaces this type inherits from, if known  

**Remarks:**  
If an interface extends another interface, reflection reports that the type implements both interfaces.  

## [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) Methods

## [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) NestedTypes

## [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) Properties

# Properties

## [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) AllMembers { get; }

Lists all methods, delegates, fields, properties, and events.  
Does not include nested types.  

## [DotNetBaseType](WithoutHaste.DataFiles.DotNet.DotNetBaseType.md) BaseType { get; protected set; }

Base type this type inherits from. Null if not known or none exists.  

## [TypeCategory](WithoutHaste.DataFiles.DotNet.TypeCategory.md) Category { get; protected set; }

## [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) ConstantFields { get; }

The subset of Fields that are constants.  

## [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) ConstructorMethods { get; }

The subset of Methods that are constructors.  

## [DotNetMethodDestructor](WithoutHaste.DataFiles.DotNet.DotNetMethodDestructor.md) DestructorMethod { get; }

The subset of Methods that are destructors. There can be zero or one destructors.  

## [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) IndexerProperties { get; }

The subset of Properties that are indexers.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) IsSealed { get; protected set; }

True if the type is sealed.  

**Remarks:**  
Abstract classes, static classes, and interfaces cannot be sealed. Exceptions can be sealed.  

## [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) NestedEnums { get; }

The subset of NestedTypes that are enums.  

## [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) NestedTypeCount { get; }

The number of types nested within this type, including sub-nested types and enums.  

## [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) NormalFields { get; }

The subset of Fields that are not constant, or where the category is unknown.  

## [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) NormalMethods { get; }

The subset of Methods that are not static, nor constructors, nor destructors, nor operators.  

## [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) NormalProperties { get; }

The subset of Properties that are not indexers.  

## [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) OperatorMethods { get; }

The subset of Methods that are operator overloads.  

## [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) StaticMethods { get; }

The subset of Methods that are static (including extension methods), but not constructors, nor destructors, nor operators.  

## [DotNetQualifiedClassName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedClassName.md) TypeName { get; }

Strongly typed name.  

# Constructors

## DotNetType([DotNetQualifiedClassName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedClassName.md) name)

# Methods

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) AddAssemblyInfo([System.Reflection.TypeInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.typeinfo) typeInfo, [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

Load additional documentation information from the assembly itself.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) AddMember([DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md) member)

Add a member to the correct level within this type.  

## [DotNetEvent](WithoutHaste.DataFiles.DotNet.DotNetEvent.md) FindInheritedEvent([Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindType, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) localName)

Returns the selected event, if it exists in this type.  

**Parameters:**  
* **[Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindType**: Function that returns the selected type from all known types in the assembly.  
* **[string](https://docs.microsoft.com/en-us/dotnet/api/system.string) localName**: Name of event, local to this type.  

## [DotNetField](WithoutHaste.DataFiles.DotNet.DotNetField.md) FindInheritedField([Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindType, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) localName)

Returns the selected field, if it exists in this type.  

**Parameters:**  
* **[Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindType**: Function that returns the selected type from all known types in the assembly.  
* **[string](https://docs.microsoft.com/en-us/dotnet/api/system.string) localName**: Name of field, local to this type.  

## [DotNetMethod](WithoutHaste.DataFiles.DotNet.DotNetMethod.md) FindInheritedMethod([Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindType, [DotNetQualifiedMethodName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md) methodName)

Returns the selected method, if it exists in this type.  

**Parameters:**  
* **[Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindType**: Function that returns the selected type from all known types in the assembly.  
* **[DotNetQualifiedMethodName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md) methodName**: Name of method, local to this type.  

## [DotNetProperty](WithoutHaste.DataFiles.DotNet.DotNetProperty.md) FindInheritedProperty([Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindType, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) localName)

Returns the selected property, if it exists in this type.  

**Parameters:**  
* **[Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindType**: Function that returns the selected type from all known types in the assembly.  
* **[string](https://docs.microsoft.com/en-us/dotnet/api/system.string) localName**: Name of property, local to this type.  

## [DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md) FindMember([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

Returns the specified member, of any type.  

## DotNetType FindType([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

Returns the selected type, whether it is this one or one of its nested types. Returns null if the type is not found.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) Is([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

Returns true if this member's name matches the provided name.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) IsDirectChild([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

Returns true if this qualified name is defined directly within this type.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) Owns([DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md) member)

Returns true if this member is defined within this type or any of its nested types.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) Owns([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

Returns true if this qualified name is defined within this type or any of its nested types.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) ResolveDuplicatedComments([Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindMember)

For all "duplicate" comments, replace the comment with the duplicated comments.  

**Parameters:**  
* **[Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindMember**: Function that returns the selected member from all known members in the assembly.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) ResolveInheritedComments([Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindType, [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) inheritancePath = null)

For all "inheritdoc" comments, replace the inheritance comment with the inherited comments.  

**Remarks:**  
Classes can inherit from their base class (or the base class's base, etc).  
Interfaces can inherit from interfaces.  
Class members can inherit from their base class or from interfaces.  

**Parameters:**  
* **[Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindType**: Function that returns the selected type from all known types in the assembly.  
* **[List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) inheritancePath**: List of types or members inheriting from each other, from top level to bottom level. Used to avoid loops.  

## [DotNetDelegate](WithoutHaste.DataFiles.DotNet.DotNetDelegate.md) ToDelegate([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

Converts the type into a delegate, transfering all applicable data.  

**Remarks:**  
If the _name_refers to a sub-type, that type is the one converted.  
The sub-type is removed from its parent and the new delegate is added in its place  

**Returns:**  
The new delegate, or null if the type is not found.  

**Parameters:**  
* **[DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name**: The fully qualified name of the delegate.  

## virtual [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) ToString()

# Static Methods

## static DotNetType FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) memberElement)

Parse .Net XML documentation for Type data.  

**Parameters:**  
* **[System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) memberElement**: Expects tag name "member".  

