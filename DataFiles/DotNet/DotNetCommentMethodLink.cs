using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a link in the comments to an internal or extenal method.
	/// </summary>
	/// <example><![CDATA[<permission cref="Namespace.Type.Method(Type1, Type2)">nested comments and/or plain text</permission>]]></example>
	public class DotNetCommentMethodLink : DotNetCommentQualifiedLink
	{
		/// <summary></summary>
		public List<DotNetParameter> Parameters = new List<DotNetParameter>();

		/// <summary>Fully qualified method name with parameters.</summary>
		/// <example>Namespace.Type.Method()</example>
		/// <example>Namespace.Type.Method(int,string)</example>
		/// <example><![CDATA[Namespace.Type.Method(System.Collections.Generic.List<int>)]]></example>
		public string FullSignature {
			get {
				return String.Format("{0}({1})", FullName, String.Join(",",Parameters.Select(p => p.FullName).ToArray()));
			}
		}

		#region Constructors

		/// <summary></summary>
		public DotNetCommentMethodLink(DotNetQualifiedName name, List<DotNetParameter> parameters) : base(name)
		{
			Parameters.AddRange(parameters);
		}

		/// <summary>Parses .Net XML documentation cref for methods.</summary>
		public static new DotNetCommentMethodLink FromVisualStudioXml(string cref)
		{
			int divider = cref.IndexOf("(");
			if(divider == -1)
				throw new XmlFormatException("Method cref expecting parentheses around parameters. Use empty parentheses for methods with no parameters.");

			DotNetQualifiedName name = new DotNetQualifiedName(cref.Substring(0, divider));
			List<DotNetParameter> parameters = DotNetMethod.ParametersFromVisualStudioXml(cref.Substring(divider));
			return new DotNetCommentMethodLink(name, parameters);
		}

		#endregion
	}
}
