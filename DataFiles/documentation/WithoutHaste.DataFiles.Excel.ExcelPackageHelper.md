# [WithoutHaste.DataFiles.Excel](TableOfContents.WithoutHaste.DataFiles.Excel.md).ExcelPackageHelper

**Static**  
**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object)  

Extensions for ExcelPackage.  

# Fields

## const [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) MIN_COLUMN_CHAR

Minimum column character in excel.  

## const [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) MIN_COLUMN_INDEX

Minimum column index in excel.  

## const [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) MIN_ROW_INDEX

Minimum row index in excel.  

# Methods

##  AddWorksheet(OfficeOpenXml.ExcelPackage, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string))

Add a new Worksheet to an ExcelPackage.  

**Returns:**  
The new Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: ExcelPackage cannot be null.  

**Parameters:**  

##  AppendRow(OfficeOpenXml.ExcelWorksheet, [List&lt;string&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1))

Add a new row of data to the end of the Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Values list cannot be null.  

**Parameters:**  

##  AppendRow(OfficeOpenXml.ExcelWorksheet, [List&lt;int&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1))

Add a new row of data to the end of the Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Values list cannot be null.  

**Parameters:**  

##  AppendRow(OfficeOpenXml.ExcelWorksheet, [List&lt;decimal&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1))

Add a new row of data to the end of the Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Values list cannot be null.  

**Parameters:**  

##  AppendRow(OfficeOpenXml.ExcelWorksheet, [List&lt;object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1))

Add a new row of data to the end of the Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Values list cannot be null.  

**Parameters:**  

##  Clear(OfficeOpenXml.ExcelWorksheet)

Remove all rows and columns from Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  

##  CountColumns(OfficeOpenXml.ExcelWorksheet)

Returns the number of columns in the Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  

##  CountRows(OfficeOpenXml.ExcelWorksheet)

Returns the number of rows in the Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  

##  CountWorksheets(OfficeOpenXml.ExcelPackage)

Returns the number of Worksheets in the ExcelPacakge.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: ExcelPackage cannot be null.  

##  GetColumnByChar(OfficeOpenXml.ExcelWorksheet, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string), [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean))

Returns all values from one column, specified by the character name of the column.  

**Returns:**  
List of data values. Includes all cells to the bottom of the Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  

**Parameters:**  

##  GetColumnByHeader(OfficeOpenXml.ExcelWorksheet, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string))

Returns all the values from one column, specified by the header value.  

**Returns:**  
List of data values, not including the header. Includes all cells to the bottom of the Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  

**Parameters:**  

##  GetColumnByIndex(OfficeOpenXml.ExcelWorksheet, [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32), [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean))

Returns all values from one column, specified by the integer index of the column.  

**Returns:**  
List of data values. Includes all cells to the bottom of the Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  

**Parameters:**  

##  GetColumnCharForHeader(OfficeOpenXml.ExcelWorksheet, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string))

Searches first row for a particular value.  

**Returns:**  
Character name of the header's column, or null. Example: "A", "Z", "AA".  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  
* **[IndexOutOfRangeException](https://docs.microsoft.com/en-us/dotnet/api/system.indexoutofrangeexception)**: No header row found.  

**Parameters:**  

##  GetRow(OfficeOpenXml.ExcelWorksheet, [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32))

Returns all the values from one row, specified by the 0-indexed row number.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  
* **[IndexOutOfRangeException](https://docs.microsoft.com/en-us/dotnet/api/system.indexoutofrangeexception)**: Row index out of range.  

**Parameters:**  

##  GetWorksheet(OfficeOpenXml.ExcelPackage, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string))

Searches the ExcelPackage for a Worksheet by name.  

**Returns:**  
The Worksheet, or null.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: ExcelPackage cannot be null.  

**Parameters:**  

##  SetColumnByChar(OfficeOpenXml.ExcelWorksheet, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string), [List&lt;object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1), [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean))

Set an entire column of values at once.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Values list cannot be null.  

**Parameters:**  

##  SetColumnByHeader(OfficeOpenXml.ExcelWorksheet, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string), [List&lt;object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1))

Set an entire column of values at once.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Values list cannot be null.  

**Parameters:**  

##  SetColumnByIndex(OfficeOpenXml.ExcelWorksheet, [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32), [List&lt;object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1), [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean))

Set an entire column of values at once.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Values list cannot be null.  

**Parameters:**  

# Static Methods

## static [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) ColumnChar([int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) columnNumber)

Convert column integer index to character index.  

**Example A:**  
1 becomes A  

**Example B:**  
26 becomes Z  

**Example C:**  
27 becomes AA  

**Parameters:**  
* **[int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) columnNumber**: 1-based index of column  

