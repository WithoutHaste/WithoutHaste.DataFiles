# [WithoutHaste.DataFiles](TableOfContents.WithoutHaste.DataFiles.md).AlphabetCounter

**Sealed**  
**Inheritance:** object → [ArbitraryCounter](WithoutHaste.DataFiles.ArbitraryCounter.md)  

Counts "A", "B", "C", ..., "Z", "AA", "AB", ..., "AZ", "BA", "BB", ...  

# Fields

## CHARACTERS

**const [Char[]](https://docs.microsoft.com/en-us/dotnet/api/system.array)**  

Valid characters.  

# Constructors

## AlphabetCounter()

Initialize a counter at "A".  

# Operators

## AlphabetCounter = AlphabetCounter counter + int delta

Increment counter.  

## AlphabetCounter = (AlphabetCounter counter)++

Increment counter by 1.  

## AlphabetCounter = AlphabetCounter counter - int delta

Decrement counter.  

## AlphabetCounter = (AlphabetCounter counter)--

Decrement counter by 1.  

