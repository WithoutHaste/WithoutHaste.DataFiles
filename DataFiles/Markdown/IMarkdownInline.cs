﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WithoutHaste.DataFiles.Markdown
{
	/// <summary>
	/// Represents a markdown element that can be included within a MarkdownLine (i.e. within text).
	/// </summary>
	public interface IMarkdownInLine : IMarkdownInSection
	{
	}
}
