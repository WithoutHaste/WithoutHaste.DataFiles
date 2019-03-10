using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a type's event.
	/// </summary>
	public class DotNetEvent : DotNetField
	{
		#region Constructors

		/// <summary></summary>
		public DotNetEvent(DotNetQualifiedName name) : base(name)
		{
			Category = FieldCategory.Normal;
		}

		/// <summary>
		/// Parse .Net XML documentation for Event data.
		/// </summary>
		/// <param name="memberElement">Expects tag name "member".</param>
		/// <example><![CDATA[<member name="E:Namespace.Type.EventName"></member>]]></example>
		public static new DotNetEvent FromVisualStudioXml(XElement memberElement)
		{
			DotNetQualifiedName name = DotNetQualifiedName.FromVisualStudioXml(memberElement.GetAttributeValue("name"));
			DotNetEvent eventMember = new DotNetEvent(name);
			eventMember.ParseVisualStudioXmlDocumentation(memberElement);
			return eventMember;
		}

		#endregion

		/// <summary>
		/// Load additional documentation information from the assembly itself.
		/// </summary>
		public void AddAssemblyInfo(EventInfo eventInfo)
		{
			TypeName = DotNetQualifiedTypeName.FromAssemblyInfo(eventInfo.EventHandlerType);
		}
	}
}
