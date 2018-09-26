﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
		public static new DotNetEvent FromVisualStudioXml(XElement memberElement)
		{
			DotNetQualifiedName name = DotNetQualifiedName.FromVisualStudioXml(memberElement.Attribute("name")?.Value);
			return new DotNetEvent(name);
		}

		#endregion

		/// <summary>
		/// Load additional documentation information from the assembly itself.
		/// </summary>
		public void AddAssemblyInfo(EventInfo eventInfo)
		{
			DataTypeName = DotNetParameterBase.FromAssemblyInfo(eventInfo.EventHandlerType);
		}
	}
}
