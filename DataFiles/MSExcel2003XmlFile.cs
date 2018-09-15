using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WithoutHaste.DataFiles
{
	/// <summary>
	/// Building a Microsoft Excel 2003 Xml file with XmlDocument.
	/// </summary>
	/// <example>
	/// Format:
	/// <? xml version="1.0" encoding="UTF-8"?>
	/// <? mso-application progid="Excel.Sheet" ?>
	/// <Workbook xmlns:c="urn:schemas-microsoft-com:office:component:spreadsheet" xmlns:html="http://www.w3.org/TR/REC-html40" xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns="urn:schemas-microsoft-com:office:spreadsheet" xmlns:x2="http://schemas.microsoft.com/office/excel/2003/xml" xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet" xmlns:x="urn:schemas-microsoft-com:office:excel">
	///   <ss:Worksheet ss:Name="Sheet1">
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
	public class MSExcel2003XmlFile
	{
		private static class LocalNames
		{
			public const string Workbook = "Workbook";
			public const string Worksheet = "Worksheet";
			public const string Styles = "Styles";
			public const string Style = "Style";
			public const string Table = "Table";
			public const string WorksheetOptions = "WorksheetOptions";
			public const string Column = "Column";
			public const string Row = "Row";
			public const string Cell = "Cell";
			public const string Data = "Data";
		}

		private static class Attributes
		{
			public const string StyleID = "StyleID";
			public const string Type = "Type";
			public const string Width = "Width";
			public const string Name = "Name";
		}

		private static class StyleIds
		{
			public const string ShortDate = "cellShortDate";
			public const string Header = "header";
			public const string Paragraph = "paragraph";
		}

		private const string ROOT_NAMESPACE = "urn:schemas-microsoft-com:office:spreadsheet";
		private static readonly Dictionary<string, string> PREFIX_NAMESPACEURI = new Dictionary<string, string>() {
			{ "c", "urn:schemas-microsoft-com:office:component:spreadsheet" },
			{ "html", "http://www.w3.org/TR/REC-html40" },
			{ "o", "urn:schemas-microsoft-com:office:office" },
			{ "ss", "urn:schemas-microsoft-com:office:spreadsheet" },
			{ "x", "urn:schemas-microsoft-com:office:excel" },
			{ "x2", "http://schemas.microsoft.com/office/excel/2003/xml" },
			{ "xsi", "http://www.w3.org/2001/XMLSchema-instance" }
		};

		//----------------------------------------------------

		public XmlDocument XmlDocument { get; protected set; }
		public XmlNode WorkbookNode { get; protected set; }
		public XmlNode StylesNode { get; protected set; }
		private List<XmlNode> tableNodes = new List<XmlNode>();
		public XmlNode[] Tables { get { return tableNodes.ToArray(); } }

		//----------------------------------------------------

		/// <summary>
		/// Sets up a default file containing no Worksheets.
		/// </summary>
		public MSExcel2003XmlFile()
		{
			XmlDocument = new XmlDocument();
			InitDeclarations();
			InitWorkbook();
			InitStyles();
		}

		/// <summary>
		/// Load from file.
		/// </summary>
		public MSExcel2003XmlFile(string fullPath)
		{
			XmlDocument = new XmlDocument();
			XmlDocument.Load(fullPath);
			//todo: do I need to verify and possibly add Declaration?
			if(!FindWorkbook())
				InitWorkbook();
			if(!FindStyles())
				InitStyles();
			FindTables();
		}

		#region Init

		private void InitDeclarations()
		{
			XmlNode versionNode = XmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
			XmlDocument.AppendChild(versionNode);

			XmlDocumentFragment applicationNode = XmlDocument.CreateDocumentFragment();
			applicationNode.InnerXml = "<?mso-application progid=\"Excel.Sheet\"?>";
			XmlDocument.AppendChild(applicationNode);
		}

		private void InitWorkbook()
		{
			WorkbookNode = XmlDocument.CreateElement(LocalNames.Workbook, ROOT_NAMESPACE);

			foreach(KeyValuePair<string, string> pair in PREFIX_NAMESPACEURI)
			{
				XmlAttribute attribute = XmlDocument.CreateAttribute("xmlns", pair.Key);
				attribute.Value = pair.Value;
				WorkbookNode.Attributes.Append(attribute);

			}
		}

		private void InitStyles()
		{
			StylesNode = XmlDocument.CreateElement(LocalNames.Styles, ROOT_NAMESPACE);
			WorkbookNode.AppendChild(StylesNode);

			AddStyle(StyleIds.ShortDate, "NumberFormat", "Format", "Short Date");
			AddStyle(StyleIds.Header, "Font", "Bold", "1");
			AddStyle(StyleIds.Paragraph, "Alignment", "WrapText", "1");
		}

		#endregion

		#region Find

		private bool FindWorkbook()
		{
			XmlNodeList workbookNodes = XmlDocument.GetElementsByTagName(LocalNames.Workbook);
			WorkbookNode = workbookNodes.Cast<XmlNode>().FirstOrDefault();
			return (WorkbookNode != null);
		}

		private bool FindStyles()
		{
			XmlNodeList stylesNodes = XmlDocument.GetElementsByTagName(LocalNames.Styles);
			StylesNode = stylesNodes.Cast<XmlNode>().FirstOrDefault();
			return (StylesNode != null);
		}

		private void FindTables()
		{
			tableNodes = XmlDocument.GetElementsByTagName(LocalNames.Table).Cast<XmlNode>().ToList();
		}

		#endregion

		/// <summary>
		/// Returns the index of the Table in the Worksheet with the specified title.
		/// </summary>
		public int GetTableIndex(string title)
		{
			int index = 0;
			foreach(XmlNode node in WorkbookNode.ChildNodes.Cast<XmlNode>().Where(node => node.LocalName == LocalNames.Worksheet))
			{
				if(node.Attributes["ss:Name"].Value == title)
				{
					return index;
				}
				index++;
			}
			return -1;
		}

		/// <summary>
		/// Add a Style.
		/// </summary>
		/// <example>
		/// <Styles>
		///   <Style ss:ID="id">
		///     <childName ss:childNameAttribute="childAttributeValue" />
		///   </Style>
		/// </Styles>
		/// </example>
		public void AddStyle(string id, string childName, string childAttributeName, string childAttributeValue)
		{
			XmlNode styleNode = XmlDocument.CreateElement(LocalNames.Style, ROOT_NAMESPACE);
			StylesNode.AppendChild(styleNode);

			XmlAttribute idAttribute = XmlDocument.CreateAttribute("ss", "ID", PREFIX_NAMESPACEURI["ss"]);
			idAttribute.Value = id;
			styleNode.Attributes.Append(idAttribute);

			XmlNode childNode = XmlDocument.CreateElement(childName, ROOT_NAMESPACE);
			XmlAttribute childAttribute = XmlDocument.CreateAttribute("ss", childAttributeName, PREFIX_NAMESPACEURI["ss"]);
			childAttribute.Value = childAttributeValue;
			childNode.Attributes.Append(childAttribute);
			styleNode.AppendChild(childNode);
		}

		/// <summary>
		/// Add a Worksheet to the end of the list of Worksheets, containing an empty Table.
		/// </summary>
		/// <param name="title">Worksheet title.</param>
		/// <returns>The index of the worksheet/table.</returns>
		public int AddWorksheet(string title)
		{
			XmlNode worksheetNode = XmlDocument.CreateElement("ss", LocalNames.Worksheet, PREFIX_NAMESPACEURI["ss"]);
			WorkbookNode.AppendChild(worksheetNode);

			XmlAttribute titleAttribute = XmlDocument.CreateAttribute("ss", Attributes.Name, PREFIX_NAMESPACEURI["ss"]);
			titleAttribute.Value = title;
			worksheetNode.Attributes.Append(titleAttribute);

			XmlNode tableNode = XmlDocument.CreateElement(LocalNames.Table, ROOT_NAMESPACE);
			worksheetNode.AppendChild(tableNode);
			tableNodes.Add(tableNode);

			XmlNode optionsNode = XmlDocument.CreateElement("x", LocalNames.WorksheetOptions, PREFIX_NAMESPACEURI["x"]);
			worksheetNode.AppendChild(optionsNode);

			return tableNodes.Count - 1;
		}

		/// <summary>
		/// Set column widths on the specified Table.
		/// </summary>
		public void SetColumnWidths(int tableIndex, List<int> widths)
		{
			ValidateTableIndex(tableIndex);
			RemoveColumns(tableIndex);
			foreach(int width in widths)
			{
				XmlNode columnNode = XmlDocument.CreateElement(LocalNames.Column, ROOT_NAMESPACE);
				columnNode.Attributes.Append(GenerateWidthAttribute(width));
				tableNodes[tableIndex].AppendChild(columnNode);
			}
		}

		/// <summary>
		/// Adds a Row of header-style Cells to the specified Table.
		/// </summary>
		public void AddHeaderRow(int tableIndex, List<string> headers)
		{
			AddRow(tableIndex, headers.Select(h => GenerateHeaderCell(h)).ToList());
		}

		/// <summary>
		/// Adds all Cells to a new Row in the specified Table.
		/// </summary>
		public void AddRow(int tableIndex, List<XmlNode> cellNodes)
		{
			ValidateTableIndex(tableIndex);
			XmlNode rowNode = XmlDocument.CreateElement(LocalNames.Row, ROOT_NAMESPACE);
			tableNodes[tableIndex].AppendChild(rowNode);

			foreach(XmlNode cellNode in cellNodes)
			{
				cellNode.Validate(LocalNames.Cell);
				rowNode.AppendChild(cellNode);
			}
		}

		/// <summary>
		/// Adds sufficient Rows to the the specified Table to contain all the columns. Add all Cells to the Rows.
		/// </summary>
		/// <param name="columns">List of columns, each column being a list of "Cell" tags.</param>
		public void AddColumns(int tableIndex, List<List<XmlNode>> columns)
		{
			ValidateTableIndex(tableIndex);

			//pad data so all columns are the same height
			int maxRowCount = columns.Max(column => column.Count());
			foreach(List<XmlNode> column in columns)
			{
				while(column.Count < maxRowCount)
				{
					column.Add(GenerateEmptyCell());
				}
			}

			//generate rows
			int rowIndex = 0;
			while(rowIndex < columns[0].Count)
			{
				XmlNode rowNode = XmlDocument.CreateElement(LocalNames.Row, ROOT_NAMESPACE);
				tableNodes[tableIndex].AppendChild(rowNode);

				foreach(List<XmlNode> column in columns)
				{
					rowNode.AppendChild(column[rowIndex]);
				}

				rowIndex++;
			}
		}

		/// <summary>
		/// Returns a list of values from the Cells in the specified Row.
		/// </summary>
		/// <param name="tableIndex">Zero-based index of Table in Workbook.</param>
		/// <param name="rowIndex">Zero-based index of Row in Table.</param>
		public List<string> GetRowValues(int tableIndex, int rowIndex)
		{
			ValidateTableIndex(tableIndex);
			ValidateRowIndex(tableIndex, rowIndex);

			XmlNode rowNode = GetRows(tableIndex)[rowIndex];
			List<XmlNode> cellNodes = rowNode.ChildNodes.Cast<XmlNode>().Where(node => node.LocalName == LocalNames.Cell).ToList();
			return cellNodes.Select(node => GetCellValue(node)).ToList();
		}

		/// <summary>
		/// Returns the number of Rows in the specified Table.
		/// </summary>
		public int GetRowCount(int tableIndex)
		{
			return GetRows(tableIndex).Count;
		}

		/// <summary>
		/// Returns a list of values from the Cells in the column with the specified header.
		/// </summary>
		/// <remarks>The first row is skipped as the header row.</remarks>
		public List<string> GetColumnValues(int tableIndex, string header)
		{
			int headerIndex = GetHeaderIndex(tableIndex, header);
			if(headerIndex == -1)
				return new List<string>();

			return GetColumnValues(tableIndex, headerIndex, firstRowIsHeader: true);
		}

		/// <summary>
		/// Returns zero-based index of the column with the selected header.
		/// Returns -1 if header is not found.
		/// </summary>
		/// <remarks>Only the first row in the table is searched for the header.</remarks>
		public int GetHeaderIndex(int tableIndex, string header)
		{
			return (GetHeaders(tableIndex)).IndexOf(header);
		}

		/// <summary>
		/// Returns all the header values from the first row.
		/// </summary>
		public List<string> GetHeaders(int tableIndex)
		{
			return GetRowValues(tableIndex, 0);
		}

		/// <summary>
		/// Returns a list of values from the cells in the selected column.
		/// </summary>
		/// <param name="firstRowIsHeader">If true, the first row of the table is skipped.</param>
		public List<string> GetColumnValues(int tableIndex, int columnIndex, bool firstRowIsHeader = true)
		{
			ValidateTableIndex(tableIndex);

			List<string> values = new List<string>();
			bool foundHeaderRow = false;
			foreach(XmlNode rowNode in tableNodes[tableIndex].ChildNodes.Cast<XmlNode>().Where(node => node.LocalName == LocalNames.Row))
			{
				if(!foundHeaderRow)
				{
					foundHeaderRow = true;
					continue;
				}

				int i = 0;
				foreach(XmlNode cellNode in rowNode.ChildNodes.Cast<XmlNode>().Where(node => node.LocalName == LocalNames.Cell))
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

		public void Save(string fullPath)
		{
			XmlDocument.Save(fullPath);
		}

		#region Generate

		public XmlAttribute GenerateWidthAttribute(int value)
		{
			return GenerateAttribute("ss", Attributes.Width, value.ToString());
		}

		public XmlAttribute GenerateStyleIdAttribute(string value)
		{
			return GenerateAttribute("ss", Attributes.StyleID, value);
		}

		public XmlAttribute GenerateNameAttribute(string value)
		{
			return GenerateAttribute("ss", Attributes.Name, value);
		}

		public XmlAttribute GenerateStringTypeAttribute()
		{
			return GenerateAttribute("ss", Attributes.Type, "String");
		}

		public XmlAttribute GenerateNumberTypeAttribute()
		{
			return GenerateAttribute("ss", Attributes.Type, "Number");
		}

		public XmlAttribute GenerateDateTypeAttribute()
		{
			return GenerateAttribute("ss", Attributes.Type, "DateTime");
		}

		public XmlAttribute GenerateTypeAttribute(string value)
		{
			return GenerateAttribute("ss", Attributes.Type, value);
		}

		public XmlAttribute GenerateAttribute(string prefix, string name, string value)
		{
			XmlAttribute attribute = XmlDocument.CreateAttribute(prefix, name, PREFIX_NAMESPACEURI[prefix]);
			attribute.Value = value;
			return attribute;
		}

		/// <summary>Creates a Cell tag containing the specified date.</summary>
		public XmlNode GenerateDateCell(DateTime data)
		{
			XmlNode cellNode = XmlDocument.CreateElement(LocalNames.Cell, ROOT_NAMESPACE);
			cellNode.Attributes.Append(GenerateStyleIdAttribute(StyleIds.ShortDate));

			XmlNode dataNode = XmlDocument.CreateElement(LocalNames.Data, ROOT_NAMESPACE);
			dataNode.Attributes.Append(GenerateDateTypeAttribute());
			dataNode.InnerText = data.ToString("yyyy-MM-ddT00:00:00.000");

			cellNode.AppendChild(dataNode);

			return cellNode;
		}

		/// <summary>Generate a Cell tag containing the specified number.</summary>
		public XmlNode GenerateNumberCell(int data)
		{
			XmlNode cellNode = XmlDocument.CreateElement(LocalNames.Cell, ROOT_NAMESPACE);

			XmlNode dataNode = XmlDocument.CreateElement(LocalNames.Data, ROOT_NAMESPACE);
			dataNode.Attributes.Append(GenerateNumberTypeAttribute());
			dataNode.InnerText = data.ToString();

			cellNode.AppendChild(dataNode);

			return cellNode;
		}

		/// <summary>Generate a header Cell tag containing the specified text.</summary>
		public XmlNode GenerateHeaderCell(string data)
		{
			return GenerateTextCell(data, StyleIds.Header);
		}

		/// <summary>Generate a paragraph Cell tag containing the specified text.</summary>
		public XmlNode GenerateParagraphCell(string data)
		{
			return GenerateTextCell(data, StyleIds.Paragraph);
		}

		/// <summary>Generate a paragraph Cell tag containing the specified text.</summary>
		public XmlNode GenerateTextCell(string data)
		{
			return GenerateTextCell(data, styleId: null);
		}

		/// <summary>Generate an empty Cell tag.</summary>
		public XmlNode GenerateEmptyCell()
		{
			return GenerateTextCell(data: "", styleId: null);
		}

		/// <summary>Generate a Cell tag containing the specified text.</summary>
		public XmlNode GenerateTextCell(string data, string styleId = null)
		{
			XmlNode cellNode = XmlDocument.CreateElement(LocalNames.Cell, ROOT_NAMESPACE);
			if(styleId != null)
			{
				cellNode.Attributes.Append(GenerateStyleIdAttribute(styleId));
			}

			XmlNode dataNode = XmlDocument.CreateElement(LocalNames.Data, ROOT_NAMESPACE);
			dataNode.Attributes.Append(GenerateStringTypeAttribute());
			dataNode.InnerText = data;

			cellNode.AppendChild(dataNode);

			return cellNode;
		}

		#endregion

		#region Private

		private void RemoveColumns(int tableIndex)
		{
			XmlNode tableNode = tableNodes[tableIndex];
			foreach(XmlNode childNode in tableNode.ChildNodes.Cast<XmlNode>().Where(node => node.LocalName == LocalNames.Column).ToList())
			{
				tableNode.RemoveChild(childNode);
			}
		}

		private List<XmlNode> GetRows(int tableIndex)
		{
			ValidateTableIndex(tableIndex);
			return tableNodes[tableIndex].ChildNodes.Cast<XmlNode>().Where(node => node.LocalName == LocalNames.Row).ToList();
		}

		public string GetCellValue(XmlNode cellNode)
		{
			foreach(XmlNode dataNode in cellNode.ChildNodes.Cast<XmlNode>().Where(node => node.LocalName == LocalNames.Data))
			{
				return dataNode.InnerText;
			}
			return null;
		}

		private void ValidateTableIndex(int index)
		{
			if(index < 0 || index >= tableNodes.Count)
				throw new IndexOutOfRangeException("Table index out of range.");
		}

		private void ValidateRowIndex(int tableIndex, int rowIndex)
		{
			ValidateTableIndex(tableIndex);
			if(rowIndex < 0 || rowIndex >= GetRowCount(tableIndex))
				throw new IndexOutOfRangeException("Row index out of range.");
		}

		#endregion

	}
}
