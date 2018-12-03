# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetReferenceClassGeneric

**Inheritance:** object → [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) → [DotNetQualifiedTypeName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName.md) → [DotNetReferenceGeneric](WithoutHaste.DataFiles.DotNet.DotNetReferenceGeneric.md)  
**Implements:** [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable)  

Represents a generic-type parameter that is in reference to a class's declared generic types.  

# Examples

## Example A:

The "T" and "U" in the constructor parameters.
```xml
class MyGeneric<T,U>
{
    public MyGeneric(T t, U u) { }
}
```  

# Properties

## LocalName

**string { public get; }**  

# Constructors

## DotNetReferenceClassGeneric(int genericTypeIndex, string alias = null)

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: _genericTypeIndex_ cannot be less than 0.  

### Parameters

#### int genericTypeIndex

0-based index of type in class declaration type parameter list.  

**Example A:**  
`Index 0 refers to "T" in "class MyGeneric<T,U> { }"`  

#### string alias

Alias of generic-type within assembly. Null if not known.  

# Static Methods

## FromVisualStudioXml(string name)

**static DotNetReferenceClassGeneric**  

Parses a .Net XML documentation class-generic-type parameter.  

**Example A:**  
`Namespace.MyType<T>{ }` is formatted as ``Namespace.MyType`1``.  

**Exceptions:**  
* **[XmlFormatException](WithoutHaste.DataFiles.XmlFormatException.md)**: _name_ is not in expected format: `` `Index``.  

## HasExpectedVisualStudioXmlFormat(string name)

**static bool**  

Check if a string is properly formatted as a parameter referencing a class-generic-type.  

**Example A:**  
`` `0``, `` `1``, `` `2``, etc.  

