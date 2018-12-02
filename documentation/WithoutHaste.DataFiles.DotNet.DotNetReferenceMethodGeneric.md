# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetReferenceMethodGeneric

**Inheritance:** object → [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) → [DotNetQualifiedTypeName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName.md) → [DotNetReferenceGeneric](WithoutHaste.DataFiles.DotNet.DotNetReferenceGeneric.md)  
**Implements:** [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable)  

Represents a generic-type parameter that is in reference to a method's declared generic types.  

# Examples

## Example A:

The "A" and "B" in the MyMethod parameters.
```xml
class MyGeneric<T,U>
{
    public MyGeneric(T t, U u) { }
    
    public void MyMethod<A,B>(A a, B b, T t, U u) { }
}
```  

# Properties

## string LocalName { get; }

# Constructors

## DotNetReferenceMethodGeneric(int genericTypeIndex, string alias = null)

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: _genericTypeIndex_ cannot be less than 0.  

### Parameters

#### int genericTypeIndex

0-based index of type in method declaration type parameter list.  

**Example A:**  
Index 0 refers to "A" in `void MyMethod<A,B>() { }`  

#### string alias

Alias of generic-type within assembly. Null if not known.  

# Static Methods

## static DotNetReferenceMethodGeneric FromVisualStudioXml(string name)

Parses a .Net XML documentation method-generic-type parameter.  

**Example A:**  
`Namespace.MyType.MyMethod<A>(A)` is formatted as ```Namespace.MyType.MyMethod``1(``0)```.  

**Exceptions:**  
* **[XmlFormatException](WithoutHaste.DataFiles.XmlFormatException.md)**: _name_ is not in expected format: &#96;&#96;Index.  

## static bool HasExpectedVisualStudioXmlFormat(string name)

Check if a string is properly formatted as a parameter referencing a method-generic-type.  

**Example A:**  
``` ``0```, ``` ``1```, ``` ``2```, etc.  

