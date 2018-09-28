using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles
{
	/// <summary>Badly formatted string.</summary>
	public class StringFormatException : Exception
	{
		/// <summary></summary>
		public StringFormatException(string message) : base(message)
		{
		}
	}
}
