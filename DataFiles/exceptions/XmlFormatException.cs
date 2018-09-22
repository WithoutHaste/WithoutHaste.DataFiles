using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles
{
	/// <summary>
	/// Badly formatted xml, or unexpected xml.
	/// </summary>
	public class XmlFormatException : Exception
	{
		/// <summary></summary>
		public XmlFormatException(string message) : base(message) { }
	}
}
