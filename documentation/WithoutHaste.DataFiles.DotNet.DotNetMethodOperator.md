# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetMethodOperator

**Inheritance:** object → [DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md) → [DotNetMethod](WithoutHaste.DataFiles.DotNet.DotNetMethod.md)  
**Implements:** [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable)  

Represents a method that is an operator.  

# Fields

## OperatorOrder

**const [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.array)**  

Operators will be sorted into this order.  

# Constructors

## DotNetMethodOperator([DotNetQualifiedMethodName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md) name)

# Methods

## CompareTo(object b)

**int**  

Methods are sorted:  
1. alphabetically by namespace  
2. then into [OperatorOrder](WithoutHaste.DataFiles.DotNet.DotNetMethodOperator.md)  
3. then as a normal method (see [DotNetQualifiedMethodName.CompareTo(object)](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md)  

# Operators

## bool = DotNetMethodOperator a > DotNetMethodOperator b

Methods are sorted:  
1. alphabetically by namespace  
2. then into [OperatorOrder](WithoutHaste.DataFiles.DotNet.DotNetMethodOperator.md)  
3. then as a normal method (see [DotNetQualifiedMethodName.CompareTo(object)](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md)  

## bool = DotNetMethodOperator a < DotNetMethodOperator b

Methods are sorted:  
1. alphabetically by namespace  
2. then into [OperatorOrder](WithoutHaste.DataFiles.DotNet.DotNetMethodOperator.md)  
3. then as a normal method (see [DotNetQualifiedMethodName.CompareTo(object)](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md)  

