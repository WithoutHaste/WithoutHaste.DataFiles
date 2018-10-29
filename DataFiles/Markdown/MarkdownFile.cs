using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WithoutHaste.Libraries;

namespace WithoutHaste.DataFiles.Markdown
{
	/// <summary>
	/// Represents a markdown file.
	/// </summary>
	public class MarkdownFile
	{
		/// <summary>Accepted markdown file extensions.</summary>
		public static readonly string[] Extensions = new string[] { ".md", ".markdown" };

		private List<IMarkdownInSection> elements = new List<IMarkdownInSection>();
		private const int DEPTH = 0;

		#region Constructors

		/// <summary>
		/// Create an empty markdown file.
		/// </summary>
		public MarkdownFile()
		{
		}

		/// <summary>
		/// Load markdown from file.
		/// </summary>
		/// <param name="filename">Full path, filename, and extension.</param>
		/// <exception cref="FileExtensionException">Unexpected file extension.</exception>
		public MarkdownFile(string filename)
		{
			FileInfo.ValidateExtension(filename, Extensions);

			//todo: load markdown from file
			throw new NotImplementedException("Loading markdown from file not implemented yet.");
		}

		#endregion

		/// <summary>
		/// Creates new section and adds it to the end of the file. Defaults to depth 1.
		/// </summary>
		/// <param name="header">Section header</param>
		/// <returns>The new section</returns>
		public MarkdownSection AddSection(string header)
		{
			MarkdownSection section = new MarkdownSection(header, DEPTH + 1);
			elements.Add(section);
			return section;
		}

		/// <summary>
		/// Adds existing section to the end of this file. Depths are not updated.
		/// </summary>
		/// <param name="section">Existing section.</param>
		public void AddSection(MarkdownSection section)
		{
			elements.Add(section);
		}

		/// <summary>
		/// Returns full markdown text for file, formatted for legibility.
		/// </summary>
		public string ToMarkdown()
		{
			StringBuilder builder = new StringBuilder();

			string previousText = null;
			foreach(IMarkdownInSection element in elements)
			{
				if(element is MarkdownSection)
				{
					previousText = (element as MarkdownSection).ToMarkdown(previousText);
				}
				else
				{
					previousText = element.ToMarkdown(previousText);
				}
				builder.Append(previousText);
			}

			return builder.ToString();
		}

	}
}
