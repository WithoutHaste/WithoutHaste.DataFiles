using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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

		//todo: implement
		/*
		/// <summary>
		/// Load markdown from file.
		/// </summary>
		/// <param name="filename">Full path, filename, and extension.</param>
		/// <exception cref="ArgumentException">Unexpected file extension.</exception>
		public MarkdownFile(string filename)
		{
			if(!Extensions.Contains(Path.GetExtension(filename).ToLower()))
				throw new ArgumentException("Unexpected file extension. Use one of these extensions: " + String.Join(", ", Extensions));

			//todo: load markdown from file
			throw new NotImplementedException("Loading markdown from file not implemented yet.");
		}
		*/

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
		public string ToMarkdownString()
		{
			StringBuilder builder = new StringBuilder();

			string previousText = null;
			foreach(IMarkdownInSection element in elements)
			{
				if(element is MarkdownSection)
				{
					previousText = (element as MarkdownSection).ToMarkdownString(previousText);
				}
				else
				{
					previousText = element.ToMarkdownString(previousText);
				}
				builder.Append(previousText);
			}

			return builder.ToString();
		}

	}
}
