# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetQualifiedClassName

**Inheritance:** object → [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md)  
**Implements:** [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable)  

Represents a fully qualified class name, for class declarations.  

**Remarks:**  
Cannot handle classes that declare more than 12 generic types,  
such as `MyType<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13>`.  

# Fields

## DefaultGenericTypeNames

Default names that will be given to generic-types, in order.  

## genericTypeAliases

**protected [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.array)**  

Specific generic type aliases for this method. Defaults to the [DefaultGenericTypeNames](WithoutHaste.DataFiles.DotNet.DotNetQualifiedClassName.md) values.  

# Properties

## FullClassNamespace

**DotNetQualifiedClassName { public get; }**  

Strongly-typed FullNamespace.  

## GenericTypeAliases

**{ }**  

A copy of the ordered list of generic-type aliases used by this class name.  

**Remarks:**  
Non-generic class names may still have default alias values.  

## GenericTypeCount

**int { public get; protected set; }**  

The number of generic-types required by the class declaration.  

## IsGeneric

**{ }**  

True if this is a generic class name.  

## LocalName

**string { public get; }**  

Local data type name, written in the c# style.  

**Example A:**  
``MyType<T> instead of MyType`1``  

## LocalXmlName

**string { public get; }**  

Local data type name, written in the Xml style.  

**Example A:**  
``MyType`1 instead of MyType<T>``  

# Constructors

## DotNetQualifiedClassName()

Empty constructor  

## DotNetQualifiedClassName(string localName, int genericTypeCount = 0)

## DotNetQualifiedClassName(string localName, [DotNetQualifiedClassName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedClassName.md) fullNamespace, int genericTypeCount = 0)

# Methods

## AddAssemblyInfo([System.Reflection.TypeInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.typeinfo) typeInfo)

**void**  

Load additional documentation information from the assembly itself.  

## Clone()

**DotNetQualifiedClassName**  

Returns deep clone of qualified name.  

# Static Methods

## FromVisualStudioXml(string name)

**static DotNetQualifiedClassName**  

Parses a .Net XML documentation type name or namespace name.  

**Remarks:**  
Does not differentiate between types and namespaces   
because a nested type will have other type names in its namespace path  
and there are no important diffences in parsing the two.  

**Example A:**  
How .Net xml documentation formats generic types:  
Backtics are followed by integers, identifying generic types.  

Single backtics (such as &#96;1) on a class declaration indicate a count of generic types for the class.  
For example, `MyGenericType<T,U,V>` is documented as ``MyGenericType`3``.  
Anywhere else within this object's documentation that a single backtic appears, it indicates the zero-based index of the generic type in reference to the class declaration.  
For example, the constructor `MyGenericType(T,U,V)` is documented as ``MyGenericType.#ctor(`0,`1,`2)``.  

**Parameters:**  
* **string name**: Name may or may not start with "T:"  

