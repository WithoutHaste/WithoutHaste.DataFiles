# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetQualifiedMethodName

**Inheritance:** object → [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md)  
**Implements:** [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable)  

Represents a fully qualified method name.  

**Remarks:**  
Cannot handle methods that declare more than 12 generic types,  
such as `MyMethod<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13>`.  

# Fields

## DefaultGenericTypeNames

Default names that will be given to generic-method-types, in order.  

## genericTypeAliases

**protected [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.array)**  

Specific generic type aliases for this method. Defaults to the [DefaultGenericTypeNames](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md) values.  

## Parameters

**[List&lt;DotNetParameter&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

# Properties

## FullClassNamespace

**[DotNetQualifiedClassName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedClassName.md) { public get; }**  

Strongly-typed FullNamespace.  

## GenericTypeCount

**int { public get; protected set; }**  

The number of generic-types required by the method declaration.  

## IsGeneric

**bool { public get; }**  

True for generic methods.  

## LocalName

**string { public get; }**  

Local method name with generic type parameters (if applicable).  

## LocalXmlName

**string { public get; }**  

Local data type name, written in the Xml style.  

**Example A:**  
``MyType`1 instead of MyType<T>``  

## ParametersWithNames

**string { public get; }**  

Returns parameter list formatted as: (TypeA a, TypeB b)  

## ParametersWithoutNames

**string { public get; }**  

Returns parameter list formatted as: (TypeA, TypeB)  

## ReturnTypeIsPartOfSignature

**bool { public get; }**  

True if the return type is necessary for distinguishing this method name from others.  

**Example A:**  
True for implicit and explicit conversion operators.  

## ReturnTypeName

**[DotNetQualifiedTypeName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName.md) { public get; protected set; }**  

Fully qualified name of return data type, if known. Null if not known.  

# Constructors

## DotNetQualifiedMethodName()

Empty constructor  

## DotNetQualifiedMethodName(string localName, [List&lt;DotNetParameter&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) parameters, [DotNetQualifiedTypeName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName.md) returnTypeName = null, int genericTypeCount = 0, [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) explicitInterface = null)

## DotNetQualifiedMethodName(string localName, [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) fullNamespace, [List&lt;DotNetParameter&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) parameters, [DotNetQualifiedTypeName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName.md) returnTypeName = null, int genericTypeCount = 0, [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) explicitInterface = null)

## DotNetQualifiedMethodName([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name, [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) explicitInterface = null)

# Methods

## AddAssemblyInfo([System.Reflection.MethodInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodinfo) methodInfo)

**void**  

Load additional documentation information from the assembly itself.  

## Clone()

**DotNetQualifiedMethodName**  

Returns deep clone of qualified name.  

## CompareTo(object b)

**virtual int**  

Methods are sorted:  
1. alphabetically by namespace  
2. alphabetically by explicit interface implementation  
3. then parameter list, shortest to longest  
4. then alphabetically by parameter types  
5. then alphabetically by return type (for some operators)  

## MatchesArguments([System.Reflection.ParameterInfo[]](https://docs.microsoft.com/en-us/dotnet/api/system.array) otherParameters)

**bool**  

Returns true if this method's parameter list matches the reflected ParameterInfo. Checks parameter types, not names.  

## MatchesArguments([List&lt;DotNetParameter&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) otherParameters)

**bool**  

Returns true if this method's parameter list matches the provided parameter list. Checks parameter types, not names.  

## MatchesLocalSignature([DotNetQualifiedMethodName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md) other)

**bool**  

Returns true if this method's signature matches the other method signature.  
Looks at local name instead of entire namespace.  

## MatchesSignature([System.Reflection.MethodInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodinfo) methodInfo)

**bool**  

Returns true if this method's signature matches the reflected MethodInfo.  

## MatchesSignature([DotNetQualifiedMethodName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md) other)

**bool**  

Returns true if this method's signature matches the other method signature.  

## PushClassGenericTypes([String[]](https://docs.microsoft.com/en-us/dotnet/api/system.array))

Update method parameter types and return type with the class's generic-type aliases.  

**Parameters:**  

## PushMethodGenericTypes([String[]](https://docs.microsoft.com/en-us/dotnet/api/system.array))

Update method parameter types and return type with the method's generic-type aliases.  

**Parameters:**  

## SetClassName([DotNetQualifiedClassName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedClassName.md))

Constructors need to reference the actual name of their type so they display the right name with aliases.  

## SetLocalName(string name)

**void**  

Set the local name of the method. Does not affect generic type parameters or method parameters.  

# Static Methods

## FromVisualStudioXml(string signature)

**static DotNetQualifiedMethodName**  

Parses a .Net XML documentation method signature.  

**Example A:**  
XML documentation of generic types: Backtics are followed by integers, identifying generic types.  

Double backtics (such as &#96;&#96;1) on a method name indicate a count of generic types for the method.  
For example, `MyMethod<A,B,C>` is documented as ```MyMethod``3```.  

Anywhere else within this method's documentation that a double backtic appears, it indicates the index of the generic type in reference to the method declaration.  
For example, `MyMethod<A,B,C>(A,B,C)` is documented as ```MyMethod``3(``0,``1,``2)```.  

A method that uses both its own generic types AND generic types from the class declaration will look like this:  
For example, `MyMethod<A,B,C>(A,B,C,T,U)` is documented as ```MyMethod``3(``0,``1,``2,`0,`1)```.  

**Example B:**  
XML documentation of implicit and explicit operators:  

`static explicit operator int(MyClass a)` becomes `MyClass.op_Explicit(MyClass)~System.Int32`.  

`static implicit operator int(MyClass a)` becomes `MyClass.op_Implicit(MyClass)~System.Int32`.  

**Parameters:**  
* **string signature**: Name may or may not start with "M:". Includes parameter list.  

## ParametersFromVisualStudioXml(string text)

**static [List&lt;DotNetParameter&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

Parse .Net XML documentation parameter lists.  

**Parameters:**  
* **string text**: Expects: null, or empty string, or "(type, type, type)"  

# Operators

## bool = DotNetQualifiedMethodName a > DotNetQualifiedMethodName b

Methods are sorted:  
1. alphabetically by namespace  
2. alphabetically by explicit interface implementation  
3. then parameter list, shortest to longest  
4. then alphabetically by parameter types  
5. then alphabetically by return type (for some operators)  

## bool = DotNetQualifiedMethodName a < DotNetQualifiedMethodName b

Methods are sorted:  
1. alphabetically by namespace  
2. alphabetically by explicit interface implementation  
3. then parameter list, shortest to longest  
4. then alphabetically by parameter types  
5. then alphabetically by return type (for some operators)  

