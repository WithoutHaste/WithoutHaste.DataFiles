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

## [System.Xml.XmlNode](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlnode) StylesNode { get; protected set; }

## [System.Xml.XmlNode[]](https://docs.microsoft.com/en-us/dotnet/api/system.array) Tables { get; }

## [System.Xml.XmlNode](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlnode) WorkbookNode { get; protected set; }

## [System.Xml.XmlDocument](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmldocument) XmlDocument { get; protected set; }

# Constructors

## MSExcel2003XmlFile()

Sets up a default file containing no Worksheets.  

## MSExcel2003XmlFile(string fullPath)

Load from file.  

# Methods

## void AddColumns(int tableIndex, [List&lt;List&lt;object&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) columns)

Creates Cells for each data values, adds sufficient Rows to the the specified Table to contain all the columns. Add all Cells to the Rows.  

**Parameters:**  
* **int tableIndex**: 0-based table index within Workbook.  
* **[List&lt;List&lt;object&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) columns**: List of columns, each column being a list of values.  

## void AddColumns(int tableIndex, [List&lt;List&lt;System.Xml.XmlNode&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) columns)

Adds sufficient Rows to the the specified Table to contain all the columns. Add all Cells to the Rows.  

**Parameters:**  
* **int tableIndex**: 0-based table index within Workbook.  
* **[List&lt;List&lt;System.Xml.XmlNode&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) columns**: List of columns, each column being a list of "Cell" tags.  

## void AddHeaderRow(int tableIndex, [List&lt;string&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) headers)

Adds a Row of header-style Cells to the specified Table.  

## void AddRow(int tableIndex, [List&lt;object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) cellValues)

Creates Cells of the appropriate type for each value, and adds all Cells to a new Row in the specified Table.  

## void AddRow(int tableIndex, [List&lt;System.Xml.XmlNode&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) cellNodes)

Adds all Cells to a new Row in the specified Table.  

## void AddStyle(string id, string childName, string childAttributeName, string childAttributeValue)

Adds a Style. If a Style with the same id already exists, it is overwritten.  

**Example A:**  

```xml
<Styles>
  <Style ss:ID="id">
    <childName ss:childNameAttribute="childAttributeValue" />
  </Style>
</Styles>
```  

## int AddWorksheet(string title)

Add a Worksheet to the end of the list of Worksheets, containing an empty Table.  

**Returns:**  
The index of the worksheet/table.  

**Parameters:**  
* **string title**: Worksheet title.  

## [System.Xml.XmlAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlattribute) GenerateAttribute(string prefix, string name, string value)

Returns a custom attribute.  

**Remarks:**  
Only namespace URIs known to this format can be referenced.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Unknown namespace URI prefix.  

## [System.Xml.XmlAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlattribute) GenerateAttribute(string prefix, string uri, string name, string value)

Returns a custom attribute.  

## [System.Xml.XmlNode](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlnode) GenerateCell(object data)

Creates a Cell tag of the appropriate type containing the specified data.  
Supports DateTime cells, Number cells, and Text cells. All unknown types are converted to strings.  

## [System.Xml.XmlNode](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlnode) GenerateDateCell([DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime) data)

Creates a Cell tag containing the specified date.  

## [System.Xml.XmlAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlattribute) GenerateDateTypeAttribute()

Returns an ss:Type="DateTime" attribute.  

## [System.Xml.XmlNode](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlnode) GenerateEmptyCell()

Generate an empty Cell tag.  

## [System.Xml.XmlNode](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlnode) GenerateHeaderCell(string data)

Generate a header Cell tag containing the specified text.  

## [System.Xml.XmlAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlattribute) GenerateNameAttribute(string value)

Returns an ss:Name attribute.  

## [System.Xml.XmlNode](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlnode) GenerateNumberCell(int data)

Generate a Cell tag containing the specified number.  

## [System.Xml.XmlAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlattribute) GenerateNumberTypeAttribute()

Returns an ss:Type="Number" attribute.  

## [System.Xml.XmlNode](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlnode) GenerateParagraphCell(string data)

Generate a paragraph Cell tag containing the specified text.  

## [System.Xml.XmlAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlattribute) GenerateStringTypeAttribute()

Returns an ss:Type="String" attribute.  

## [System.Xml.XmlAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlattribute) GenerateStyleIdAttribute(string value)

Returns an ss:StyleID attribute.  

## [System.Xml.XmlNode](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlnode) GenerateTextCell(string data)

Generate a paragraph Cell tag containing the specified text.  

## [System.Xml.XmlNode](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlnode) GenerateTextCell(string data, string styleId = null)

Generate a Cell tag containing the specified text.  

## [System.Xml.XmlAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlattribute) GenerateTypeAttribute(string value)

Returns a custom ss:Type attribute.  

## [System.Xml.XmlAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlattribute) GenerateWidthAttribute(int value)

Returns an ss:Width attribute.  

## int GetColumnCount(int tableIndex)

Returns the number of Columns in the specified Table.  

## [List&lt;string&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) GetColumnValues(int tableIndex, string header)

Returns a list of values from the Cells in the column with the specified header.  

**Remarks:**  
The first row is skipped as the header row.  

## [List&lt;string&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) GetColumnValues(int tableIndex, int columnIndex, bool firstRowIsHeader = True)

Returns a list of values from the cells in the selected column.  

**Parameters:**  
* **int tableIndex**: 0-based table index within Workbook.  
* **int columnIndex**: 0-based column index within Table.  
* **bool firstRowIsHeader**: If true, the first row of the table is skipped.  

## int GetHeaderIndex(int tableIndex, string header)

Returns zero-based index of the column with the selected header.  
Returns -1 if header is not found.  

**Remarks:**  
Only the first row in the table is searched for the header.  

## [List&lt;string&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) GetHeaders(int tableIndex)

Returns all the header values from the first row.  

## int GetRowCount(int tableIndex)

Returns the number of Rows in the specified Table.  

## [List&lt;string&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) GetRowValues(int tableIndex, int rowIndex)

Returns a list of values from the Cells in the specified Row.  

**Parameters:**  
* **int tableIndex**: Zero-based index of Table in Workbook.  
* **int rowIndex**: Zero-based index of Row in Table.  

## int GetTableIndex(string title)

Returns the index of the Table in the Worksheet with the specified title.  

## void RemoveStyle(string id)

Removes a Style.  

## void Save(string fullPath)

Save file.  

## void SetColumnWidths(int tableIndex, [List&lt;int&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) widths)

Set column widths on the specified Table. Overwrites column widths if they were already set.  

# Static Methods

## static string DateToString([DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime) date)

Converts a date to the expected string format.  

