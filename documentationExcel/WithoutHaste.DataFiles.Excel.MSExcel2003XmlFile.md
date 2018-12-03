# [WithoutHaste.DataFiles.Excel](TableOfContents.WithoutHaste.DataFiles.Excel.md).MSExcel2003XmlFile

**Inheritance:** object  

Building a Microsoft Excel 2003 Xml file with XmlDocument.  

# Examples

## Example A:

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

# Properties

## StylesNode

**[System.Xml.XmlNode](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlnode) { public get; protected set; }**  

## Tables

**[System.Xml.XmlNode[]](https://docs.microsoft.com/en-us/dotnet/api/system.array) { public get; }**  

## WorkbookNode

**[System.Xml.XmlNode](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlnode) { public get; protected set; }**  

## XmlDocument

**[System.Xml.XmlDocument](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmldocument) { public get; protected set; }**  

# Constructors

## MSExcel2003XmlFile()

Sets up a default file containing no Worksheets.  

## MSExcel2003XmlFile(string fullPath)

Load from file.  

# Methods

## AddColumns(int tableIndex, [List&lt;List&lt;object&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) columns)

**void**  

Creates Cells for each data values, adds sufficient Rows to the the specified Table to contain all the columns. Add all Cells to the Rows.  

**Parameters:**  
* **int tableIndex**: 0-based table index within Workbook.  
* **[List&lt;List&lt;object&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) columns**: List of columns, each column being a list of values.  

## AddColumns(int tableIndex, [List&lt;List&lt;System.Xml.XmlNode&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) columns)

**void**  

Adds sufficient Rows to the the specified Table to contain all the columns. Add all Cells to the Rows.  

**Parameters:**  
* **int tableIndex**: 0-based table index within Workbook.  
* **[List&lt;List&lt;System.Xml.XmlNode&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) columns**: List of columns, each column being a list of "Cell" tags.  

## AddHeaderRow(int tableIndex, [List&lt;string&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) headers)

**void**  

Adds a Row of header-style Cells to the specified Table.  

## AddRow(int tableIndex, [List&lt;object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) cellValues)

**void**  

Creates Cells of the appropriate type for each value, and adds all Cells to a new Row in the specified Table.  

## AddRow(int tableIndex, [List&lt;System.Xml.XmlNode&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) cellNodes)

**void**  

Adds all Cells to a new Row in the specified Table.  

## AddStyle(string id, string childName, string childAttributeName, string childAttributeValue)

**void**  

Adds a Style. If a Style with the same id already exists, it is overwritten.  

**Example A:**  

```xml
<Styles>
  <Style ss:ID="id">
    <childName ss:childNameAttribute="childAttributeValue" />
  </Style>
</Styles>
```  

## AddWorksheet(string title)

**int**  

Add a Worksheet to the end of the list of Worksheets, containing an empty Table.  

**Returns:**  
The index of the worksheet/table.  

**Parameters:**  
* **string title**: Worksheet title.  

## GenerateAttribute(string prefix, string name, string value)

**[System.Xml.XmlAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlattribute)**  

Returns a custom attribute.  

**Remarks:**  
Only namespace URIs known to this format can be referenced.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Unknown namespace URI prefix.  

## GenerateAttribute(string prefix, string uri, string name, string value)

**[System.Xml.XmlAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlattribute)**  

Returns a custom attribute.  

## GenerateCell(object data)

**[System.Xml.XmlNode](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlnode)**  

Creates a Cell tag of the appropriate type containing the specified data.  
Supports DateTime cells, Number cells, and Text cells. All unknown types are converted to strings.  

## GenerateDateCell([DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime) data)

**[System.Xml.XmlNode](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlnode)**  

Creates a Cell tag containing the specified date.  

## GenerateDateTypeAttribute()

**[System.Xml.XmlAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlattribute)**  

Returns an ss:Type="DateTime" attribute.  

## GenerateEmptyCell()

**[System.Xml.XmlNode](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlnode)**  

Generate an empty Cell tag.  

## GenerateHeaderCell(string data)

**[System.Xml.XmlNode](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlnode)**  

Generate a header Cell tag containing the specified text.  

## GenerateNameAttribute(string value)

**[System.Xml.XmlAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlattribute)**  

Returns an ss:Name attribute.  

## GenerateNumberCell(int data)

**[System.Xml.XmlNode](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlnode)**  

Generate a Cell tag containing the specified number.  

## GenerateNumberTypeAttribute()

**[System.Xml.XmlAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlattribute)**  

Returns an ss:Type="Number" attribute.  

## GenerateParagraphCell(string data)

**[System.Xml.XmlNode](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlnode)**  

Generate a paragraph Cell tag containing the specified text.  

## GenerateStringTypeAttribute()

**[System.Xml.XmlAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlattribute)**  

Returns an ss:Type="String" attribute.  

## GenerateStyleIdAttribute(string value)

**[System.Xml.XmlAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlattribute)**  

Returns an ss:StyleID attribute.  

## GenerateTextCell(string data)

**[System.Xml.XmlNode](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlnode)**  

Generate a paragraph Cell tag containing the specified text.  

## GenerateTextCell(string data, string styleId = null)

**[System.Xml.XmlNode](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlnode)**  

Generate a Cell tag containing the specified text.  

## GenerateTypeAttribute(string value)

**[System.Xml.XmlAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlattribute)**  

Returns a custom ss:Type attribute.  

## GenerateWidthAttribute(int value)

**[System.Xml.XmlAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlattribute)**  

Returns an ss:Width attribute.  

## GetColumnCount(int tableIndex)

**int**  

Returns the number of Columns in the specified Table.  

## GetColumnValues(int tableIndex, string header)

**[List&lt;string&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

Returns a list of values from the Cells in the column with the specified header.  

**Remarks:**  
The first row is skipped as the header row.  

## GetColumnValues(int tableIndex, int columnIndex, bool firstRowIsHeader = True)

**[List&lt;string&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

Returns a list of values from the cells in the selected column.  

**Parameters:**  
* **int tableIndex**: 0-based table index within Workbook.  
* **int columnIndex**: 0-based column index within Table.  
* **bool firstRowIsHeader**: If true, the first row of the table is skipped.  

## GetHeaderIndex(int tableIndex, string header)

**int**  

Returns zero-based index of the column with the selected header.  
Returns -1 if header is not found.  

**Remarks:**  
Only the first row in the table is searched for the header.  

## GetHeaders(int tableIndex)

**[List&lt;string&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

Returns all the header values from the first row.  

## GetRowCount(int tableIndex)

**int**  

Returns the number of Rows in the specified Table.  

## GetRowValues(int tableIndex, int rowIndex)

**[List&lt;string&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

Returns a list of values from the Cells in the specified Row.  

**Parameters:**  
* **int tableIndex**: Zero-based index of Table in Workbook.  
* **int rowIndex**: Zero-based index of Row in Table.  

## GetTableIndex(string title)

**int**  

Returns the index of the Table in the Worksheet with the specified title.  

## RemoveStyle(string id)

**void**  

Removes a Style.  

## Save(string fullPath)

**void**  

Save file.  

## SetColumnWidths(int tableIndex, [List&lt;int&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) widths)

**void**  

Set column widths on the specified Table. Overwrites column widths if they were already set.  

# Static Methods

## DateToString([DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime) date)

**static string**  

Converts a date to the expected string format.  

