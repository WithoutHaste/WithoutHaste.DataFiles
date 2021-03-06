﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WithoutHaste.DataFiles.Markdown
{
	/// <summary>
	/// Represents a markdown element that can be included within a MarkdownSection.
	/// </summary>
	public interface IMarkdownInSection
	{
		/// <summary>
		/// Return markdown-formatted text.
		/// </summary>
		string ToMarkdownString(string previousText);
	}
}
