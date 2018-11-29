# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetQualifiedClassName

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md)  
**Implements:** [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable)  

Represents a fully qualified class name, for class declarations.  

**Remarks:**  
Cannot handle classes that declare more than 12 generic types,  
such as `MyType<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13>`.  

# Fields

## protected [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string[]) genericTypeAliases

Specific generic type aliases for this type. If null, the shared [WithoutHaste.DataFiles.DotNet.DotNetQualifiedClassName.GenericTypeNames](WithoutHaste.DataFiles.DotNet.DotNetQualifiedClassName.md) will be used.  

## static [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string[]) GenericTypeNames

Default names that will be given to generic-types, in order.  

# Properties

## DotNetQualifiedClassName FullClassNamespace { get; }

Strongly-typed FullNamespace.  

## [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) GenericTypeCount { get; protected set; }

The number of generic-types required by the class declaration.  

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) LocalName { get; }

Local data type name, written in the c# style.  

**Example A:**  
`MyType<T> instead of MyType`1`  

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) LocalXmlName { get; }

Local data type name, written in the Xml style.  

**Example A:**  
`MyType`1 instead of MyType<T>`  

# Constructors

## DotNetQualifiedClassName()

Empty constructor  

## DotNetQualifiedClassName([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) localName, [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) genericTypeCount = 0)

## DotNetQualifiedClassName([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) localName, [DotNetQualifiedClassName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedClassName.md) fullNamespace, [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) genericTypeCount = 0)

# Methods

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) AddAssemblyInfo([System.Reflection.TypeInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.typeinfo) typeInfo)

Load additional documentation information from the assembly itself.  

## DotNetQualifiedClassName Clone()

Returns deep clone of qualified name.  

# Static Methods

## static DotNetQualifiedClassName FromVisualStudioXml([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) name)

Parses a .Net XML documentation type name or namespace name.  

**Remarks:**  
Does not differentiate between types and namespaces   
because a nested type will have other type names in its namespace path  
and there are no important diffences in parsing the two.  

**Example A:**  
How .Net xml documentation formats generic types:  
Backtics are followed by integers, identifying generic types.  

Single backtics (such as `1) on a class declaration indicate a count of generic types for the class.`MyGenericType<T,U,V> is documented as MyGenericType`3`  

Anywhere else within this object's documentation that a single backtic appears, it indicates the index of the generic type in reference to the class declaration.`MyGenericType(T,U,V) is documented as MyGenericType.#ctor(`0,`1,`2)`  

**Parameters:**  
* **[string](https://docs.microsoft.com/en-us/dotnet/api/system.string) name**: Name may or may not start with "T:"  

