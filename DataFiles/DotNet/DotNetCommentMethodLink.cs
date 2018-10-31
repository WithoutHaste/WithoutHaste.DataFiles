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
		/// <summary>Strongly typed name.</summary>
		public DotNetQualifiedMethodName MethodName { get { return (Name as DotNetQualifiedMethodName); } }

		/// <summary>Fully qualified method name with parameters.</summary>
		/// <example>Namespace.Type.Method()</example>
		/// <example>Namespace.Type.Method(int,string)</example>
		/// <example><![CDATA[Namespace.Type.Method(System.Collections.Generic.List<int>)]]></example>
		public string FullSignature {
			get {
				return String.Format("{0}({1})", FullName, String.Join(",", MethodName.Parameters.Select(p => p.FullTypeName).ToArray()));
			}
		}

		#region Constructors

		/// <summary></summary>
		public DotNetCommentMethodLink(DotNetQualifiedMethodName name) : base(name)
		{
		}

		/// <summary>Parses .Net XML documentation cref for methods.</summary>
		public static new DotNetCommentMethodLink FromVisualStudioXml(string cref)
		{
			DotNetQualifiedMethodName name = DotNetQualifiedMethodName.FromVisualStudioXml(cref);
			return new DotNetCommentMethodLink(name);
		}

		#endregion

		/// <summary>
		/// Returns true if this method link and the method have matching signatures, based on the fully qualified name and the list of parameter types.
		/// </summary>
		public bool MatchesSignature(DotNetMethod method)
		{
			return MethodName.MatchesSignature(method.MethodName);
		}
	}
}
