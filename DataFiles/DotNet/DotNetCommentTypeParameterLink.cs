﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a link in the comments to an internal generic-type parameter.
	/// </summary>
	/// <example><![CDATA[<typeparamref name="T" />]]></example>
	public class DotNetCommentTypeParameterLink : DotNetCommentParameterLink
	{
		#region Constructors

		/// <summary></summary>
		public DotNetCommentTypeParameterLink(string name) : base(name)
		{
		}

		/// <summary></summary>
		public DotNetCommentTypeParameterLink(string name, CommentTag tag) : base(name)
		{
			Tag = tag;
		}

		/// <summary>Parses .Net XML documentation for typeparamref.</summary>
		public static new DotNetCommentTypeParameterLink FromVisualStudioXml(XElement element)
		{
			ValidateXmlTag(element, "typeparamref");
			return new DotNetCommentTypeParameterLink(element.Attribute("name")?.Value, DotNetComment.GetTag(element));
		}

		#endregion
	}
}
