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

# Constructors

## DotNetReferenceClassGeneric(int genericTypeIndex)

Creates a generic-type using the [DotNetQualifiedClassName.DefaultGenericTypeNames](WithoutHaste.DataFiles.DotNet.DotNetQualifiedClassName.md).  

**Remarks:**  
Index value will be set to 0 if it is less than 0.  
Alias will be set to "?" if the index value is out of range.  

**Parameters:**  
* **int genericTypeIndex**: 0-based index of generic-type in relation to the class's declaration.  

## DotNetReferenceClassGeneric(int genericTypeIndex, string alias)

Creates a generic-type using the provided alias.  

**Remarks:**  
Index value will be set to 0 if it is less than 0.  

**Parameters:**  
* **int genericTypeIndex**: 0-based index of generic-type in relation to the class's declaration.  
* **string alias**: The provided value will be used for the type alias, regardless of the index.  

# Methods

## Clone()

**DotNetReferenceClassGeneric**  

Returns deep clone of generic reference name.  

## GetLocalized([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) other)

**DotNetReferenceClassGeneric**  

**Misc:**  
  

## MatchesSignature([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) other)

**bool**  

Returns true if these types match. Does not look at aliases.  

## MatchesSignature([Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) type)

**bool**  

Returns true if this generic type matches the reflected type.  
Compares generic indexes and whether it is a class-generic or method-generic.  
Does not compare aliases or which specific class/method the type is referencing.  

# Static Methods

## FromVisualStudioXml(string typeName)

**static DotNetReferenceClassGeneric**  

Parses a .Net XML documentation type names that reference class generic types parameters.  

**Returns:**  
Returns a default value if the _typeName_ is not in the correct format.  

**Example A:**  
Given:
```xml
public class MyType<T>
{ 
	T MyField;
}
```

   
the type of the field is formatted as `` `0``.  

## HasExpectedVisualStudioXmlFormat(string name)

**static bool**  

Check if a string is properly formatted as a parameter referencing a class-generic-type.  

**Example A:**  
`` `0``, `` `1``, `` `2``, etc.  

