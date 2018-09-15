using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles
{
	/// <summary>
	/// Exceptions related to XmlNode objects.
	/// </summary>
	public class XmlNodeException : Exception
	{
		public XmlNodeException(string message) : base(message)
		{
		}
	}
}
