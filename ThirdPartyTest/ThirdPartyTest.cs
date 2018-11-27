using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeyRed.MarkdownSharp;

namespace ThirdPartyTest
{
	/// <summary>
	/// Class uses a third party library that neither DataFilesTest nor DataFiles has installed.
	/// Enables testing how DataFiles.DotNet handles reflection on unknown third party libraries.
	/// </summary>
	public class ThirdPartyTest
	{
		/// <summary>
		/// Return a third party type.
		/// </summary>
		public Markdown MethodA()
		{
			return new Markdown();
		}

		/// <summary>
		/// Parameter with third party type.
		/// </summary>
		/// <param name="a">Parameter comments.</param>
		public void MethodB(Markdown a)
		{
		}

		/// <summary>
		/// Return a third party type.
		/// Parameter with third party type.
		/// </summary>
		/// <param name="a">Parameter comments.</param>
		public Markdown MethodC(Markdown a)
		{
			return a;
		}

	}
}
