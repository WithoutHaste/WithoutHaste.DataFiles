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

**{ }**  

# Methods

## Localize([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md))

Generic type references cannot be localized.  

## SetAlias([String[]](https://docs.microsoft.com/en-us/dotnet/api/system.array))

Set the generic-type alias of this type, based on this ordered list of aliases.  

**Returns:**  
Returns False if the index is out of bounds and the alias is not updated.  

# Derived By

[WithoutHaste.DataFiles.DotNet.DotNetReferenceClassGeneric](WithoutHaste.DataFiles.DotNet.DotNetReferenceClassGeneric.md)  
Represents a generic-type parameter that is in reference to a class's declared generic types.  

[WithoutHaste.DataFiles.DotNet.DotNetReferenceMethodGeneric](WithoutHaste.DataFiles.DotNet.DotNetReferenceMethodGeneric.md)  
Represents a generic-type parameter that is in reference to a method's declared generic types.  

