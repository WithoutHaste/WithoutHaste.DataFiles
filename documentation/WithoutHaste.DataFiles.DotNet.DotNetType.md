# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetType

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md)  

Represents a data type.  

# Fields

## [List&lt;WithoutHaste.DataFiles.DotNet.DotNetDelegate&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) Delegates

## [List&lt;WithoutHaste.DataFiles.DotNet.DotNetEvent&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) Events

## [List&lt;WithoutHaste.DataFiles.DotNet.DotNetField&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) Fields

## [List&lt;WithoutHaste.DataFiles.DotNet.DotNetBaseType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) ImplementedInterfaces

Interfaces this type inherits from, if known  

**Remarks:**  
If an interface extends another interface, reflection reports that the type implements both interfaces.  

## [List&lt;WithoutHaste.DataFiles.DotNet.DotNetMethod&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) Methods

## [List&lt;WithoutHaste.DataFiles.DotNet.DotNetType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) NestedTypes

## [List&lt;WithoutHaste.DataFiles.DotNet.DotNetProperty&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) Properties

# Properties

## [List&lt;WithoutHaste.DataFiles.DotNet.DotNetMember&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) AllMembers { get; }

Lists all methods, delegates, fields, properties, and events.  
Does not include nested types.  

## [WithoutHaste.DataFiles.DotNet.DotNetBaseType](WithoutHaste.DataFiles.DotNet.DotNetBaseType.md) BaseType { get; protected set; }

Base type this type inherits from. Null if not known or none exists.  

## [WithoutHaste.DataFiles.DotNet.TypeCategory](WithoutHaste.DataFiles.DotNet.TypeCategory.md) Category { get; protected set; }

## [List&lt;WithoutHaste.DataFiles.DotNet.DotNetField&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) ConstantFields { get; }

The subset of Fields that are constants.  

## [List&lt;WithoutHaste.DataFiles.DotNet.DotNetMethodConstructor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) ConstructorMethods { get; }

The subset of Methods that are constructors.  

## [WithoutHaste.DataFiles.DotNet.DotNetMethodDestructor](WithoutHaste.DataFiles.DotNet.DotNetMethodDestructor.md) DestructorMethod { get; }

The subset of Methods that are destructors. There can be zero or one destructors.  

## [List&lt;WithoutHaste.DataFiles.DotNet.DotNetIndexer&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) IndexerProperties { get; }

The subset of Properties that are indexers.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) IsSealed { get; protected set; }

True if the type is sealed.  

**Remarks:**  
Abstract classes, static classes, and interfaces cannot be sealed. Exceptions can be sealed.  

## [List&lt;WithoutHaste.DataFiles.DotNet.DotNetType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) NestedEnums { get; }

The subset of NestedTypes that are enums.  

## [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) NestedTypeCount { get; }

The number of types nested within this type, including sub-nested types and enums.  

## [List&lt;WithoutHaste.DataFiles.DotNet.DotNetField&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) NormalFields { get; }

The subset of Fields that are not constant, or where the category is unknown.  

## [List&lt;WithoutHaste.DataFiles.DotNet.DotNetMethod&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) NormalMethods { get; }

The subset of Methods that are not static, nor constructors, nor destructors, nor operators.  

## [List&lt;WithoutHaste.DataFiles.DotNet.DotNetProperty&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) NormalProperties { get; }

The subset of Properties that are not indexers.  

## [List&lt;WithoutHaste.DataFiles.DotNet.DotNetMethodOperator&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) OperatorMethods { get; }

The subset of Methods that are operator overloads.  

## [List&lt;WithoutHaste.DataFiles.DotNet.DotNetMethod&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) StaticMethods { get; }

The subset of Methods that are static, but not constructors, nor destructors, nor operators.  

## [WithoutHaste.DataFiles.DotNet.DotNetQualifiedClassName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedClassName.md) TypeName { get; }

Strongly typed name.  

# Constructors

## DotNetType([WithoutHaste.DataFiles.DotNet.DotNetQualifiedClassName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedClassName.md) name)

