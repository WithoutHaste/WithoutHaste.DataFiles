using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a type's property.
	/// </summary>
	public class DotNetProperty : DotNetField
	{
		#region Constructors

		/// <summary></summary>
		public DotNetProperty(DotNetQualifiedName name) : base(name)
		{
			Category = FieldCategory.Normal;
		}

		/// <summary>
		/// Parse .Net XML documentation for Property data.
		/// </summary>
		/// <param name="memberElement">Expects tag name "member".</param>
		public static new DotNetProperty FromVisualStudioXml(XElement memberElement)
		{
			DotNetQualifiedName name = DotNetQualifiedName.FromVisualStudioXml(memberElement.Attribute("name")?.Value);
			return new DotNetProperty(name);
		}

		#endregion

		/// <summary>
		/// Load additional documentation information from the assembly itself.
		/// </summary>
		public virtual void AddAssemblyInfo(PropertyInfo propertyInfo)
		{
			DataTypeName = DotNetParameterBase.FromAssemblyInfo(propertyInfo.PropertyType);
		}
	}
}
