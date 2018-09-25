using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a fully qualified type name or member name.
	/// </summary>
	public class DotNetQualifiedName
	{
		/// <summary>Fully qualified namespace.</summary>
		/// <remarks>Null if there is no namespace.</remarks>
		public DotNetQualifiedName FullNamespace { get; protected set; }

		/// <summary>Fully qualified name.</summary>
		public string FullName { get { return ToString(); } }

		/// <summary>Name without namespace.</summary>
		public virtual string LocalName { get { return localName; } }

		/// <summary>Name with namespace or required generic types.</summary>
		protected string localName;

		#region Constructors

		/// <summary>Empty constructor</summary>
		public DotNetQualifiedName()
		{
		}

		/// <summary></summary>
		public DotNetQualifiedName(string localName)
		{
			this.localName = localName;
		}

		/// <summary></summary>
		public DotNetQualifiedName(string localName, DotNetQualifiedName fullNamespace)
		{
			this.localName = localName;
			this.FullNamespace = fullNamespace;
		}
		
		/// <summary>
		/// Parses a .Net XML documentation type, method, or other member name.
		/// </summary>
		/// <param name="name">
		///   <list>
		///     <item>Names starting with "T:" are parsed as Type names.</item>
		///     <item>Names starting with "M:" are parsed as Method names.</item>
		///     <item>Names starting with "F:" are parsed as Member names.</item>
		///     <item>Names starting with "P:" are parsed as Member names.</item>
		///     <item>Names starting with "E:" are parsed as Member names.</item>
		///   </list>
		/// </param>
		/// <exception cref="XmlFormatException">Name does not start with /[TMFPE]:/</exception>
		public static DotNetQualifiedName FromVisualStudioXml(string name)
		{
			if(String.IsNullOrEmpty(name)) return new DotNetQualifiedName();

			if(name.Length < 2 || name[1] != ':') throw new XmlFormatException(String.Format("Name '{0}' does not start with /[TMFPE]:/", name));

			switch(name[0])
			{
				case 'T': return TypeNameFromVisualStudioXml(name);
				case 'M': return MethodNameFromVisualStudioXml(name);
				case 'F':
				case 'P':
				case 'E': return MemberNameFromVisualStudioXml(name);
				default:
					throw new XmlFormatException(String.Format("Name '{0}' does not start with /[TMFPE]:/", name));
			}
		}

		/// <summary>
		/// Parses a .Net XML documentation parameter type name.
		/// </summary>
		public static DotNetQualifiedName ParameterTypeFromVisualStudioXml(string typeName)
		{
			if(String.IsNullOrEmpty(typeName)) return new DotNetQualifiedName();

			//fully qualified generic type parameters
			//such as System.Collections.Generic.List<T> which are formatted as System.Collections.Generic{`0}
			List<DotNetParameter> parameters = null;
			if(typeName.EndsWith("}"))
			{
				string parameterSignature = typeName.Substring(typeName.IndexOf("{"));
				typeName = typeName.Substring(0, typeName.IndexOf("{"));
				parameters = DotNetMethod.ParametersFromVisualStudioXml(parameterSignature);
			}

			int divider = typeName.LastIndexOf('.');
			string localName = typeName;
			string fullNamespace = null;
			if(divider != -1)
			{
				localName = typeName.Substring(divider + 1);
				fullNamespace = typeName.Substring(0, divider);
			}
			else
			{
				//method-generic-type parameters
				int genericTypeIndex = 0;
				if(localName.StartsWith("``"))
				{
					Int32.TryParse(localName.Substring(localName.IndexOf("``") + 2), out genericTypeIndex);
					localName = DotNetQualifiedMethodName.GenericTypeNames[genericTypeIndex];
				}
				//class-generic-type parameters
				else if(localName.StartsWith("`"))
				{
					Int32.TryParse(localName.Substring(localName.IndexOf('`') + 1), out genericTypeIndex);
					localName = DotNetQualifiedClassName.GenericTypeNames[genericTypeIndex];
				}
			}

			if(parameters != null)
			{
				localName += String.Format("<{0}>", String.Join(",", parameters.Select(p => p.TypeName.ToString()).ToArray()));
			}

			if(String.IsNullOrEmpty(fullNamespace)) return new DotNetQualifiedName(localName);

			return new DotNetQualifiedName(localName, TypeNameFromVisualStudioXml(fullNamespace));
		}

		/// <summary>
		/// Parses a .Net XML documentation type name or namespace name.
		/// </summary>
		/// <remarks>
		/// Does not differentiate between types and namespaces 
		/// because a nested type will have other type names in its namespace path
		/// and there are no important diffences in parsing the two.
		/// </remarks>
		/// <param name="name">Name may or may not start with "T:"</param>
		private static DotNetQualifiedName TypeNameFromVisualStudioXml(string name)
		{
			if(name.StartsWith("T:")) name = name.Substring(2);

			int divider = name.LastIndexOf('.');
			string localName = name;
			string fullNamespace = null;
			if(divider != -1)
			{
				localName = name.Substring(divider + 1);
				fullNamespace = name.Substring(0, divider);
			}

			int classGenericTypeCount = 0;
			if(localName.Contains("`"))
			{
				Int32.TryParse(localName.Substring(localName.IndexOf('`') + 1), out classGenericTypeCount);
				localName = localName.Substring(0, localName.IndexOf('`'));
			}

			if(String.IsNullOrEmpty(fullNamespace)) return new DotNetQualifiedClassName(localName, classGenericTypeCount);

			return new DotNetQualifiedClassName(localName, TypeNameFromVisualStudioXml(fullNamespace), classGenericTypeCount);
		}

		/// <summary>
		/// Parses a .Net XML documentation method signature.
		/// </summary>
		/// <param name="signature">Name may or may not start with "M:". Includes parameter list.</param>
		private static DotNetQualifiedName MethodNameFromVisualStudioXml(string signature)
		{
			if(signature.StartsWith("M:")) signature = signature.Substring(2);

			string parameters = null;
			if(signature.Contains("("))
			{
				parameters = signature.Substring(signature.IndexOf("("));
				signature = signature.Substring(0, signature.IndexOf("("));
				//parameters are ignored
			}

			int divider = signature.LastIndexOf('.');
			string localName = signature;
			string fullNamespace = null;
			if(divider != -1)
			{
				localName = signature.Substring(divider + 1);
				fullNamespace = signature.Substring(0, divider);
			}

			int methodGenericTypeCount = 0;
			if(localName.Contains("``"))
			{
				Int32.TryParse(localName.Substring(localName.IndexOf("``") + 2), out methodGenericTypeCount);
				localName = localName.Substring(0, localName.IndexOf("``"));
			}

			if(String.IsNullOrEmpty(fullNamespace)) return new DotNetQualifiedMethodName(localName, methodGenericTypeCount);

			return new DotNetQualifiedMethodName(localName, TypeNameFromVisualStudioXml(fullNamespace), methodGenericTypeCount);
		}

		/// <summary>
		/// Parses a .Net XML documentation member name.
		/// </summary>
		/// <remarks>
		/// There is no support for generic types here because .Net XMl documentation does not include member types, just the names.
		/// </remarks>
		/// <param name="name">Name may or may not start with /[FPE]:/</param>
		private static DotNetQualifiedName MemberNameFromVisualStudioXml(string name)
		{
			if(name.StartsWith("F:")) name = name.Substring(2);
			if(name.StartsWith("P:")) name = name.Substring(2);
			if(name.StartsWith("E:")) name = name.Substring(2);

			int divider = name.LastIndexOf('.');
			string localName = name;
			string fullNamespace = null;
			if(divider != -1)
			{
				localName = name.Substring(divider + 1);
				fullNamespace = name.Substring(0, divider);
			}

			if(String.IsNullOrEmpty(fullNamespace)) return new DotNetQualifiedName(localName);

			return new DotNetQualifiedName(localName, TypeNameFromVisualStudioXml(fullNamespace));
		}

		#endregion

		/// <summary></summary>
		public DotNetQualifiedName SetLocalName(string name)
		{
			return new DotNetQualifiedName() {
				FullNamespace = this.FullNamespace,
				localName = name
			};
		}

		/// <summary>Return the names combined with a '.' delimiter.</summary>
		public static string Combine(params string[] names)
		{
			return String.Join(".", names);
		}

		/// <summary>Return the names combined with a '.' delimiter.</summary>
		public static string Combine(List<string> names)
		{
			return String.Join(".", names);
		}

		#region Low Level Operations

		/// <summary>Returns dot notation of namespaces and local name.</summary>
		/// <example>A.B.C.LocalName</example>
		public static implicit operator string(DotNetQualifiedName name)
		{
			return name?.ToString();
		}

		/// <summary>Returns dot notation of namespaces and local name.</summary>
		/// <example>A.B.C.LocalName</example>
		public override string ToString()
		{
			if(FullNamespace == null)
			{
				return LocalName;
			}
			return FullNamespace + "." + LocalName;
		}

		#endregion
	}
}
