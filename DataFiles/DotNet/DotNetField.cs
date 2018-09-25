using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a type's field.
	/// </summary>
	public class DotNetField : DotNetMember
	{
		#region Constructors

		/// <summary></summary>
		public DotNetField(DotNetQualifiedName name) : base(name)
		{
		}

		/// <summary>
		/// Parse .Net XML documentation for Field data.
		/// </summary>
		/// <param name="memberElement">Expects tag name "member".</param>
		public static DotNetField FromVisualStudioXml(XElement memberElement)
		{
			DotNetQualifiedName name = DotNetQualifiedName.FromVisualStudioXml(memberElement.Attribute("name")?.Value);
			return new DotNetField(name);
		}

		#endregion
	}
}
