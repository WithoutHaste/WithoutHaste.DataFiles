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
		/// <summary>Default names that will be given to generic types, in order.</summary>
		public static string[] GenericTypeNames = new string[] { "T", "U", "V", "W", "T2", "U2", "V2", "W2", "T3", "U3", "V3", "W3" };

		/// <summary>Fully qualified namespace.</summary>
		/// <remarks>Null if there is no namespace.</remarks>
		public string FullNamespace { get; protected set; }

		/// <summary>Fully qualified name.</summary>
		public string FullName {
			get {
				if(String.IsNullOrEmpty(FullNamespace))
					return LocalName;
				return String.Format("{0}.{1}", FullNamespace, LocalName);
			}
		}

		/// <summary>Name without namespace.</summary>
		public string LocalName {
			get {
				if(genericTypeCount == 0)
					return localName;
				return String.Format("{0}<{1}>", localName, String.Join(",", GenericTypeNames.Take(genericTypeCount).ToArray()));
			}
		}

		private string localName;
		private int genericTypeCount;

		#region Constructors

		/// <summary>Empty constructor</summary>
		public DotNetQualifiedName()
		{
		}

		/// <summary>Normal constructor</summary>
		/// <param name="name">Fully qualified name, using period (.) delimiter.</param>
		/// <param name="genericTypeCount">For generic types: the number of types they require.</param>
		public DotNetQualifiedName(string name, int genericTypeCount=0)
		{
			if(name == null)
				throw new ArgumentException("Name cannot be null.", "name");

			this.genericTypeCount = genericTypeCount;

			int divider = name.LastIndexOf('.');
			if(divider == -1)
			{
				this.localName = name;
				return;
			}
			this.localName = name.Substring(divider + 1);
			FullNamespace = name.Substring(0, divider);
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

			//generic types
			int genericTypeCount = 0;
			if(typeName.Contains("`"))
			{
				Int32.TryParse(typeName.Substring(typeName.IndexOf('`') + 1), out genericTypeCount);
				typeName = typeName.Substring(0, typeName.IndexOf('`'));
			}

			return new DotNetQualifiedName(typeName, genericTypeCount);
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
	}
}
