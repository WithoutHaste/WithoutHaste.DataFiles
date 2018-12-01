# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetMethodOperator

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md) → [DotNetMethod](WithoutHaste.DataFiles.DotNet.DotNetMethod.md)  
**Implements:** [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable)  

Represents a method that is an operator.  

# Fields

## const [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.array) OperatorOrder

Operators will be sorted into this order.  

# Constructors

## DotNetMethodOperator([DotNetQualifiedMethodName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md) name)

# Methods

## [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) CompareTo([object](https://docs.microsoft.com/en-us/dotnet/api/system.object) b)

Methods are sorted:  
1. alphabetically by namespace  
2. then into [OperatorOrder](WithoutHaste.DataFiles.DotNet.DotNetMethodOperator.md)  
3. then as a normal method (see [DotNetQualifiedMethodName.CompareTo(object)](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md)  

# Operators

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) = DotNetMethodOperator a > DotNetMethodOperator b

Methods are sorted:  
1. alphabetically by namespace  
2. then into [OperatorOrder](WithoutHaste.DataFiles.DotNet.DotNetMethodOperator.md)  
3. then as a normal method (see [DotNetQualifiedMethodName.CompareTo(object)](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md)  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) = DotNetMethodOperator a < DotNetMethodOperator b

Methods are sorted:  
1. alphabetically by namespace  
2. then into [OperatorOrder](WithoutHaste.DataFiles.DotNet.DotNetMethodOperator.md)  
3. then as a normal method (see [DotNetQualifiedMethodName.CompareTo(object)](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md)  

