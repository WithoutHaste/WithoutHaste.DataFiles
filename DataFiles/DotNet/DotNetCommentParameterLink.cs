using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a link in the comments to an internal parameter name.
	/// </summary>
	/// <example><![CDATA[<paramref name="paramName" />]]></example>
	public class DotNetCommentParameterLink : DotNetComment, IDotNetCommentLink
	{
		/// <summary>Name of the parameter in local method.</summary>
		public string Name { get; protected set; }

		/// <inheritdoc />
		public string FullName { get { return Name; } }

		#region Constructors

		/// <summary></summary>
		public DotNetCommentParameterLink(string name)
		{
			Name = name;
		}

		/// <summary></summary>
		public DotNetCommentParameterLink(string name, CommentTag tag)
		{
			Name = name;
			Tag = tag;
		}

		/// <summary>Parses .Net XML documentation for paramref.</summary>
		public static new DotNetCommentParameterLink FromVisualStudioXml(XElement element)
		{
			ValidateXmlTag(element, "paramref");
			return new DotNetCommentParameterLink(element.Attribute("name")?.Value, DotNetComment.GetTag(element));
		}

		#endregion
	}
}
