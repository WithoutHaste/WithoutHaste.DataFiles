using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.Markdown
{
	/// <summary>
	/// Display style of text.
	/// </summary>
	/// <remarks>
	/// Number enum values with powers of 2 to allow bitwise operations.
	/// </remarks>
	public enum TextStyle {
		/// <summary></summary>
		Normal = 0,
		/// <summary></summary>
		Bold = 1,
		/// <summary></summary>
		Italic = 2
	};

	/// <summary>
	/// Represents plain text.
	/// </summary>
	public class MarkdownText : IMarkdownInLine, IMarkdownInSection
	{
		/// <summary></summary>
		public string Text { get; protected set; }

		/// <summary></summary>
		/// <remarks>Supports multiple selections such as <c>TextStype.Bold | TextStyle.Italic</c>.</remarks>
		public TextStyle Style { get; protected set; }

		#region Constructors

		/// <summary></summary>
		public MarkdownText(string text)
		{
			Text = text;
			Style = TextStyle.Normal;
		}

		/// <summary></summary>
		public MarkdownText(string text, TextStyle style)
		{
			Text = text;
			Style = style;
		}

		/// <summary>Generate bold text.</summary>
		/// <remarks><paramref name="text"/> is trimmed to conform to Markdown formatting requirements.</remarks>
		public static MarkdownText Bold(string text)
		{
			return new MarkdownText(text.Trim(), TextStyle.Bold);
		}

		/// <summary>Generate italic text.</summary>
		/// <remarks><paramref name="text"/> is trimmed to conform to Markdown formatting requirements.</remarks>
		public static MarkdownText Italic(string text)
		{
			return new MarkdownText(text.Trim(), TextStyle.Italic);
		}

		/// <summary>Generate bold-italic text.</summary>
		/// <remarks><paramref name="text"/> is trimmed to conform to Markdown formatting requirements.</remarks>
		public static MarkdownText BoldItalic(string text)
		{
			return new MarkdownText(text.Trim(), TextStyle.Bold | TextStyle.Italic);
		}

		#endregion

		/// <inheritdoc />
		/// <remarks>Uses ** for bold and _ for italics.</remarks>
		public string ToMarkdown(string previousText)
		{
			if(Style == TextStyle.Bold)
				return "**" + Text + "**";
			if(Style == TextStyle.Italic)
				return "_" + Text + "_";
			if(Style == (TextStyle.Bold | TextStyle.Italic))
				return "**_" + Text + "_**";
			return Text;
		}
	}
}
