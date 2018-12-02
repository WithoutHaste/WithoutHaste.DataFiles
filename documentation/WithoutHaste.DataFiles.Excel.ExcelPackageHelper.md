# [WithoutHaste.DataFiles.Excel](TableOfContents.WithoutHaste.DataFiles.Excel.md).ExcelPackageHelper

**Static**  
**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object)  

Extensions for EPPlus OfficeOpenXml.ExcelPackage.  

# Fields

## const [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) MIN_COLUMN_CHAR

Minimum column character in excel.  

## const [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) MIN_COLUMN_INDEX

Minimum column index in excel.  

## const [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) MIN_ROW_INDEX

Minimum row index in excel.  

# Static Methods

## static OfficeOpenXml.ExcelWorksheet AddWorksheet(OfficeOpenXml.ExcelPackage excelPackage, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) name)

Add a new Worksheet to an ExcelPackage.  

**Returns:**  
The new Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: _excelPackage_ cannot be null.  

**Parameters:**  
* **OfficeOpenXml.ExcelPackage excelPackage**:   
* **[string](https://docs.microsoft.com/en-us/dotnet/api/system.string) name**: The name of the Worksheet.  

## static [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) AppendRow(OfficeOpenXml.ExcelWorksheet worksheet, [List&lt;string&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) values)

Add a new row of data to the end of the Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Values list cannot be null.  

**Parameters:**  
* **OfficeOpenXml.ExcelWorksheet worksheet**:   
* **[List&lt;string&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) values**: The data values for the row.  

## static [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) AppendRow(OfficeOpenXml.ExcelWorksheet worksheet, [List&lt;int&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) values)

Add a new row of data to the end of the Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Values list cannot be null.  

**Parameters:**  
* **OfficeOpenXml.ExcelWorksheet worksheet**:   
* **[List&lt;int&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) values**: The data values for the row.  

## static [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) AppendRow(OfficeOpenXml.ExcelWorksheet worksheet, [List&lt;decimal&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) values)

Add a new row of data to the end of the Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Values list cannot be null.  

**Parameters:**  
* **OfficeOpenXml.ExcelWorksheet worksheet**:   
* **[List&lt;decimal&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) values**: The data values for the row.  

## static [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) AppendRow(OfficeOpenXml.ExcelWorksheet worksheet, [List&lt;object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) values)

Add a new row of data to the end of the Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Values list cannot be null.  

**Parameters:**  
* **OfficeOpenXml.ExcelWorksheet worksheet**:   
* **[List&lt;object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) values**: The data values for the row.  

## static [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) Clear(OfficeOpenXml.ExcelWorksheet worksheet)

Remove all rows and columns from Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  

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

## static [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) CountColumns(OfficeOpenXml.ExcelWorksheet worksheet)

Returns the number of columns in the Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  

## static [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) CountRows(OfficeOpenXml.ExcelWorksheet worksheet)

Returns the number of rows in the Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  

## static [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) CountWorksheets(OfficeOpenXml.ExcelPackage excelPackage)

Returns the number of Worksheets in the ExcelPacakge.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: ExcelPackage cannot be null.  

## static [List&lt;object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) GetColumnByChar(OfficeOpenXml.ExcelWorksheet worksheet, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) column, [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) skipFirstRow = True)

Returns all values from one column, specified by the character name of the column.  

**Returns:**  
List of data values. Includes all cells to the bottom of the Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  

**Parameters:**  
* **OfficeOpenXml.ExcelWorksheet worksheet**:   
* **[string](https://docs.microsoft.com/en-us/dotnet/api/system.string) column**: Character name of column. Example: "A", "Z", "AA".  
* **[bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) skipFirstRow**: If true, the first value in the column is not included. Intended for skipping the header value.  

## static [List&lt;object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) GetColumnByHeader(OfficeOpenXml.ExcelWorksheet worksheet, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) header)

Returns all the values from one column, specified by the header value.  

**Returns:**  
List of data values, not including the header. Includes all cells to the bottom of the Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  

**Parameters:**  
* **OfficeOpenXml.ExcelWorksheet worksheet**:   
* **[string](https://docs.microsoft.com/en-us/dotnet/api/system.string) header**:   

## static [List&lt;object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) GetColumnByIndex(OfficeOpenXml.ExcelWorksheet worksheet, [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) column, [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) skipFirstRow = True)

Returns all values from one column, specified by the integer index of the column.  

**Returns:**  
List of data values. Includes all cells to the bottom of the Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  

**Parameters:**  
* **OfficeOpenXml.ExcelWorksheet worksheet**:   
* **[int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) column**: Integer index of column, starting at 1.  
* **[bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) skipFirstRow**: If true, the first value in the column is not included. Intended for skipping the header value.  

## static [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) GetColumnCharForHeader(OfficeOpenXml.ExcelWorksheet worksheet, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) header)

Searches first row for a particular value.  

**Returns:**  
Character name of the header's column, or null. Example: "A", "Z", "AA".  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  
* **[IndexOutOfRangeException](https://docs.microsoft.com/en-us/dotnet/api/system.indexoutofrangeexception)**: No header row found.  

**Parameters:**  
* **OfficeOpenXml.ExcelWorksheet worksheet**:   
* **[string](https://docs.microsoft.com/en-us/dotnet/api/system.string) header**: Header value.  

## static [List&lt;object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) GetRow(OfficeOpenXml.ExcelWorksheet worksheet, [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) row)

Returns all the values from one row, specified by the 0-indexed row number.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  
* **[IndexOutOfRangeException](https://docs.microsoft.com/en-us/dotnet/api/system.indexoutofrangeexception)**: Row index out of range.  

**Parameters:**  
* **OfficeOpenXml.ExcelWorksheet worksheet**:   
* **[int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) row**: Row number, starting at 1.  

## static OfficeOpenXml.ExcelWorksheet GetWorksheet(OfficeOpenXml.ExcelPackage excelPackage, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) name)

Searches the ExcelPackage for a Worksheet by name.  

**Returns:**  
The Worksheet, or null.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: _excelPackage_ cannot be null.  

**Parameters:**  
* **OfficeOpenXml.ExcelPackage excelPackage**:   
* **[string](https://docs.microsoft.com/en-us/dotnet/api/system.string) name**: The name of the Worksheet.  

## static [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) SetColumnByChar(OfficeOpenXml.ExcelWorksheet worksheet, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) column, [List&lt;object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) values, [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) skipFirstRow = True)

Set an entire column of values at once.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Values list cannot be null.  

**Parameters:**  
* **OfficeOpenXml.ExcelWorksheet worksheet**:   
* **[string](https://docs.microsoft.com/en-us/dotnet/api/system.string) column**: Character name of column. Example: "A", "Z", "AA".  
* **[List&lt;object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) values**: Data values for the column.  
* **[bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) skipFirstRow**: If true, values[0] is applied to row 2 instead of 1. Intended for indicating the first row is for headers.  

## static [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) SetColumnByHeader(OfficeOpenXml.ExcelWorksheet worksheet, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) header, [List&lt;object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) values)

Set an entire column of values at once.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Values list cannot be null.  

**Parameters:**  
* **OfficeOpenXml.ExcelWorksheet worksheet**:   
* **[string](https://docs.microsoft.com/en-us/dotnet/api/system.string) header**: Header value of column.  
* **[List&lt;object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) values**: Data values for the column. Should not include the header.  

## static [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) SetColumnByIndex(OfficeOpenXml.ExcelWorksheet worksheet, [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) column, [List&lt;object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) values, [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) skipFirstRow = True)

Set an entire column of values at once.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Values list cannot be null.  

**Parameters:**  
* **OfficeOpenXml.ExcelWorksheet worksheet**:   
* **[int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) column**: Integer index of column, starting at 1.  
* **[List&lt;object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) values**: Data values for the column.  
* **[bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) skipFirstRow**: If true, values[0] is applied to row 2 instead of 1. Intended for indicating the first row is for headers.  

