# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetQualifiedTypeName

**Inheritance:** object → [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md)  
**Implements:** [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable)  

Represents a fully qualified type name, for return types / field types / property types / parameter types.  

# Fields

## [List&lt;DotNetQualifiedTypeName&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) GenericTypeParameters

If this is a generic type, these are the specified parameter types.  

**Example A:**  
In parameter type `List<System.String>`, System.String is the generic-type parameter of List.  

# Properties

## DotNetQualifiedTypeName FullTypeNamespace { get; }

Strongly-typed FullNamespace.  

## string LocalName { get; }

Local data type name with generic type parameters (if applicable).  

# Constructors

## DotNetQualifiedTypeName()

Empty constructor  

## DotNetQualifiedTypeName(string localName)

## DotNetQualifiedTypeName(string localName, [DotNetQualifiedTypeName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName.md) fullNamespace)

## DotNetQualifiedTypeName(string localName, [List&lt;DotNetQualifiedTypeName&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) genericTypeParameters)

## DotNetQualifiedTypeName(string localName, [List&lt;DotNetQualifiedTypeName&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) genericTypeParameters, [DotNetQualifiedTypeName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName.md) fullNamespace)

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: _genericTypeParameters_ cannot be null.  

**Parameters:**  
* **string localName**:   
* **[List&lt;DotNetQualifiedTypeName&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) genericTypeParameters**: List of generic-type parameters within this type.  
* **DotNetQualifiedTypeName fullNamespace**:   

# Methods

## DotNetQualifiedTypeName Clone()

Returns deep clone of qualified name.  

## DotNetQualifiedTypeName GetLocalized([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) other)

Returns a new name object which has been localized to the provided other name. The current object is not altered.  

## virtual void Localize([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) other)

Simplifies this qualified name based on the _other_ name.  
In other words, removes the portion of the namespace that this and the _other_ have in common.  
Alters the current object.  

**Remarks:**  
Will always keep at least the LocalName.  

Preserves explicit interface implementations.  

**Example A:**  
"System.Collections.Generic.List".Localize("System.Collections") returns "Generic.List".  

**Example B:**  
"System.Collections.Generic.List".Localize("System.Collections.Standard.List") returns "Standard.List".  

**Example C:**  
"System.Collections.Generic.List".Localize("System.Collections.Generic.List") returns "List".  

# Static Methods

## static DotNetQualifiedTypeName FromAssemblyInfo([Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) type)

Parses a System.Reflection.AssemblyInfo full name.  

## static DotNetQualifiedTypeName FromAssemblyInfo([Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) type, [List&lt;DotNetQualifiedTypeName&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) bubbleUpParameters = null)

Parses a System.Reflection.AssemblyInfo full name.  

**Misc:**  
* The escape character is backslash (\)  
* Nested types are separated with '+' instead of '.'  
* Class declaration of generic types are shown the same as .Net XML documentation: MyType&#96;1 for one generic type  
* When a generic type is defined: System.Collections.Generic.List&#96;1[U], where U is the type alias from the class declaration  

**Parameters:**  
* **[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) type**:   
* **[List&lt;DotNetQualifiedTypeName&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) bubbleUpParameters**: Optional. When reflection gives type information about a generic type nested inside a generic type, all the generic-type-arguments are listed in the inner-most type. This is for passing that information back up the chain of types.  

## static DotNetQualifiedTypeName FromAssemblyInfo(string typeName)

## static DotNetQualifiedTypeName FromVisualStudioXml(string typeName)

Parses a .Net XML documentation type name.  
Not intended for type declarations. Intended for field types, property types, parameter types, and return types.  

# Derived By

[WithoutHaste.DataFiles.DotNet.DotNetReferenceGeneric](WithoutHaste.DataFiles.DotNet.DotNetReferenceGeneric.md)  
Represents a generic-type parameter that is not in a class declaration or a method declaration.  

