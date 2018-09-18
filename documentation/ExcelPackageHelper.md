# ExcelPackageHelper

Static type.

Extensions for ExcelPackage.

Base Type: System.Object

## Fields

### Constant Fields

#### String MIN_COLUMN_CHAR

Minimum column character in excel.

#### Int32 MIN_COLUMN_INDEX

Minimum column index in excel.

#### Int32 MIN_ROW_INDEX

Minimum row index in excel.

## Static Methods

### ExcelWorksheet AddWorksheet(OfficeOpenXml.ExcelPackage excelPackage, System.String name)

Add a new Worksheet to an ExcelPackage.

Parameter name: The name of the Worksheet.  

Returns: The new Worksheet.

ArgumentException: ExcelPackage cannot be null.

### Void Clear(OfficeOpenXml.ExcelWorksheet worksheet)

Remove all rows and columns from Worksheet.

ArgumentException: Worksheet cannot be null.

### String ColumnChar(System.Int32 columnNumber)

Convert column integer index to character index.

Parameter columnNumber: 1-based index of column  

### Int32 CountColumns(OfficeOpenXml.ExcelWorksheet worksheet)

Returns the number of columns in the Worksheet.

ArgumentException: Worksheet cannot be null.

### Int32 CountRows(OfficeOpenXml.ExcelWorksheet worksheet)

Returns the number of rows in the Worksheet.

ArgumentException: Worksheet cannot be null.

### Int32 CountWorksheets(OfficeOpenXml.ExcelPackage excelPackage)

Returns the number of Worksheets in the ExcelPacakge.

ArgumentException: ExcelPackage cannot be null.

### List`1 GetColumnByChar(OfficeOpenXml.ExcelWorksheet worksheet, System.String column, System.Boolean skipFirstRow)

Returns all values from one column, specified by the character name of the column.

Parameter column: Character name of column. Example: "A", "Z", "AA".  
Parameter skipFirstRow: If true, the first value in the column is not included. Intended for skipping the header value.  

Returns: List of data values. Includes all cells to the bottom of the Worksheet.

ArgumentException: Worksheet cannot be null.

### List`1 GetColumnByHeader(OfficeOpenXml.ExcelWorksheet worksheet, System.String header)

Returns all the values from one column, specified by the header value.

Returns: List of data values, not including the header. Includes all cells to the bottom of the Worksheet.

ArgumentException: Worksheet cannot be null.

### List`1 GetColumnByIndex(OfficeOpenXml.ExcelWorksheet worksheet, System.Int32 column, System.Boolean skipFirstRow)

Returns all values from one column, specified by the integer index of the column.

Parameter column: Integer index of column, starting at 1.  
Parameter skipFirstRow: If true, the first value in the column is not included. Intended for skipping the header value.  

Returns: List of data values. Includes all cells to the bottom of the Worksheet.

ArgumentException: Worksheet cannot be null.

### String GetColumnCharForHeader(OfficeOpenXml.ExcelWorksheet worksheet, System.String header)

Searches first row for a particular value.

Parameter header: Header value.  

Returns: Character name of the header's column, or null. Example: "A", "Z", "AA".

ArgumentException: Worksheet cannot be null.
IndexOutOfRangeException: No header row found.

### List`1 GetRow(OfficeOpenXml.ExcelWorksheet worksheet, System.Int32 row)

Returns all the values from one row, specified by the 0-indexed row number.

Parameter row: Row number, starting at 1.  

ArgumentException: Worksheet cannot be null.
IndexOutOfRangeException: Row index out of range.

### ExcelWorksheet GetWorksheet(OfficeOpenXml.ExcelPackage excelPackage, System.String name)

Searches the ExcelPackage for a Worksheet by name.

Parameter name: The name of the Worksheet.  

Returns: The Worksheet, or null.

ArgumentException: ExcelPackage cannot be null.

## Methods

### AppendRow(OfficeOpenXml.ExcelWorksheet, System.Collections.Generic.List{System.String})

### AppendRow(OfficeOpenXml.ExcelWorksheet, System.Collections.Generic.List{System.Int32})

### AppendRow(OfficeOpenXml.ExcelWorksheet, System.Collections.Generic.List{System.Decimal})

### AppendRow(OfficeOpenXml.ExcelWorksheet, System.Collections.Generic.List{System.Object})

Add a new row of data to the end of the Worksheet.

ArgumentException: Worksheet cannot be null.
ArgumentException: Values list cannot be null.

### SetColumnByChar(OfficeOpenXml.ExcelWorksheet, System.String, System.Collections.Generic.List{System.Object}, System.Boolean)

Set an entire column of values at once.

ArgumentException: Worksheet cannot be null.
ArgumentException: Values list cannot be null.

### SetColumnByHeader(OfficeOpenXml.ExcelWorksheet, System.String, System.Collections.Generic.List{System.Object})

Set an entire column of values at once.

ArgumentException: Worksheet cannot be null.
ArgumentException: Values list cannot be null.

### SetColumnByIndex(OfficeOpenXml.ExcelWorksheet, System.Int32, System.Collections.Generic.List{System.Object}, System.Boolean)

Set an entire column of values at once.

ArgumentException: Worksheet cannot be null.
ArgumentException: Values list cannot be null.

