using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		string ToMarkdown();
	}
}
