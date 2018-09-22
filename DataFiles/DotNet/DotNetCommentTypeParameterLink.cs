using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a link in the comments to an internal generic-type parameter.
	/// </summary>
	public class DotNetCommentTypeParameterLink : DotNetCommentParameterLink
	{
		#region Constructors

		/// <summary></summary>
		public DotNetCommentTypeParameterLink(string name) : base(name)
		{
		}

		/// <summary>Parses .Net XML documentation for typeparamref.</summary>
		public static new DotNetCommentTypeParameterLink FromVisualStudioXml(XElement element)
		{
			return new DotNetCommentTypeParameterLink(element.Attribute("name")?.Value);
		}

		#endregion
	}
}
