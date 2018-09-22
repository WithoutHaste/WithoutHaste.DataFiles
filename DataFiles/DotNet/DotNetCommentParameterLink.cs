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
	public class DotNetCommentParameterLink : DotNetComment, IDotNetCommentLink
	{
		/// <summary>Name of the parameter in local method.</summary>
		public string Name { get; protected set; }

		#region Constructors

		/// <summary></summary>
		public DotNetCommentParameterLink(string name)
		{
			Name = name;
		}

		/// <summary>Parses .Net XML documentation for paramref.</summary>
		public static new DotNetCommentParameterLink FromVisualStudioXml(XElement element)
		{
			return new DotNetCommentParameterLink(element.Attribute("name")?.Value);
		}

		#endregion
	}
}
