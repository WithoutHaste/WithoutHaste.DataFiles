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
		/// <summary>Default names that will be given to generic-types, in order.</summary>
		public static string[] GenericTypeNames = new string[] { "T", "U", "V", "W", "T2", "U2", "V2", "W2", "T3", "U3", "V3", "W3" };

		/// <summary>Default names that will be given to generic-method-types, in order.</summary>
		public static string[] GenericMethodTypeNames = new string[] { "A", "B", "C", "D", "A2", "B2", "C2", "D2", "A3", "B3", "C3", "D3" };

		/// <summary>Fully qualified namespace.</summary>
		/// <remarks>Null if there is no namespace.</remarks>
		public DotNetQualifiedName FullNamespace { get; protected set; }

		/// <summary>Fully qualified name.</summary>
		public string FullName { get { return ToString(); } }

		/// <summary>Name without namespace.</summary>
		public string LocalName {
			get {
				if(genericTypeCount == 0)
					return localName;
				if(usesGenericMethodTypeNames)
					return String.Format("{0}<{1}>", localName, String.Join(",", GenericMethodTypeNames.Take(genericTypeCount).ToArray()));
				else
					return String.Format("{0}<{1}>", localName, String.Join(",", GenericTypeNames.Take(genericTypeCount).ToArray()));
			}
		}

		private string localName;
		private int genericTypeCount;
		private bool usesGenericMethodTypeNames = false;

		#region Constructors

		/// <summary>Empty constructor</summary>
		public DotNetQualifiedName()
		{
		}

		/// <summary>Normal constructor</summary>
		/// <param name="name">Fully qualified name, using period (.) delimiter.</param>
		/// <example>Generic type names end with "`D" where D is the integer count of generic types used.</example>
		public DotNetQualifiedName(string name)
		{
			if(name == null)
				throw new ArgumentException("Name cannot be null.", "name");

			int divider = name.LastIndexOf('.');
			if(divider == -1)
			{
				this.localName = name;
				//non-constructor generic-type parameters
				int genericTypeIndex = 0;
				if(this.localName.Contains("``"))
				{
					Int32.TryParse(this.localName.Substring(this.localName.IndexOf("``") + 2), out genericTypeIndex);
					this.localName = GenericMethodTypeNames[genericTypeIndex];
				}
				//generic-type-constructor parameters
				else if(this.localName.Contains("`"))
				{
					Int32.TryParse(this.localName.Substring(this.localName.IndexOf('`') + 1), out genericTypeIndex);
					this.localName = GenericTypeNames[genericTypeIndex];
				}
				return;
			}
			this.localName = name.Substring(divider + 1);
			string fullNamespace = name.Substring(0, divider);

			//generic-methods
			if(this.localName.Contains("``"))
			{
				Int32.TryParse(this.localName.Substring(this.localName.IndexOf("``") + 2), out this.genericTypeCount);
				this.localName = this.localName.Substring(0, this.localName.IndexOf("``"));
				this.usesGenericMethodTypeNames = true;
			}
			//generic-types
			else if(this.localName.Contains("`"))
			{
				Int32.TryParse(this.localName.Substring(this.localName.IndexOf('`') + 1), out this.genericTypeCount);
				this.localName = this.localName.Substring(0, this.localName.IndexOf('`'));
			}

			if(!String.IsNullOrEmpty(fullNamespace))
			{
				FullNamespace = new DotNetQualifiedName(fullNamespace);
			}
		}

		/// <summary>
		/// Parse a .Net XML documentation type name.
		/// </summary>
		public static DotNetQualifiedName FromVisualStudioXml(string typeName)
		{
			if(typeName == null)
				return new DotNetQualifiedName();

			//remove leading "X:" from <member name="X:Name"> (many different characters used)
			if(typeName.Length > 1 && typeName[1] == ':')
			{
				typeName = typeName.Substring(2);
			}

			return new DotNetQualifiedName(typeName);
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

		/// <summary>Returns dot notation of namespaces and local name.</summary>
		/// <example>A.B.C.LocalName</example>
		public static implicit operator string(DotNetQualifiedName name)
		{
			return name.ToString();
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
	}
}
