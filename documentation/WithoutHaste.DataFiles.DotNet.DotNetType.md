# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetType

**Inheritance:** object → [DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md)  

Represents a data type: a class, interface, struct, or enum.  

# Fields

## Delegates

**[List&lt;DotNetDelegate&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

Delegates defined within this type.  

## Events

**[List&lt;DotNetEvent&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

All events defined within the type.  

## Fields

**[List&lt;DotNetField&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

All fields defined within this type.  

**Remarks:**  
By the .Net definition of "field", meaning that properties and events are not included.  

## ImplementedInterfaces

**[List&lt;DotNetBaseType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

Interfaces this type implements, if known.  

**Remarks:**  
If an interface extends another interface, reflection reports that the type implements both interfaces.  

## Methods

**[List&lt;DotNetMethod&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

All methods defined within this type.  

## NestedTypes

**[List&lt;DotNetType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

Data types defined/nested within this type.  

## Properties

**[List&lt;DotNetProperty&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

All properties defined within the type.  

# Properties

## AllMembers

**[List&lt;DotNetMember&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) { public get; }**  

Lists all methods, delegates, fields, properties, and events.  
Does not include nested types.  

## BaseType

**[DotNetBaseType](WithoutHaste.DataFiles.DotNet.DotNetBaseType.md) { public get; protected set; }**  

Base type this type inherits from. Null if not known or none exists.  

## Category

**[TypeCategory](WithoutHaste.DataFiles.DotNet.TypeCategory.md) { public get; protected set; }**  

## ClassName

**[DotNetQualifiedClassName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedClassName.md) { public get; }**  

Strongly-typed name.  

## ConstantFields

**[List&lt;DotNetField&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) { public get; }**  

The subset of Fields that are constants.  

## ConstructorMethods

**[List&lt;DotNetMethodConstructor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) { public get; }**  

The subset of Methods that are constructors.  

## DestructorMethod

**[DotNetMethodDestructor](WithoutHaste.DataFiles.DotNet.DotNetMethodDestructor.md) { public get; }**  

The subset of Methods that are destructors. There can be zero or one destructors.  

## IndexerProperties

**[List&lt;DotNetIndexer&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) { public get; }**  

The subset of Properties that are indexers.  

## IsSealed

**bool { public get; protected set; }**  

True if the type is sealed.  

**Remarks:**  
Abstract classes, static classes, and interfaces cannot be sealed. Exceptions can be sealed.  

## NestedEnums

**[List&lt;DotNetType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) { public get; }**  

The subset of NestedTypes that are enums.  

## NestedTypeCount

**int { public get; }**  

The number of types nested within this type, including sub-nested types and enums.  

## NormalFields

**[List&lt;DotNetField&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) { public get; }**  

The subset of Fields that are not constant, or where the category is unknown.  

## NormalMethods

**[List&lt;DotNetMethod&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) { public get; }**  

The subset of Methods that are not static, nor constructors, nor destructors, nor operators.  

## NormalProperties

**[List&lt;DotNetProperty&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) { public get; }**  

The subset of Properties that are not indexers.  

## OperatorMethods

**[List&lt;DotNetMethodOperator&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) { public get; }**  

The subset of Methods that are operator overloads.  

## StaticMethods

**[List&lt;DotNetMethod&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) { public get; }**  

The subset of Methods that are static (including extension methods), but not constructors, nor destructors, nor operators.  

# Constructors

## DotNetType([DotNetQualifiedClassName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedClassName.md) name)

# Methods

## AddAssemblyInfo([System.Reflection.TypeInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.typeinfo) typeInfo, [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

**void**  

Load additional documentation information from the assembly itself for this type or one of its nested type descendents.  

## AddMember([DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md) member)

**void**  

Add a member to this type or one of its nested type descendents.  

## FindInheritedEvent([Func&lt;DotNetQualifiedName,DotNetType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindType, string localName)

**[DotNetEvent](WithoutHaste.DataFiles.DotNet.DotNetEvent.md)**  

Returns the selected event, if it exists in this type.  

**Parameters:**  
* **[Func&lt;DotNetQualifiedName,DotNetType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindType**: Function that returns the selected type from all known types in the assembly.  
* **string localName**: Name of event, local to this type.  

## FindInheritedField([Func&lt;DotNetQualifiedName,DotNetType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindType, string localName)

**[DotNetField](WithoutHaste.DataFiles.DotNet.DotNetField.md)**  

Returns the selected field, if it exists in this type.  

**Parameters:**  
* **[Func&lt;DotNetQualifiedName,DotNetType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindType**: Function that returns the selected type from all known types in the assembly.  
* **string localName**: Name of field, local to this type.  

## FindInheritedMethod([Func&lt;DotNetQualifiedName,DotNetType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindType, [DotNetQualifiedMethodName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md) methodName)

**[DotNetMethod](WithoutHaste.DataFiles.DotNet.DotNetMethod.md)**  

Returns the selected method, if it exists in this type.  

**Parameters:**  
* **[Func&lt;DotNetQualifiedName,DotNetType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindType**: Function that returns the selected type from all known types in the assembly.  
* **[DotNetQualifiedMethodName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md) methodName**: Name of method, local to this type.  

## FindInheritedProperty([Func&lt;DotNetQualifiedName,DotNetType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindType, string localName)

**[DotNetProperty](WithoutHaste.DataFiles.DotNet.DotNetProperty.md)**  

Returns the selected property, if it exists in this type.  

**Parameters:**  
* **[Func&lt;DotNetQualifiedName,DotNetType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindType**: Function that returns the selected type from all known types in the assembly.  
* **string localName**: Name of property, local to this type.  

## FindMember([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

**[DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md)**  

Returns the specified member from this type of its nested type descendents. Can return a field, property, event, method, delegate, or type.  

## FindType([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

**DotNetType**  

Returns the selected type, whether it is the current type or one of its nested type descendents. Returns null if the type is not found.  

## Is([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

**bool**  

Returns true if this member's name matches the provided name.  

## IsDirectChild([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

**bool**  

Returns true if this qualified name is defined directly within this type.  

## Owns([DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md) member)

**bool**  

Returns true if this member is defined within this type or any of its nested type descendents.  

## Owns([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

**bool**  

Returns true if this qualified name is defined within this type or any of its nested type dscendents.  

## ResolveDuplicatedComments([Func&lt;DotNetQualifiedName,DotNetMember&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindMember)

**void**  

For all "duplicate" comments, replace the comment with the duplicated comments.  

**Parameters:**  
* **[Func&lt;DotNetQualifiedName,DotNetMember&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindMember**: Function that returns the selected member from all known members in the assembly.  

## ResolveInheritedComments([Func&lt;DotNetQualifiedName,DotNetType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindType, [List&lt;DotNetQualifiedName&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) inheritancePath = null)

**void**  

For all "inheritdoc" comments, replace the inheritance comment with the inherited comments.  

**Remarks:**  
Classes can inherit from their base class (or the base class's base, etc).  
Interfaces can inherit from interfaces.  
Class members can inherit from their base class or from interfaces.  

**Parameters:**  
* **[Func&lt;DotNetQualifiedName,DotNetType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindType**: Function that returns the selected type from all known types in the assembly.  
* **[List&lt;DotNetQualifiedName&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) inheritancePath**: List of types or members inheriting from each other, from top level to bottom level. Used to avoid loops.  

## ToDelegate([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

**[DotNetDelegate](WithoutHaste.DataFiles.DotNet.DotNetDelegate.md)**  

Converts the selected type into a delegate, transfering all applicable data.  

**Remarks:**  
If the _name_ refers to a nested type descendent, that type is the one converted.  
The nested type is removed from its parent and the new delegate is added in its place  

**Returns:**  
The new delegate, or null if the type is not found.  

**Parameters:**  
* **[DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name**: The fully qualified name of the delegate.  

## ToString()

**virtual string**  

Returns FullName of type.  

# Static Methods

## FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) memberElement)

**static DotNetType**  

Parse .Net XML documentation for Type data.  

**Example A:**  
`<member name="T:Namespace.Type"></member>`  

**Parameters:**  
* **[System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) memberElement**: Expects tag name "member".  

