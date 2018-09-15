using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WithoutHaste.DataFiles
{
	/// <summary>
	/// XmlDocument utilities specific to the Microsoft Excel 2003 Xml file format.
	/// </summary>
	/// <example>
	/// Format:
	/// <ss:Workbook>
	///   <ss:Worksheet>
	///     <Table>
	///       <Column />
	///       <Row>
	///         <Cell>
	///           <Data>
	///             Cell Value
	///           </Data>
	///         </Cell>
	///       </Row>
	///     </Table>
	///   </ss:Worksheet>
	/// </ss:Workbook>
	/// </example>
	public static class MSExcel2003XmlFormat
	{
		private const string WORKBOOK_NODE_LOCALNAME = "Workbook";
		private const string WORKSHEET_NODE_LOCALNAME = "Worksheet";
		private const string TABLE_NODE_LOCALNAME = "Table";
		private const string WORKSHEETOPTIONS_NODE_LOCALNAME = "WorksheetOptions";
		private const string COLUMN_NODE_LOCALNAME = "Column";
		private const string ROW_NODE_LOCALNAME = "Row";
		private const string CELL_NODE_LOCALNAME = "Cell";
		private const string DATA_NODE_LOCALNAME = "Data";

		private const string STYLEID_ATTRIBUTE = "StyleID";
		private const string TYPE_ATTRIBUTE = "Type";
		private const string WIDTH_ATTRIBUTE = "Width";
		private const string NAME_ATTRIBUTE = "Name";

		private const string CELLSTYLEID_SHORTDATE = "cellShortDate";
		private const string CELLSTYLEID_HEADER = "header";
		private const string CELLSTYLEID_PARAGRAPH = "paragraph";

		private static readonly Dictionary<string, string> PREFIX_NAMESPACEURI = new Dictionary<string, string>() {
			{ "ss", "urn:schemas-microsoft-com:office:spreadsheet" },
			{ "x", "urn:schemas-microsoft-com:office:excel" }
		};

		/// <summary>
		/// Generate a default file with no Worksheets. Return Workbook.
		/// </summary>
		public static XmlNode GenerateDefaultWorkbook(XmlDocument xmlDocument)
		{
			XmlNode versionNode = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
			xmlDocument.AppendChild(versionNode);

			XmlDocumentFragment applicationNode = xmlDocument.CreateDocumentFragment();
			applicationNode.InnerXml = "<?mso-application progid=\"Excel.Sheet\"?>";
			xmlDocument.AppendChild(applicationNode);

			XmlDocumentFragment workbookFragment = xmlDocument.CreateDocumentFragment();
			workbookFragment.InnerXml = "<Workbook xmlns:c='urn:schemas-microsoft-com:office:component:spreadsheet' xmlns:html='http://www.w3.org/TR/REC-html40' xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns='urn:schemas-microsoft-com:office:spreadsheet' xmlns:x2='http://schemas.microsoft.com/office/excel/2003/xml' xmlns:ss='urn:schemas-microsoft-com:office:spreadsheet' xmlns:x='urn:schemas-microsoft-com:office:excel'></Workbook>";
			xmlDocument.AppendChild(workbookFragment);

			XmlNode workbookNode = GetWorkbookNode(xmlDocument);

			//c# notes: passing workbookNode.NamespaceURI (the root's namespace) into child that do not need any namespace so that an empty xmlns="" is not added to that tag
			XmlNode stylesNode = xmlDocument.CreateElement("Styles", workbookNode.NamespaceURI);
			workbookNode.AppendChild(stylesNode);

			string shortDateStyleId = "cellShortDate";
			XmlNode shortDateStyleNode = xmlDocument.CreateElement("Style", workbookNode.NamespaceURI);
			XmlAttribute shortDateAttribute = xmlDocument.CreateAttribute("ss", "ID", "urn:schemas-microsoft-com:office:spreadsheet");
			shortDateAttribute.Value = shortDateStyleId;
			shortDateStyleNode.Attributes.Append(shortDateAttribute);
			stylesNode.AppendChild(shortDateStyleNode);
			XmlNode numberFormatNode = xmlDocument.CreateElement("NumberFormat", workbookNode.NamespaceURI);
			XmlAttribute numberFormatAttribute = xmlDocument.CreateAttribute("ss", "Format", "urn:schemas-microsoft-com:office:spreadsheet");
			numberFormatAttribute.Value = "Short Date";
			numberFormatNode.Attributes.Append(numberFormatAttribute);
			shortDateStyleNode.AppendChild(numberFormatNode);

			string headerStyleId = "header";
			XmlNode headerStyleNode = xmlDocument.CreateElement("Style", workbookNode.NamespaceURI);
			XmlAttribute headerAttribute = xmlDocument.CreateAttribute("ss", "ID", "urn:schemas-microsoft-com:office:spreadsheet");
			headerAttribute.Value = headerStyleId;
			headerStyleNode.Attributes.Append(headerAttribute);
			stylesNode.AppendChild(headerStyleNode);
			XmlNode fontFormatNode = xmlDocument.CreateElement("Font", workbookNode.NamespaceURI);
			XmlAttribute fontFormatAttribute = xmlDocument.CreateAttribute("ss", "Bold", "urn:schemas-microsoft-com:office:spreadsheet");
			fontFormatAttribute.Value = "1";
			fontFormatNode.Attributes.Append(fontFormatAttribute);
			headerStyleNode.AppendChild(fontFormatNode);

			string paragraphStyleId = "paragraph";
			XmlNode paragraphStyleNode = xmlDocument.CreateElement("Style", workbookNode.NamespaceURI);
			XmlAttribute paragraphAttribute = xmlDocument.CreateAttribute("ss", "ID", "urn:schemas-microsoft-com:office:spreadsheet");
			paragraphAttribute.Value = paragraphStyleId;
			paragraphStyleNode.Attributes.Append(paragraphAttribute);
			stylesNode.AppendChild(paragraphStyleNode);
			XmlNode alignmentNode = xmlDocument.CreateElement("Alignment", workbookNode.NamespaceURI);
			XmlAttribute alignmentAttribute = xmlDocument.CreateAttribute("ss", "WrapText", "urn:schemas-microsoft-com:office:spreadsheet");
			alignmentAttribute.Value = "1";
			alignmentNode.Attributes.Append(alignmentAttribute);
			paragraphStyleNode.AppendChild(alignmentNode);

			return workbookNode;
		}

		/// <summary>
		/// Generates a Row tag of header Cells based on the provided headers, and adds it to the Table tag.
		/// </summary>
		/// <param name="tableNode">Expects "Table" tag.</param>
		/// <returns>Returns new Row tag.</returns>
		/// <exception cref="XmlNodeException">If any XmlNode is null or has the wrong tag name.</exception>
		public static XmlNode AddHeaderRowToTable(XmlDocument xmlDocument, XmlNode tableNode, List<string> headers)
		{
			return AddRowToTable(xmlDocument, tableNode, headers.Select(h => GenerateHeaderCell(xmlDocument, h)).ToList());
		}

		/// <summary>
		/// Generates a Row tag containing all the provided Cell tags, and adds it to the Table tag.
		/// </summary>
		/// <param name="tableNode">Expects "Table" tag.</param>
		/// <param name="cellNodes">Expects "Cell" tags.</param>
		/// <returns>Returns new Row tag.</returns>
		/// <exception cref="XmlNodeException">If any XmlNode is null or has the wrong tag name.</exception>
		public static XmlNode AddRowToTable(XmlDocument xmlDocument, XmlNode tableNode, List<XmlNode> cellNodes)
		{
			string rootNamespace = GetRootNamespace(xmlDocument);

			tableNode.Validate(TABLE_NODE_LOCALNAME);

			XmlNode rowNode = xmlDocument.CreateElement(ROW_NODE_LOCALNAME, rootNamespace);
			tableNode.AppendChild(rowNode);

			foreach(XmlNode cellNode in cellNodes)
			{
				cellNode.Validate(CELL_NODE_LOCALNAME);
				rowNode.AppendChild(cellNode);
			}

			return rowNode;
		}

		/// <summary>
		/// Adds Column tags to Table, specifying column widths.
		/// Only call this once per Table.
		/// </summary>
		/// <param name="tableNode">Expects "Table" tag.</param>
		/// <exception cref="XmlNodeException">If any XmlNode is null or has the wrong tag name.</exception>
		public static void AddColumnWidths(XmlDocument xmlDocument, XmlNode tableNode, List<int> widths)
		{
			string rootNamespace = GetRootNamespace(xmlDocument);

			tableNode.Validate(TABLE_NODE_LOCALNAME);

			foreach(int width in widths)
			{
				XmlNode columnNode = xmlDocument.CreateElement(COLUMN_NODE_LOCALNAME, rootNamespace);
				columnNode.Attributes.Append(GenerateWidthAttribute(xmlDocument, width));
				tableNode.AppendChild(columnNode);
			}
		}

		/// <summary>
		/// Generates Row tags as needed and adds then to the Table. Adds all columns of cells to the Table.
		/// </summary>
		/// <param name="tableNode">Expects "Table" tag.</param>
		/// <param name="columns">List of columns, each column being a list of "Cell" tags.</param>
		/// <exception cref="XmlNodeException">If any XmlNode is null or has the wrong tag name.</exception>
		public static void AddColumnsToTable(XmlDocument xmlDocument, XmlNode tableNode, List<List<XmlNode>> columns)
		{
			string rootNamespace = GetRootNamespace(xmlDocument);

			tableNode.Validate(TABLE_NODE_LOCALNAME);

			//pad data so all columns are the same height
			int maxRowCount = columns.Max(column => column.Count());
			foreach(List<XmlNode> column in columns)
			{
				while(column.Count < maxRowCount)
				{
					column.Add(GenerateEmptyCell(xmlDocument));
				}
			}

			//generate rows
			int rowIndex = 0;
			while(rowIndex < columns[0].Count)
			{
				XmlNode rowNode = xmlDocument.CreateElement(ROW_NODE_LOCALNAME, rootNamespace);
				tableNode.AppendChild(rowNode);

				foreach(List<XmlNode> column in columns)
				{
					rowNode.AppendChild(column[rowIndex]);
				}

				rowIndex++;
			}
		}

		/// <summary>
		/// Returns zero-based index of the column with the selected header.
		/// Returns -1 if header is not found.
		/// </summary>
		/// <remarks>Only the first row in the table is searched for the header.</remarks>
		/// <param name="tableNode">Expects "Table" tag.</param>
		/// <exception cref="XmlNodeException">If the XmlNode is null or has the wrong tag name.</exception>
		public static int GetHeaderIndex(XmlNode tableNode, string header)
		{
			List<string> headers = GetHeaders(tableNode);
			if(headers.Contains(header))
				return headers.IndexOf(header);
			return -1;
		}

		/// <summary>
		/// Returns all the header values from the first row.
		/// </summary>
		/// <param name="tableNode">Expects "Table" tag.</param>
		/// <exception cref="XmlNodeException">If the XmlNode is null or has the wrong tag name.</exception>
		public static List<string> GetHeaders(XmlNode tableNode)
		{
			tableNode.Validate(TABLE_NODE_LOCALNAME);

			List<string> headers = new List<string>();
			foreach(XmlNode rowNode in tableNode.ChildNodes.Cast<XmlNode>().Where(node => node.LocalName == ROW_NODE_LOCALNAME))
			{
				foreach(XmlNode cellNode in rowNode.ChildNodes.Cast<XmlNode>().Where(node => node.LocalName == CELL_NODE_LOCALNAME))
				{
					headers.Add(GetCellValue(cellNode));
				}
				break;
			}
			return headers;
		}

		/// <summary>
		/// Returns a list of values from the cells in the selected column.
		/// </summary>
		/// <remarks>The first row is skipped as the header row.</remarks>
		/// <param name="tableNode">Expects "Table" tag.</param>
		/// <exception cref="XmlNodeException">If the XmlNode is null or has the wrong tag name.</exception>
		public static List<string> GetColumnValues(XmlNode tableNode, string header)
		{
			tableNode.Validate(TABLE_NODE_LOCALNAME);

			int columnIndex = GetHeaderIndex(tableNode, header);
			if(columnIndex == -1)
				return new List<string>();

			return GetColumnValues(tableNode, columnIndex, firstRowIsHeader: true);
		}

		/// <summary>
		/// Returns a list of values from the cells in the selected column.
		/// </summary>
		/// <param name="tableNode">Expects "Table" tag.</param>
		/// <param name="firstRowIsHeader">If true, the first row of the table is skipped.</param>
		/// <remarks>Uses zero-based index. The first row is skipped as a header row.</remarks>
		public static List<string> GetColumnValues(XmlNode tableNode, int columnIndex, bool firstRowIsHeader = true)
		{
			List<string> values = new List<string>();
			bool foundHeaderRow = false;
			foreach(XmlNode rowNode in tableNode.ChildNodes.Cast<XmlNode>().Where(node => node.LocalName == ROW_NODE_LOCALNAME))
			{
				if(!foundHeaderRow)
				{
					foundHeaderRow = true;
					continue;
				}

				int i = 0;
				foreach(XmlNode cellNode in rowNode.ChildNodes.Cast<XmlNode>().Where(node => node.LocalName == CELL_NODE_LOCALNAME))
				{
					if(i < columnIndex)
					{
						i++;
						continue;
					}

					values.Add(GetCellValue(cellNode));
					break;
				}
			}
			return values;
		}

		/// <summary>
		/// Returns a list of values from the cells in the Row.
		/// </summary>
		/// <param name="rowNode">Expects "Row" tag.</param>
		/// <exception cref="XmlNodeException">If the XmlNode is null or has the wrong tag name.</exception>
		public static List<string> GetRowValues(XmlNode rowNode)
		{
			rowNode.Validate(ROW_NODE_LOCALNAME);

			List<string> values = new List<string>();
			foreach(XmlNode cellNode in rowNode.ChildNodes.Cast<XmlNode>().Where(node => node.LocalName == CELL_NODE_LOCALNAME))
			{
				values.Add(GetCellValue(cellNode));
			}
			return values;
		}

		/// <summary>
		/// Returns the value stored in the Data node in a Cell node. Returns null if no value is found.
		/// </summary>
		/// <param name="cellNode">Expects "Cell" tag.</param>
		/// <exception cref="XmlNodeException">If the XmlNode is null or has the wrong tag name.</exception>
		public static string GetCellValue(XmlNode cellNode)
		{
			cellNode.Validate(CELL_NODE_LOCALNAME);

			foreach(XmlNode dataNode in cellNode.ChildNodes.Cast<XmlNode>().Where(node => node.LocalName == DATA_NODE_LOCALNAME))
			{
				return dataNode.InnerText;
			}
			return null;
		}

		/// <summary>
		/// Adds new Worksheet tag with the specified title, and nest Table tag, to the Workbook. Returns the Table.
		/// </summary>
		/// <param name="workbookNode">Expects "Workbook" tag.</param>
		/// <exception cref="XmlNodeException">If the XmlNode is null or has the wrong tag name.</exception>
		public static XmlNode AddWorksheetAndTableToWorkbook(XmlDocument xmlDocument, XmlNode workbookNode, string title)
		{
			XmlNode worksheetNode = AddWorksheetToWorkbook(xmlDocument, workbookNode, title);
			return AddTableToWorksheet(xmlDocument, worksheetNode);
		}

		/// <summary>
		/// Adds new Worksheet tag with the specified title to the Workbook. Returns the Worksheet.
		/// </summary>
		/// <param name="workbookNode">Expects "Workbook" tag.</param>
		/// <exception cref="XmlNodeException">If the XmlNode is null or has the wrong tag name.</exception>
		private static XmlNode AddWorksheetToWorkbook(XmlDocument xmlDocument, XmlNode workbookNode, string title)
		{
			workbookNode.Validate(WORKBOOK_NODE_LOCALNAME);

			XmlNode worksheetNode = xmlDocument.CreateElement("ss", WORKSHEET_NODE_LOCALNAME, PREFIX_NAMESPACEURI["ss"]);
			XmlAttribute titleAttribute = xmlDocument.CreateAttribute("ss", NAME_ATTRIBUTE, PREFIX_NAMESPACEURI["ss"]);
			titleAttribute.Value = title;
			worksheetNode.Attributes.Append(titleAttribute);
			workbookNode.AppendChild(worksheetNode);
			return worksheetNode;
		}

		/// <summary>
		/// Adds new Table tag to the Worksheet. Returns the Table.
		/// Only call this once per Worksheet.
		/// </summary>
		/// <param name="worksheetNode">Expects "Worksheet" tag.</param>
		/// <exception cref="XmlNodeException">If the XmlNode is null or has the wrong tag name.</exception>
		private static XmlNode AddTableToWorksheet(XmlDocument xmlDocument, XmlNode worksheetNode)
		{
			worksheetNode.Validate(WORKSHEET_NODE_LOCALNAME);

			string rootNamespace = GetRootNamespace(xmlDocument);

			XmlNode tableNode = xmlDocument.CreateElement(TABLE_NODE_LOCALNAME, rootNamespace);
			worksheetNode.AppendChild(tableNode);

			XmlNode optionsNode = xmlDocument.CreateElement("x", WORKSHEETOPTIONS_NODE_LOCALNAME, PREFIX_NAMESPACEURI["x"]);
			worksheetNode.AppendChild(optionsNode);

			return tableNode;
		}

		/// <summary>
		/// Returns a list of Row tags from the Table.
		/// </summary>
		/// <param name="tableNode">Expects "Table" tag.</param>
		public static List<XmlNode> GetRows(XmlNode tableNode, bool skipFirstRow = true)
		{
			tableNode.Validate(TABLE_NODE_LOCALNAME);

			List<XmlNode> rowNodes = tableNode.ChildNodes.Cast<XmlNode>().Where(node => node.LocalName == ROW_NODE_LOCALNAME).ToList();
			if(skipFirstRow && rowNodes.Count > 0)
			{
				return rowNodes.Skip(1).ToList();
			}
			return rowNodes;
		}

		/// <summary>Creates a Cell tag containing the specified date.</summary>
		public static XmlNode GenerateDateCell(XmlDocument xmlDocument, DateTime data)
		{
			string rootNamespace = GetRootNamespace(xmlDocument);

			XmlNode cellNode = xmlDocument.CreateElement(CELL_NODE_LOCALNAME, rootNamespace);
			cellNode.Attributes.Append(GenerateStyleIdAttribute(xmlDocument, CELLSTYLEID_SHORTDATE));

			XmlNode dataNode = xmlDocument.CreateElement(DATA_NODE_LOCALNAME, rootNamespace);
			dataNode.Attributes.Append(GenerateDateTypeAttribute(xmlDocument));
			dataNode.InnerText = data.ToString("yyyy-MM-ddT00:00:00.000");

			cellNode.AppendChild(dataNode);

			return cellNode;
		}

		/// <summary>Generate a Cell tag containing the specified number.</summary>
		public static XmlNode GenerateNumberCell(XmlDocument xmlDocument, int data)
		{
			string rootNamespace = GetRootNamespace(xmlDocument);

			XmlNode cellNode = xmlDocument.CreateElement(CELL_NODE_LOCALNAME, rootNamespace);

			XmlNode dataNode = xmlDocument.CreateElement(DATA_NODE_LOCALNAME, rootNamespace);
			dataNode.Attributes.Append(GenerateNumberTypeAttribute(xmlDocument));
			dataNode.InnerText = data.ToString();

			cellNode.AppendChild(dataNode);

			return cellNode;
		}

		/// <summary>Generate a header Cell tag containing the specified text.</summary>
		public static XmlNode GenerateHeaderCell(XmlDocument xmlDocument, string data)
		{
			return GenerateTextCell(xmlDocument, data, CELLSTYLEID_HEADER);
		}

		/// <summary>Generate a paragraph Cell tag containing the specified text.</summary>
		public static XmlNode GenerateParagraphCell(XmlDocument xmlDocument, string data)
		{
			return GenerateTextCell(xmlDocument, data, CELLSTYLEID_PARAGRAPH);
		}

		/// <summary>Generate a paragraph Cell tag containing the specified text.</summary>
		public static XmlNode GenerateTextCell(XmlDocument xmlDocument, string data)
		{
			return GenerateTextCell(xmlDocument, data, styleId: null);
		}

		/// <summary>Generate an empty Cell tag.</summary>
		public static XmlNode GenerateEmptyCell(XmlDocument xmlDocument)
		{
			return GenerateTextCell(xmlDocument, data: "", styleId: null);
		}

		/// <summary>Generate a Cell tag containing the specified text.</summary>
		private static XmlNode GenerateTextCell(XmlDocument xmlDocument, string data, string styleId = null)
		{
			string rootNamespace = GetRootNamespace(xmlDocument);

			XmlNode cellNode = xmlDocument.CreateElement(CELL_NODE_LOCALNAME, rootNamespace);
			if(styleId != null)
			{
				cellNode.Attributes.Append(GenerateStyleIdAttribute(xmlDocument, styleId));
			}

			XmlNode dataNode = xmlDocument.CreateElement(DATA_NODE_LOCALNAME, rootNamespace);
			dataNode.Attributes.Append(GenerateStringTypeAttribute(xmlDocument));
			dataNode.InnerText = data;

			cellNode.AppendChild(dataNode);

			return cellNode;
		}

		/// <summary>Creates a StyleID Attribute with the specified value.</summary>
		public static XmlAttribute GenerateStyleIdAttribute(XmlDocument xmlDocument, string value)
		{
			return GenerateAttribute(xmlDocument, "ss", STYLEID_ATTRIBUTE, value);
		}

		/// <summary>Creates a Name Attribute with the specified value.</summary>
		public static XmlAttribute GenerateNameAttribute(XmlDocument xmlDocument, string value)
		{
			return GenerateAttribute(xmlDocument, "ss", NAME_ATTRIBUTE, value);
		}

		/// <summary>Creates an Width Attribute with the specified value.</summary>
		public static XmlAttribute GenerateWidthAttribute(XmlDocument xmlDocument, int value)
		{
			return GenerateAttribute(xmlDocument, "ss", WIDTH_ATTRIBUTE, value.ToString());
		}

		/// <summary>Creates a string Type Attribute.</summary>
		public static XmlAttribute GenerateStringTypeAttribute(XmlDocument xmlDocument)
		{
			return GenerateAttribute(xmlDocument, "ss", TYPE_ATTRIBUTE, "String");
		}

		/// <summary>Creates a number Type Attribute.</summary>
		public static XmlAttribute GenerateNumberTypeAttribute(XmlDocument xmlDocument)
		{
			return GenerateAttribute(xmlDocument, "ss", TYPE_ATTRIBUTE, "Number");
		}

		/// <summary>Creates a date Type Attribute.</summary>
		public static XmlAttribute GenerateDateTypeAttribute(XmlDocument xmlDocument)
		{
			return GenerateAttribute(xmlDocument, "ss", TYPE_ATTRIBUTE, "DateTime");
		}

		/// <summary>Creates an Type Attribute with the specified value.</summary>
		private static XmlAttribute GenerateTypeAttribute(XmlDocument xmlDocument, string value)
		{
			return GenerateAttribute(xmlDocument, "ss", TYPE_ATTRIBUTE, value);
		}

		/// <summary>Creates an Attribute with the specified namespace and value.</summary>
		private static XmlAttribute GenerateAttribute(XmlDocument xmlDocument, string prefix, string name, string value)
		{
			XmlAttribute attribute = xmlDocument.CreateAttribute(prefix, name, PREFIX_NAMESPACEURI[prefix]);
			attribute.Value = value;
			return attribute;
		}

		/// <summary>
		/// Returns the first Workbook tag from the document, or null if no matching tag is found.
		/// </summary>
		public static XmlNode GetWorkbookNode(XmlDocument xmlDocument)
		{
			return xmlDocument.GetElementsByTagName("Workbook").Cast<XmlNode>().Where(node => node.LocalName == WORKBOOK_NODE_LOCALNAME).FirstOrDefault();
		}

		/// <summary></summary>
		public static string GetRootNamespace(XmlDocument xmlDocument)
		{
			XmlNode workbookNode = GetWorkbookNode(xmlDocument);
			if(workbookNode == null)
				return null;
			return workbookNode.NamespaceURI;
		}
	}
}
