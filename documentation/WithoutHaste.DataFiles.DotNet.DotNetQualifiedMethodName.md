# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetQualifiedMethodName

**Inheritance:** object → [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md)  
**Implements:** [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable)  

Represents a fully qualified method name.  

**Remarks:**  
Cannot handle methods that declare more than 12 generic types,  
such as `MyMethod<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13>`.  

# Fields

## protected [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.array) genericTypeAliases

Specific generic type aliases for this method. If null, the shared [GenericTypeNames](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md) will be used.  

## static [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.array) GenericTypeNames

Default names that will be given to generic-method-types, in order.  

## [List&lt;DotNetParameter&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) Parameters

# Properties

## [DotNetQualifiedClassName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedClassName.md) FullClassNamespace { get; }

Strongly-typed FullNamespace.  

## int GenericTypeCount { get; protected set; }

The number of generic-types required by the method declaration.  

## bool IsGeneric { get; }

True for generic methods.  

## string LocalName { get; }

Local method name with generic type parameters (if applicable).  

## string LocalXmlName { get; }

Local data type name, written in the Xml style.  

**Example A:**  
``MyType`1 instead of MyType<T>``  

## string ParametersWithNames { get; }

Returns parameter list formatted as: (TypeA a, TypeB b)  

## string ParametersWithoutNames { get; }

Returns parameter list formatted as: (TypeA, TypeB)  

## bool ReturnTypeIsPartOfSignature { get; }

True if the return type is necessary for distinguishing this method name from others.  

**Example A:**  
True for implicit and explicit conversion operators.  

## [DotNetQualifiedTypeName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName.md) ReturnTypeName { get; protected set; }

Fully qualified name of return data type, if known. Null if not known.  

# Constructors

## DotNetQualifiedMethodName()

Empty constructor  

## DotNetQualifiedMethodName(string localName, [List&lt;DotNetParameter&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) parameters, [DotNetQualifiedTypeName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName.md) returnTypeName = null, int genericTypeCount = 0, [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) explicitInterface = null)

## DotNetQualifiedMethodName(string localName, [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) fullNamespace, [List&lt;DotNetParameter&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) parameters, [DotNetQualifiedTypeName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName.md) returnTypeName = null, int genericTypeCount = 0, [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) explicitInterface = null)

## DotNetQualifiedMethodName([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name, [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) explicitInterface = null)

# Methods

## void AddAssemblyInfo([System.Reflection.MethodInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodinfo) methodInfo)

Load additional documentation information from the assembly itself.  

## DotNetQualifiedMethodName Clone()

Returns deep clone of qualified name.  

## virtual int CompareTo(object b)

Methods are sorted:  
1. alphabetically by namespace  
2. alphabetically by explicit interface implementation  
3. then parameter list, shortest to longest  
4. then alphabetically by parameter types  
5. then alphabetically by return type (for some operators)  

## bool MatchesArguments([System.Reflection.ParameterInfo[]](https://docs.microsoft.com/en-us/dotnet/api/system.array) otherParameters)

Returns true if this method's parameter list matches the reflected ParameterInfo. Checks parameter types, not names.  

## bool MatchesArguments([List&lt;DotNetParameter&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) otherParameters)

Returns true if this method's parameter list matches the provided parameter list. Checks parameter types, not names.  

## bool MatchesLocalSignature([DotNetQualifiedMethodName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md) other)

Returns true if this method's signature matches the other method signature.  
Looks at local name instead of entire namespace.  

## bool MatchesSignature([System.Reflection.MethodInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodinfo) methodInfo)

Returns true if this method's signature matches the reflected MethodInfo.  

## bool MatchesSignature([DotNetQualifiedMethodName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md) other)

Returns true if this method's signature matches the other method signature.  

## void SetLocalName(string name)

Set the local name of the method. Does not affect generic type parameters or method parameters.  

# Static Methods

## static DotNetQualifiedMethodName FromVisualStudioXml(string signature)

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

## static [List&lt;DotNetParameter&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) ParametersFromVisualStudioXml(string text)

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

