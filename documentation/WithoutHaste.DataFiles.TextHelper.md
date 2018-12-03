# [WithoutHaste.DataFiles](TableOfContents.WithoutHaste.DataFiles.md).TextHelper

**Static**  
**Inheritance:** object  

Contains operations for parsing and editing text.  

# Static Methods

## IsAllWhitespace(this string text)

**static bool**  

Returns true if text is empty or contains only whitespace characters.  

## RemoveFromEnd(this string text, string end)

**static string**  

Removes the _end_ string from the end of _text_, if it exists there.  

## RemoveFromStart(this string text, string start)

**static string**  

Removes the _start_ string from the beginning of _text_, if it exists there.  

## RemoveOuterBraces(this string text)

**static string**  

Removes outer matched pairs of braces from string.  
Only changes string if first and last characters are a matched pair of braces.  
Supports {}, [], (), and `<>`.  

## ReplaceUnescapedCharacters(this string text, char escapeChar, char searchChar, char replacementChar)

**static string**  

Replaces all instances of the unescaped _searchChar_ in the _text_.  

**Remarks:**  
The _escapeChar_ can escape itself.  

**Example A:**  

```
string original = "A.B.C\.D\\.E";
string result = original.ReplaceUnescapedCharacters('\', '.', '_');
//result = "A_B_C\.D\\_E"
```  

## SplitIgnoreNested(this string text, char delimiter)

**static [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.array)**  

Split _text_ on the _delimiter_   
but do not split if _delimiter_ is nested within matched braces.  
Supports braces: {}, [], (), and `<>`.  

**Remarks:**  
Returns empty string for empty matches.  

**Example A:**  
"A,B{c,d},E[f,g,h]".SplitIgnoreNested(",") returns ["A", "B{c,d}", "E[f,g,h]"]  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Delimitor cannot be a supported brace character.  
* **[StringFormatException](WithoutHaste.DataFiles.StringFormatException.md)**: Mismatched open/close braces.  

