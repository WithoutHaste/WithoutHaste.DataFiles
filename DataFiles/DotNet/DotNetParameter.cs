using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary></summary>
	public enum ParameterCategory
	{
		/// <summary></summary>
		Unknown = 0,
		/// <summary></summary>
		Normal,
		/// <summary>Parameter is either an out or a ref parameter.</summary>
		OutOrRef,
		/// <summary></summary>
		Out,
		/// <summary></summary>
		Ref,
	};

	/// <summary>
	/// Represents a normal-type parameter in a method signature.
	/// </summary>
	public class DotNetParameter
	{
		/// <summary>Fully qualified data type name object.</summary>
		public DotNetQualifiedTypeName TypeName { get; protected set; }

		/// <summary>Fully qualified data type name.</summary>
		public string FullTypeName { get { return TypeName?.FullName; } }

		/// <summary>Local data type name.</summary>
		public string LocalTypeName { get { return TypeName?.LocalName; } }

		/// <summary>Name of parameter. Null if not known.</summary>
		public string Name { get; protected set; }

		/// <summary></summary>
		public ParameterCategory Category { get; protected set; }

		/// <summary>
		/// Returns formatted string "Type Name", "out Type Name" or "ref Type Name".
		/// </summary>
		public string SignatureWithName {
			get {
				string signature = FullTypeName + " " + Name;
				if(Category == ParameterCategory.Out)
					return "out " + signature;
				if(Category == ParameterCategory.Ref)
					return "ref " + signature;
				return signature;
			}
		}

		/// <summary>
		/// Returns formatted string "Type", "out Type" or "ref Type".
		/// </summary>
		public string SignatureWithoutName {
			get {
				string signature = FullTypeName;
				if(Category == ParameterCategory.Out)
					return "out " + signature;
				if(Category == ParameterCategory.Ref)
					return "ref " + signature;
				return signature;
			}
		}

		#region Constructors

		/// <summary>Empty constructor.</summary>
		/// <remarks>Category defaults to Unknown.</remarks>
		public DotNetParameter()
		{
			TypeName = null;
			Category = ParameterCategory.Unknown;
		}

		/// <summary></summary>
		/// <remarks>Category defaults to Normal.</remarks>
		/// <param name="typeName">Fully qualified data type name.</param>
		public DotNetParameter(DotNetQualifiedTypeName typeName)
		{
			TypeName = typeName;
			Category = ParameterCategory.Normal;
		}


		/// <summary></summary>
		/// <param name="typeName">Fully qualified data type name.</param>
		/// <param name="category">Category of parameter.</param>
		public DotNetParameter(DotNetQualifiedTypeName typeName, ParameterCategory category)
		{
			TypeName = typeName;
			Category = category;
		}

		/// <summary>
		/// Parses a .Net XML documentation parameter type name.
		/// </summary>
		public static DotNetParameter FromVisualStudioXml(string typeName)
		{
			ParameterCategory category = ParameterCategory.Normal;
			if(typeName.Contains("@"))
				category = ParameterCategory.OutOrRef;
			typeName = typeName.Replace("@", "");
			return new DotNetParameter(DotNetQualifiedTypeName.FromVisualStudioXml(typeName), category);
		}

		#endregion

		/// <summary>
		/// Load additional documentation information from the assembly itself.
		/// </summary>
		public virtual void AddAssemblyInfo(ParameterInfo parameterInfo)
		{
			if(parameterInfo.IsOut)
				Category = ParameterCategory.Out;
			else if(parameterInfo.ParameterType.Name.Contains("&"))
				Category = ParameterCategory.Ref;

			Name = parameterInfo.Name;
		}

		#region Low Level

		/// <duplicate cref='SignatureWithName'/>
		public override string ToString()
		{
			return SignatureWithName;
		}

		#endregion
	}
}
