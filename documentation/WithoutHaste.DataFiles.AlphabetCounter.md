# [WithoutHaste.DataFiles](TableOfContents.WithoutHaste.DataFiles.md).AlphabetCounter

**Sealed**  
**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ArbitraryCounter](WithoutHaste.DataFiles.ArbitraryCounter.md)  

Counts "A", "B", "C", ..., "Z", "AA", "AB", ..., "AZ", "BA", "BB", ...  

# Fields

## const [Char[]](https://docs.microsoft.com/en-us/dotnet/api/system.array) CHARACTERS

Valid characters.  

# Constructors

## AlphabetCounter()

Initialize a counter at "A".  

# Operators

## AlphabetCounter = AlphabetCounter counter + [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) delta

Increment counter.  

## AlphabetCounter = (AlphabetCounter counter)++

Increment counter by 1.  

## AlphabetCounter = AlphabetCounter counter - [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) delta

Decrement counter.  

## AlphabetCounter = (AlphabetCounter counter)--

Decrement counter by 1.  