# Methods

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) AddAssemblyInfo([System.Reflection.TypeInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.typeinfo) typeInfo, [WithoutHaste.DataFiles.DotNet.DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

Load additional documentation information from the assembly itself.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) AddMember([WithoutHaste.DataFiles.DotNet.DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md) member)

Add a member to the correct level within this type.  

## [WithoutHaste.DataFiles.DotNet.DotNetEvent](WithoutHaste.DataFiles.DotNet.DotNetEvent.md) FindInheritedEvent([Func&lt;WithoutHaste.DataFiles.DotNet.DotNetQualifiedName,WithoutHaste.DataFiles.DotNet.DotNetType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindType, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) localName)

Returns the selected event, if it exists in this type.  

**Parameters:**  
* **[Func&lt;WithoutHaste.DataFiles.DotNet.DotNetQualifiedName,WithoutHaste.DataFiles.DotNet.DotNetType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindType**: Function that returns the selected type from all known types in the assembly.  
* **[string](https://docs.microsoft.com/en-us/dotnet/api/system.string) localName**: Name of event, local to this type.  

## [WithoutHaste.DataFiles.DotNet.DotNetField](WithoutHaste.DataFiles.DotNet.DotNetField.md) FindInheritedField([Func&lt;WithoutHaste.DataFiles.DotNet.DotNetQualifiedName,WithoutHaste.DataFiles.DotNet.DotNetType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindType, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) localName)

Returns the selected field, if it exists in this type.  

**Parameters:**  
* **[Func&lt;WithoutHaste.DataFiles.DotNet.DotNetQualifiedName,WithoutHaste.DataFiles.DotNet.DotNetType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindType**: Function that returns the selected type from all known types in the assembly.  
* **[string](https://docs.microsoft.com/en-us/dotnet/api/system.string) localName**: Name of field, local to this type.  

## [WithoutHaste.DataFiles.DotNet.DotNetMethod](WithoutHaste.DataFiles.DotNet.DotNetMethod.md) FindInheritedMethod([Func&lt;WithoutHaste.DataFiles.DotNet.DotNetQualifiedName,WithoutHaste.DataFiles.DotNet.DotNetType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindType, [WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md) methodName)

Returns the selected method, if it exists in this type.  

**Parameters:**  
* **[Func&lt;WithoutHaste.DataFiles.DotNet.DotNetQualifiedName,WithoutHaste.DataFiles.DotNet.DotNetType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindType**: Function that returns the selected type from all known types in the assembly.  
* **[WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md) methodName**: Name of method, local to this type.  

## [WithoutHaste.DataFiles.DotNet.DotNetProperty](WithoutHaste.DataFiles.DotNet.DotNetProperty.md) FindInheritedProperty([Func&lt;WithoutHaste.DataFiles.DotNet.DotNetQualifiedName,WithoutHaste.DataFiles.DotNet.DotNetType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindType, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) localName)

Returns the selected property, if it exists in this type.  

**Parameters:**  
* **[Func&lt;WithoutHaste.DataFiles.DotNet.DotNetQualifiedName,WithoutHaste.DataFiles.DotNet.DotNetType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindType**: Function that returns the selected type from all known types in the assembly.  
* **[string](https://docs.microsoft.com/en-us/dotnet/api/system.string) localName**: Name of property, local to this type.  

## [WithoutHaste.DataFiles.DotNet.DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md) FindMember([WithoutHaste.DataFiles.DotNet.DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

Returns the specified member, of any type.  

## DotNetType FindType([WithoutHaste.DataFiles.DotNet.DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

Returns the selected type, whether it is this one or one of its nested types. Returns null if the type is not found.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) Is([WithoutHaste.DataFiles.DotNet.DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

Returns true if this member's name matches the provided name.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) IsDirectChild([WithoutHaste.DataFiles.DotNet.DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

Returns true if this qualified name is defined directly within this type.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) Owns([WithoutHaste.DataFiles.DotNet.DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md) member)

Returns true if this member is defined within this type or any of its nested types.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) Owns([WithoutHaste.DataFiles.DotNet.DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

Returns true if this qualified name is defined within this type or any of its nested types.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) ResolveDuplicatedComments([Func&lt;WithoutHaste.DataFiles.DotNet.DotNetQualifiedName,WithoutHaste.DataFiles.DotNet.DotNetMember&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindMember)

For all "duplicate" comments, replace the comment with the duplicated comments.  

**Parameters:**  
* **[Func&lt;WithoutHaste.DataFiles.DotNet.DotNetQualifiedName,WithoutHaste.DataFiles.DotNet.DotNetMember&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindMember**: Function that returns the selected member from all known members in the assembly.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) ResolveInheritedComments([Func&lt;WithoutHaste.DataFiles.DotNet.DotNetQualifiedName,WithoutHaste.DataFiles.DotNet.DotNetType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindType, [List&lt;WithoutHaste.DataFiles.DotNet.DotNetQualifiedName&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) inheritancePath = null)

For all "inheritdoc" comments, replace the inheritance comment with the inherited comments.  

**Remarks:**  
Classes can inherit from their base class (or the base class's base, etc).  
Interfaces can inherit from interfaces.  
Class members can inherit from their base class or from interfaces.  

**Parameters:**  
* **[Func&lt;WithoutHaste.DataFiles.DotNet.DotNetQualifiedName,WithoutHaste.DataFiles.DotNet.DotNetType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindType**: Function that returns the selected type from all known types in the assembly.  
* **[List&lt;WithoutHaste.DataFiles.DotNet.DotNetQualifiedName&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) inheritancePath**: List of types or members inheriting from each other, from top level to bottom level. Used to avoid loops.  

## [WithoutHaste.DataFiles.DotNet.DotNetDelegate](WithoutHaste.DataFiles.DotNet.DotNetDelegate.md) ToDelegate([WithoutHaste.DataFiles.DotNet.DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

Converts the type into a delegate, transfering all applicable data.  

**Remarks:**  
If the _name_refers to a sub-type, that type is the one converted.  
The sub-type is removed from its parent and the new delegate is added in its place  

**Returns:**  
The new delegate, or null if the type is not found.  

**Parameters:**  
* **[WithoutHaste.DataFiles.DotNet.DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name**: The fully qualified name of the delegate.  

## virtual [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) ToString()

# Static Methods

## static DotNetType FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) memberElement)

Parse .Net XML documentation for Type data.  

**Parameters:**  
* **[System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) memberElement**: Expects tag name "member".  

