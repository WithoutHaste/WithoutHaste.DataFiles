# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetReferenceGeneric

**Abstract**  
**Inheritance:** object → [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) → [DotNetQualifiedTypeName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName.md)  
**Implements:** [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable)  

Represents a generic-type parameter that is not in a class declaration or a method declaration.  

# Examples

## Example A:

The "U"s in MyMethod(`List<U>` list, U obj).  

# Fields

## protected int genericTypeIndex

0-based index in class's generic type list corresponding to this parameter.  

# Properties

## string Alias { get; protected set; }

The generic-type alias specified in the assembly. Null if not known.  
Whether this refers to a class-generic or method-generic is determined by the subclass.  

# Constructors

## DotNetReferenceGeneric(int genericTypeIndex, string alias = null)

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: _genericTypeIndex_ cannot be less than 0.  

### Parameters

#### int genericTypeIndex

0-based index of type in class or method declaration type parameter list.  

**Example A:**  
Index 0 refers to "T" in `class MyGeneric<T,U> { }`.  

**Example B:**  
Index 0 refers to "A" in `void MyMethod<A,B>() { }`.  

#### string alias

Alias of generic-type within assembly. Null if not known.  

# Methods

## void SetAlias(string alias)

# Derived By

[WithoutHaste.DataFiles.DotNet.DotNetReferenceClassGeneric](WithoutHaste.DataFiles.DotNet.DotNetReferenceClassGeneric.md)  
Represents a generic-type parameter that is in reference to a class's declared generic types.  

[WithoutHaste.DataFiles.DotNet.DotNetReferenceMethodGeneric](WithoutHaste.DataFiles.DotNet.DotNetReferenceMethodGeneric.md)  
Represents a generic-type parameter that is in reference to a method's declared generic types.  

