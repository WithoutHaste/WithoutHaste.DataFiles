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

# Constructors

## DotNetReferenceMethodGeneric(int genericTypeIndex)

Creates a generic-type using the [DotNetQualifiedMethodName.DefaultGenericTypeNames](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md).  

**Remarks:**  
Index value will be set to 0 if it is less than 0.  
Alias will be set to "?" if the index value is out of range.  

**Parameters:**  
* **int genericTypeIndex**: 0-based index of generic-type in relation to the method's declaration.  

## DotNetReferenceMethodGeneric(int genericTypeIndex, string alias)

Creates a generic-type using the provided alias.  

**Remarks:**  
Index value will be set to 0 if it is less than 0.  

**Parameters:**  
* **int genericTypeIndex**: 0-based index of generic-type in relation to the method's declaration.  
* **string alias**: The provided value will be used for the type alias, regardless of the index.  

# Methods

## Clone()

**DotNetReferenceMethodGeneric**  

Returns deep clone of generic reference name.  

## GetLocalized([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) other)

**DotNetReferenceMethodGeneric**  

Returns a new name object which has been localized to the provided other name. The current object is not altered.  

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

**static DotNetReferenceMethodGeneric**  

Parses a .Net XML documentation type names that reference method generic type parameters.  

**Returns:**  
Returns a default value if the _typeName_ is not in the correct format.  

**Example A:**  
Given:
```xml
public class MyType
{ 
	public void MyMethod<A>(A a) { }
}
```

   
the type of the method parameter is formatted as ``` ``0```.  

## HasExpectedVisualStudioXmlFormat(string name)

**static bool**  

Check if a string is properly formatted as a parameter referencing a method-generic-type.  

**Example A:**  
``` ``0```, ``` ``1```, ``` ``2```, etc.  

