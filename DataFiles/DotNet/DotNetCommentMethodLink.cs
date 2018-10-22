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
				return String.Format("{0}({1})", FullName, String.Join(",",Parameters.Select(p => p.FullTypeName).ToArray()));
			}
		}

		#region Constructors

		/// <summary></summary>
		public DotNetCommentMethodLink(DotNetQualifiedName name) : base(name)
		{
		}

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
			{
				return new DotNetCommentMethodLink(DotNetQualifiedName.FromVisualStudioXml(cref));
			}

			DotNetQualifiedName name = DotNetQualifiedName.FromVisualStudioXml(cref.Substring(0, divider));
			List<DotNetParameter> parameters = DotNetMethod.ParametersFromVisualStudioXml(cref.Substring(divider));
			return new DotNetCommentMethodLink(name, parameters);
		}

		#endregion

		/// <summary>
		/// Returns true if this method link and the method have matching signatures, based on the fully qualified name and the list of parameter types.
		/// </summary>
		public bool MatchesSignature(DotNetMethod method)
		{
			if(Name != method.Name)
				return false;
			if(Parameters.Count != method.Parameters.Count)
				return false;
			for(int i = 0; i < Parameters.Count; i++)
			{
				if(Parameters[i].TypeName != method.Parameters[i].TypeName)
					return false;
			}
			return true;
		}
	}
}
