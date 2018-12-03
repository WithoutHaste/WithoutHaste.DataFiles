# [WithoutHaste.DataFiles.Excel](TableOfContents.WithoutHaste.DataFiles.Excel.md).ExcelPackageHelper

**Static**  
**Inheritance:** object  

Extensions for EPPlus OfficeOpenXml.ExcelPackage.  

# Fields

## MIN_COLUMN_CHAR

**const string**  

Minimum column character in excel.  

## MIN_COLUMN_INDEX

**const int**  

Minimum column index in excel.  

## MIN_ROW_INDEX

**const int**  

Minimum row index in excel.  

# Static Methods

## AddWorksheet(OfficeOpenXml.ExcelPackage excelPackage, string name)

**static OfficeOpenXml.ExcelWorksheet**  

Add a new Worksheet to an ExcelPackage.  

**Returns:**  
The new Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: _excelPackage_ cannot be null.  

**Parameters:**  
* **OfficeOpenXml.ExcelPackage excelPackage**:   
* **string name**: The name of the Worksheet.  

## AppendRow(OfficeOpenXml.ExcelWorksheet worksheet, [List&lt;string&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) values)

**static void**  

Add a new row of data to the end of the Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Values list cannot be null.  

**Parameters:**  
* **OfficeOpenXml.ExcelWorksheet worksheet**:   
* **[List&lt;string&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) values**: The data values for the row.  

## AppendRow(OfficeOpenXml.ExcelWorksheet worksheet, [List&lt;int&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) values)

**static void**  

Add a new row of data to the end of the Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Values list cannot be null.  

**Parameters:**  
* **OfficeOpenXml.ExcelWorksheet worksheet**:   
* **[List&lt;int&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) values**: The data values for the row.  

## AppendRow(OfficeOpenXml.ExcelWorksheet worksheet, [List&lt;decimal&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) values)

**static void**  

Add a new row of data to the end of the Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Values list cannot be null.  

**Parameters:**  
* **OfficeOpenXml.ExcelWorksheet worksheet**:   
* **[List&lt;decimal&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) values**: The data values for the row.  

## AppendRow(OfficeOpenXml.ExcelWorksheet worksheet, [List&lt;object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) values)

**static void**  

Add a new row of data to the end of the Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Values list cannot be null.  

**Parameters:**  
* **OfficeOpenXml.ExcelWorksheet worksheet**:   
* **[List&lt;object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) values**: The data values for the row.  

## Clear(OfficeOpenXml.ExcelWorksheet worksheet)

**static void**  

Remove all rows and columns from Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  

## ColumnChar(int columnNumber)

**static string**  

Convert column integer index to character index.  

**Example A:**  
1 becomes A  

**Example B:**  
26 becomes Z  

**Example C:**  
27 becomes AA  

**Parameters:**  
* **int columnNumber**: 1-based index of column  

## CountColumns(OfficeOpenXml.ExcelWorksheet worksheet)

**static int**  

Returns the number of columns in the Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  

## CountRows(OfficeOpenXml.ExcelWorksheet worksheet)

**static int**  

Returns the number of rows in the Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  

## CountWorksheets(OfficeOpenXml.ExcelPackage excelPackage)

**static int**  

Returns the number of Worksheets in the ExcelPacakge.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: ExcelPackage cannot be null.  

## GetColumnByChar(OfficeOpenXml.ExcelWorksheet worksheet, string column, bool skipFirstRow = True)

