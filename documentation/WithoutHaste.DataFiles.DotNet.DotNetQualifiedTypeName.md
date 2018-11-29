# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetQualifiedTypeName

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md)  
**Implements:** [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable)  

Represents a fully qualified type name, for return types / field types / property types / parameter types.  

# Fields

## [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) GenericTypeParameters

If this is a generic type, these are the specified parameter types.  

**Example A:**  
`In parameter type List<System.String>, System.String is the generic-type parameter of List.`  

# Properties

## DotNetQualifiedTypeName FullTypeNamespace { get; }

Strongly-typed FullNamespace.  

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) LocalName { get; }

Local data type name with generic type parameters (if applicable).  

# Constructors

## DotNetQualifiedTypeName()

Empty constructor  

## DotNetQualifiedTypeName([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) localName)

## DotNetQualifiedTypeName([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) localName, [DotNetQualifiedTypeName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName.md) fullNamespace)

## DotNetQualifiedTypeName([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) localName, [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) genericTypeParameters)

**Misc:**  
See [DotNetQualifiedTypeName(string, List&lt;WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName&gt;, WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName)](WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName.md)  

## DotNetQualifiedTypeName([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) localName, [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) genericTypeParameters, [DotNetQualifiedTypeName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName.md) fullNamespace)

### Exceptions

#### [ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)

_genericTypeParameters_  

 cannot be null.  

**Parameters:**  
* **[string](https://docs.microsoft.com/en-us/dotnet/api/system.string) localName**:   
* **[List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) genericTypeParameters**: List of generic-type parameters within this type.  
* **DotNetQualifiedTypeName fullNamespace**:   

# Methods

## DotNetQualifiedTypeName Clone()

Returns deep clone of qualified name.  

# Static Methods

## static DotNetQualifiedTypeName FromAssemblyInfo([Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) type)

Parses a System.Reflection.AssemblyInfo full name.  

## static DotNetQualifiedTypeName FromAssemblyInfo([Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) type, [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) bubbleUpParameters = null)

Parses a System.Reflection.AssemblyInfo full name.  

**Misc:**  
* **The escape character is '\'**  
* **Nested types are separated with '+' instead of '.'**  
* **Class declaration of generic types are shown the same as .Net XML documentation: MyType`1 for one generic type**  
* **When a generic type is defined: System.Collections.Generic.List`1[U], where U is the type alias from the class declaration**  

**Parameters:**  
* **[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) type**:   
* **[List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) bubbleUpParameters**: Optional. When reflection gives type information about a generic type nested inside a generic type, all the generic-type-arguments are listed in the inner-most type. This is for passing that information back up the chain of types.  

## static DotNetQualifiedTypeName FromAssemblyInfo([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) typeName)

## static DotNetQualifiedTypeName FromVisualStudioXml([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) typeName)

Parses a .Net XML documentation type name.  
Not intended for type declarations. Intended for field types, property types, parameter types, and return types.  

