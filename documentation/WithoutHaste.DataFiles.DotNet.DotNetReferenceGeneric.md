# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetReferenceGeneric

**Abstract**  
**Inheritance:** object → [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) → [DotNetQualifiedTypeName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName.md)  
**Implements:** [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable)  

Represents a generic-type parameter that is not in a class declaration or a method declaration.  

# Examples

## Example A:

The "U"s in MyMethod(`List<U>` list, U obj).  

# Fields

## genericTypeIndex

**protected int**  

0-based index in class's generic type list corresponding to this parameter.  

# Properties

## Alias

**string { public get; protected set; }**  

The generic-type alias specified in the assembly. Null if not known.  
Whether this refers to a class-generic or method-generic is determined by the subclass.  

## LocalName

**string { public get; }**  

# Methods

## Localize([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) other)

**virtual void**  

Generic type references cannot be localized.  

# Derived By

[WithoutHaste.DataFiles.DotNet.DotNetReferenceClassGeneric](WithoutHaste.DataFiles.DotNet.DotNetReferenceClassGeneric.md)  
Represents a generic-type parameter that is in reference to a class's declared generic types.  

[WithoutHaste.DataFiles.DotNet.DotNetReferenceMethodGeneric](WithoutHaste.DataFiles.DotNet.DotNetReferenceMethodGeneric.md)  
Represents a generic-type parameter that is in reference to a method's declared generic types.  

