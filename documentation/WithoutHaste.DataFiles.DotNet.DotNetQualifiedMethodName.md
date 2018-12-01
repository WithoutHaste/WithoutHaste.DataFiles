# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetQualifiedMethodName

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md)  
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

## [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) GenericTypeCount { get; protected set; }

The number of generic-types required by the method declaration.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) IsGeneric { get; }

True for generic methods.  

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) LocalName { get; }

Local method name with generic type parameters (if applicable).  

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) LocalXmlName { get; }

Local data type name, written in the Xml style.  

**Example A:**  
``MyType`1 instead of MyType<T>``  

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) ParametersWithNames { get; }

Returns parameter list formatted as: (TypeA a, TypeB b)  

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) ParametersWithoutNames { get; }

Returns parameter list formatted as: (TypeA, TypeB)  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) ReturnTypeIsPartOfSignature { get; }

True if the return type is necessary for distinguishing this method name from others.  

**Example A:**  
True for implicit and explicit conversion operators.  

## [DotNetQualifiedTypeName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName.md) ReturnTypeName { get; protected set; }

Fully qualified name of return data type, if known. Null if not known.  

# Constructors

## DotNetQualifiedMethodName()

Empty constructor  

## DotNetQualifiedMethodName([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) localName, [List&lt;DotNetParameter&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) parameters, [DotNetQualifiedTypeName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName.md) returnTypeName = null, [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) genericTypeCount = 0, [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) explicitInterface = null)

## DotNetQualifiedMethodName([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) localName, [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) fullNamespace, [List&lt;DotNetParameter&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) parameters, [DotNetQualifiedTypeName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName.md) returnTypeName = null, [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) genericTypeCount = 0, [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) explicitInterface = null)

## DotNetQualifiedMethodName([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name, [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) explicitInterface = null)

# Methods

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) AddAssemblyInfo([System.Reflection.MethodInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodinfo) methodInfo)

Load additional documentation information from the assembly itself.  

## DotNetQualifiedMethodName Clone()

Returns deep clone of qualified name.  

## virtual [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) CompareTo([object](https://docs.microsoft.com/en-us/dotnet/api/system.object) b)

Methods are sorted:  
* alphabetically by namespace  
* alphabetically by explicit interface implementation  
* then parameter list, shortest to longest  
* then alphabetically by parameter types  
* then alphabetically by return type (for some operators)  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) MatchesArguments([System.Reflection.ParameterInfo[]](https://docs.microsoft.com/en-us/dotnet/api/system.array) otherParameters)

Returns true if this method&#96;s parameter list matches the reflected ParameterInfo. Checks parameter types, not names.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) MatchesArguments([List&lt;DotNetParameter&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) otherParameters)

Returns true if this method&#96;s parameter list matches the provided parameter list. Checks parameter types, not names.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) MatchesLocalSignature([DotNetQualifiedMethodName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md) other)

Returns true if this method&#96;s signature matches the other method signature.  
Looks at local name instead of entire namespace.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) MatchesSignature([System.Reflection.MethodInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodinfo) methodInfo)

Returns true if this method&#96;s signature matches the reflected MethodInfo.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) MatchesSignature([DotNetQualifiedMethodName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md) other)

Returns true if this method&#96;s signature matches the other method signature.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) SetLocalName([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) name)

Set the local name of the method. Does not affect generic type parameters or method parameters.  

# Static Methods

## static DotNetQualifiedMethodName FromVisualStudioXml([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) signature)

Parses a .Net XML documentation method signature.  

**Example A:**  
How .Net xml documentation formats generic types:  
                 Backtics are followed by integers, identifying generic types.  
              
                 Double backtics (such as ``1) on a method name indicate a count of generic types for the method.  
                 Example: ```MyMethod<A,B,C> is documented as MyMethod``3```                   
            		Anywhere else within this method&#96;s documentation that a double backtic appears, it indicates the index of the generic type in reference to the method declaration.  
                 Example: ```MyMethod<A,B,C>(A,B,C) is documented as MyMethod``3(``0,``1,``2)```                   
            		A method that uses both its own generic types AND generic types from the class declaration will look like this:  
                 Example: ```MyMethod<A,B,C>(A,B,C,T,U) is documented as MyMethod``3(``0,``1,``2,`0,`1)```  

**Example B:**  
How .Net xml documentation formats implicit and explicit operators:  
  
static explicit operator int(MyClass a)  
becomes  
MyClass.op_Explicit(MyClass)~System.Int32  
  
static implicit operator int(MyClass a)  
becomes  
MyClass.op_Implicit(MyClass)~System.Int32  

**Parameters:**  
* **[string](https://docs.microsoft.com/en-us/dotnet/api/system.string) signature**: Name may or may not start with "M:". Includes parameter list.  

## static [List&lt;DotNetParameter&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) ParametersFromVisualStudioXml([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text)

Parse .Net XML documentation parameter lists.  

**Parameters:**  
* **[string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text**: Expects: null
Expects: empty string
Expects: "(type, type, type)"  

# Operators

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) = DotNetQualifiedMethodName a > DotNetQualifiedMethodName b

Methods are sorted:  
* alphabetically by namespace  
* alphabetically by explicit interface implementation  
* then parameter list, shortest to longest  
* then alphabetically by parameter types  
* then alphabetically by return type (for some operators)  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) = DotNetQualifiedMethodName a < DotNetQualifiedMethodName b

Methods are sorted:  
* alphabetically by namespace  
* alphabetically by explicit interface implementation  
* then parameter list, shortest to longest  
* then alphabetically by parameter types  
* then alphabetically by return type (for some operators)  

