# MSExcel2003XmlFile

Building a Microsoft Excel 2003 Xml file with XmlDocument.

Base Type: System.Object

## Examples

Format:
            
```
<? xml version="1.0" encoding="UTF-8"?>
<? mso-application progid="Excel.Sheet" ?>
<Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet"
  xmlns:c="urn:schemas-microsoft-com:office:component:spreadsheet"
  xmlns:html="http://www.w3.org/TR/REC-html40"
  xmlns:o="urn:schemas-microsoft-com:office:office"
  xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
  xmlns:x="urn:schemas-microsoft-com:office:excel"
  xmlns:x2="http://schemas.microsoft.com/office/excel/2003/xml"
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <ss:Worksheet ss:Name="Sheet1">
    <Table>
      <Column />
      <Row>
        <Cell>
          <Data>
            Cell Value
          </Data>
        </Cell>
      </Row>
    </Table>
  </ss:Worksheet>
</ss:Workbook>
```

## Properties

### XmlNode StylesNode

### XmlNode[] Tables

### XmlNode WorkbookNode

### XmlDocument XmlDocument

## Constructors

### MSExcel2003XmlFile()

Sets up a default file containing no Worksheets.

### MSExcel2003XmlFile(System.String fullPath)

Load from file.

## Static Methods

### String DateToString(System.DateTime date)

Converts a date to the expected string format.

## Methods

### AddColumns(System.Int32, System.Collections.Generic.List{System.Collections.Generic.List{System.Object}})

Creates Cells for each data values, adds sufficient Rows to the the specified Table to contain all the columns. Add all Cells to the Rows.

### AddColumns(System.Int32, System.Collections.Generic.List{System.Collections.Generic.List{System.Xml.XmlNode}})

Adds sufficient Rows to the the specified Table to contain all the columns. Add all Cells to the Rows.

### AddHeaderRow(System.Int32, System.Collections.Generic.List{System.String})

Adds a Row of header-style Cells to the specified Table.

### AddRow(System.Int32, System.Collections.Generic.List{System.Object})

Creates Cells of the appropriate type for each value, and adds all Cells to a new Row in the specified Table.

### AddRow(System.Int32, System.Collections.Generic.List{System.Xml.XmlNode})

Adds all Cells to a new Row in the specified Table.

### Int32 AddWorksheet(System.String title)

Add a Worksheet to the end of the list of Worksheets, containing an empty Table.

Parameter title: Worksheet title.  

Returns: The index of the worksheet/table.

### XmlAttribute GenerateAttribute(System.String prefix, System.String name, System.String value)

Returns a custom attribute.

ArgumentException: Unknown namespace URI prefix.

### XmlAttribute GenerateAttribute(System.String prefix, System.String uri, System.String name, System.String value)

Returns a custom attribute.

### XmlNode GenerateCell(System.Object data)

Creates a Cell tag of the appropriate type containing the specified data.
            Supports DateTime cells, Number cells, and Text cells. All unknown types are converted to strings.

### XmlNode GenerateDateCell(System.DateTime data)

Creates a Cell tag containing the specified date.

### XmlAttribute GenerateDateTypeAttribute()

Returns an ss:Type="DateTime" attribute.

### XmlNode GenerateEmptyCell()

Generate an empty Cell tag.

### XmlNode GenerateHeaderCell(System.String data)

Generate a header Cell tag containing the specified text.

### XmlAttribute GenerateNameAttribute(System.String value)

Returns an ss:Name attribute.

### XmlNode GenerateNumberCell(System.Int32 data)

Generate a Cell tag containing the specified number.

### XmlAttribute GenerateNumberTypeAttribute()

Returns an ss:Type="Number" attribute.

### XmlNode GenerateParagraphCell(System.String data)

Generate a paragraph Cell tag containing the specified text.

### XmlAttribute GenerateStringTypeAttribute()

Returns an ss:Type="String" attribute.

### XmlAttribute GenerateStyleIdAttribute(System.String value)

Returns an ss:StyleID attribute.

### XmlNode GenerateTextCell(System.String data)

Generate a paragraph Cell tag containing the specified text.

### XmlNode GenerateTextCell(System.String data, System.String styleId)

Generate a Cell tag containing the specified text.

### XmlAttribute GenerateTypeAttribute(System.String value)

Returns a custom ss:Type attribute.

### XmlAttribute GenerateWidthAttribute(System.Int32 value)

Returns an ss:Width attribute.

### Int32 GetColumnCount(System.Int32 tableIndex)

Returns the number of Columns in the specified Table.

### List`1 GetColumnValues(System.Int32 tableIndex, System.String header)

Returns a list of values from the Cells in the column with the specified header.

### List`1 GetColumnValues(System.Int32 tableIndex, System.Int32 columnIndex, System.Boolean firstRowIsHeader)

Returns a list of values from the cells in the selected column.

Parameter firstRowIsHeader: If true, the first row of the table is skipped.  

### Int32 GetHeaderIndex(System.Int32 tableIndex, System.String header)

Returns zero-based index of the column with the selected header.
            Returns -1 if header is not found.

### List`1 GetHeaders(System.Int32 tableIndex)

Returns all the header values from the first row.

### Int32 GetRowCount(System.Int32 tableIndex)

Returns the number of Rows in the specified Table.

### List`1 GetRowValues(System.Int32 tableIndex, System.Int32 rowIndex)

Returns a list of values from the Cells in the specified Row.

Parameter tableIndex: Zero-based index of Table in Workbook.  
Parameter rowIndex: Zero-based index of Row in Table.  

### Int32 GetTableIndex(System.String title)

Returns the index of the Table in the Worksheet with the specified title.

### Void RemoveStyle(System.String id)

Removes a Style.

### Void Save(System.String fullPath)

Save file.

### SetColumnWidths(System.Int32, System.Collections.Generic.List{System.Int32})

Set column widths on the specified Table. Overwrites column widths if they were already set.