**static [List&lt;object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

Returns all values from one column, specified by the character name of the column.  

**Returns:**  
List of data values. Includes all cells to the bottom of the Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  

**Parameters:**  
* **OfficeOpenXml.ExcelWorksheet worksheet**:   
* **string column**: Character name of column. Example: "A", "Z", "AA".  
* **bool skipFirstRow**: If true, the first value in the column is not included. Intended for skipping the header value.  

## GetColumnByHeader(OfficeOpenXml.ExcelWorksheet worksheet, string header)

**static [List&lt;object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

Returns all the values from one column, specified by the header value.  

**Returns:**  
List of data values, not including the header. Includes all cells to the bottom of the Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  

**Parameters:**  
* **OfficeOpenXml.ExcelWorksheet worksheet**:   
* **string header**:   

## GetColumnByIndex(OfficeOpenXml.ExcelWorksheet worksheet, int column, bool skipFirstRow = True)

**static [List&lt;object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

Returns all values from one column, specified by the integer index of the column.  

**Returns:**  
List of data values. Includes all cells to the bottom of the Worksheet.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  

**Parameters:**  
* **OfficeOpenXml.ExcelWorksheet worksheet**:   
* **int column**: Integer index of column, starting at 1.  
* **bool skipFirstRow**: If true, the first value in the column is not included. Intended for skipping the header value.  

## GetColumnCharForHeader(OfficeOpenXml.ExcelWorksheet worksheet, string header)

**static string**  

Searches first row for a particular value.  

**Returns:**  
Character name of the header's column, or null. Example: "A", "Z", "AA".  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  
* **[IndexOutOfRangeException](https://docs.microsoft.com/en-us/dotnet/api/system.indexoutofrangeexception)**: No header row found.  

**Parameters:**  
* **OfficeOpenXml.ExcelWorksheet worksheet**:   
* **string header**: Header value.  

## GetRow(OfficeOpenXml.ExcelWorksheet worksheet, int row)

**static [List&lt;object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

Returns all the values from one row, specified by the 0-indexed row number.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  
* **[IndexOutOfRangeException](https://docs.microsoft.com/en-us/dotnet/api/system.indexoutofrangeexception)**: Row index out of range.  

**Parameters:**  
* **OfficeOpenXml.ExcelWorksheet worksheet**:   
* **int row**: Row number, starting at 1.  

## GetWorksheet(OfficeOpenXml.ExcelPackage excelPackage, string name)

**static OfficeOpenXml.ExcelWorksheet**  

Searches the ExcelPackage for a Worksheet by name.  

**Returns:**  
The Worksheet, or null.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: _excelPackage_ cannot be null.  

**Parameters:**  
* **OfficeOpenXml.ExcelPackage excelPackage**:   
* **string name**: The name of the Worksheet.  

## SetColumnByChar(OfficeOpenXml.ExcelWorksheet worksheet, string column, [List&lt;object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) values, bool skipFirstRow = True)

**static void**  

Set an entire column of values at once.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Values list cannot be null.  

**Parameters:**  
* **OfficeOpenXml.ExcelWorksheet worksheet**:   
* **string column**: Character name of column. Example: "A", "Z", "AA".  
* **[List&lt;object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) values**: Data values for the column.  
* **bool skipFirstRow**: If true, values[0] is applied to row 2 instead of 1. Intended for indicating the first row is for headers.  

## SetColumnByHeader(OfficeOpenXml.ExcelWorksheet worksheet, string header, [List&lt;object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) values)

**static void**  

Set an entire column of values at once.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Values list cannot be null.  

**Parameters:**  
* **OfficeOpenXml.ExcelWorksheet worksheet**:   
* **string header**: Header value of column.  
* **[List&lt;object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) values**: Data values for the column. Should not include the header.  

## SetColumnByIndex(OfficeOpenXml.ExcelWorksheet worksheet, int column, [List&lt;object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) values, bool skipFirstRow = True)

**static void**  

Set an entire column of values at once.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Worksheet cannot be null.  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Values list cannot be null.  

**Parameters:**  
* **OfficeOpenXml.ExcelWorksheet worksheet**:   
* **int column**: Integer index of column, starting at 1.  
* **[List&lt;object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) values**: Data values for the column.  
* **bool skipFirstRow**: If true, values[0] is applied to row 2 instead of 1. Intended for indicating the first row is for headers.  

