using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles;

namespace DataFilesTest
{
	[TestClass]
	public class TextHelperTests
	{
		[TestMethod]
		public void TextHelper_ReplaceUnescapedCharacters_Simple()
		{
			//arrange
			string input = "+a+bb+ccc.d+ee+";
			char escapeChar = '\\';
			char searchChar = '+';
			char replacementChar = '.';
			string expectedOutput = ".a.bb.ccc.d.ee.";
			//act
			string result = input.ReplaceUnescapedCharacters(escapeChar, searchChar, replacementChar);
			//assert
			Assert.AreEqual(expectedOutput, result);
		}

		[TestMethod]
		public void TextHelper_ReplaceUnescapedCharacters_SingleEscapes()
		{
			//arrange
			string input = @"\+a+bb\+ccc.d+e\e+";
			char escapeChar = '\\';
			char searchChar = '+';
			char replacementChar = '.';
			string expectedOutput = @"\+a.bb\+ccc.d.e\e.";
			//act
			string result = input.ReplaceUnescapedCharacters(escapeChar, searchChar, replacementChar);
			//assert
			Assert.AreEqual(expectedOutput, result);
		}

		[TestMethod]
		public void TextHelper_ReplaceUnescapedCharacters_MultipleEscapes()
		{
			//arrange
			string input = @"\\\+a+bb\\+ccc.d+e\e+";
			char escapeChar = '\\';
			char searchChar = '+';
			char replacementChar = '.';
			string expectedOutput = @"\\\+a.bb\\.ccc.d.e\e.";
			//act
			string result = input.ReplaceUnescapedCharacters(escapeChar, searchChar, replacementChar);
			//assert
			Assert.AreEqual(expectedOutput, result);
		}

		[TestMethod]
		public void TextHelper_RemoveOutBraces_CurlyBraces()
		{
			//arrange
			string input = @"{abc { def} }";
			string expectedOutput = @"abc { def} ";
			//act
			string result = input.RemoveOuterBraces();
			//assert
			Assert.AreEqual(expectedOutput, result);
		}

		[TestMethod]
		public void TextHelper_RemoveOutBraces_SquareBraces()
		{
			//arrange
			string input = @"[abc [ def] ]";
			string expectedOutput = @"abc [ def] ";
			//act
			string result = input.RemoveOuterBraces();
			//assert
			Assert.AreEqual(expectedOutput, result);
		}

		[TestMethod]
		public void TextHelper_RemoveOutBraces_AngleBraces()
		{
			//arrange
			string input = @"<abc < def> >";
			string expectedOutput = @"abc < def> ";
			//act
			string result = input.RemoveOuterBraces();
			//assert
			Assert.AreEqual(expectedOutput, result);
		}

		[TestMethod]
		public void TextHelper_RemoveOutBraces_Parentheses()
		{
			//arrange
			string input = @"(abc ( def) )";
			string expectedOutput = @"abc ( def) ";
			//act
			string result = input.RemoveOuterBraces();
			//assert
			Assert.AreEqual(expectedOutput, result);
		}

		[TestMethod]
		public void TextHelper_RemoveOutBraces_CurlyBrace_NoChange()
		{
			//arrange
			string input = @"{abc { def} } ";
			string expectedOutput = @"{abc { def} } ";
			//act
			string result = input.RemoveOuterBraces();
			//assert
			Assert.AreEqual(expectedOutput, result);
		}

		[TestMethod]
		public void TextHelper_SplitIgnoreNested_NoDelimiter()
		{
			//arrange
			char delimiter = ',';
			string input = "abcdefg";
			string[] expectedOutput = new string[] { "abcdefg" };
			//act
			string[] result = input.SplitIgnoreNested(delimiter);
			//assert
			Assert.AreEqual(expectedOutput.Length, result.Length);
			for(int i = 0; i < expectedOutput.Length; i++)
				Assert.AreEqual(expectedOutput[i], result[i]);
		}

		[TestMethod]
		public void TextHelper_SplitIgnoreNested_EmptyMatches()
		{
			//arrange
			char delimiter = ',';
			string input = "abc,d,,efg,";
			string[] expectedOutput = new string[] { "abc", "d", "", "efg", "" };
			//act
			string[] result = input.SplitIgnoreNested(delimiter);
			//assert
			Assert.AreEqual(expectedOutput.Length, result.Length);
			for(int i = 0; i < expectedOutput.Length; i++)
				Assert.AreEqual(expectedOutput[i], result[i]);
		}

		[TestMethod]
		public void TextHelper_SplitIgnoreNested_NothingInNested()
		{
			//arrange
			char delimiter = ',';
			string input = "abc,d[],e{EE.<FF>}fg()";
			string[] expectedOutput = new string[] { "abc", "d[]", "e{EE.<FF>}fg()" };
			//act
			string[] result = input.SplitIgnoreNested(delimiter);
			//assert
			Assert.AreEqual(expectedOutput.Length, result.Length);
			for(int i = 0; i < expectedOutput.Length; i++)
				Assert.AreEqual(expectedOutput[i], result[i]);
		}

		[TestMethod]
		public void TextHelper_SplitIgnoreNested_DelimiterInNested()
		{
			//arrange
			char delimiter = ',';
			string input = "abc,d[X,Y],e{EE,<,F,F,>}fg(,)";
			string[] expectedOutput = new string[] { "abc", "d[X,Y]", "e{EE,<,F,F,>}fg(,)" };
			//act
			string[] result = input.SplitIgnoreNested(delimiter);
			//assert
			Assert.AreEqual(expectedOutput.Length, result.Length);
			for(int i = 0; i < expectedOutput.Length; i++)
				Assert.AreEqual(expectedOutput[i], result[i]);
		}

		[TestMethod]
		public void TextHelper_SplitIgnoreNested_CommaDelimitor()
		{
			//arrange
			char delimiter = ',';
			string input = "a,b,c";
			string[] expectedOutput = new string[] { "a", "b", "c" };
			//act
			string[] result = input.SplitIgnoreNested(delimiter);
			//assert
			Assert.AreEqual(expectedOutput.Length, result.Length);
			for(int i = 0; i < expectedOutput.Length; i++)
				Assert.AreEqual(expectedOutput[i], result[i]);
		}

		[TestMethod]
		public void TextHelper_SplitIgnoreNested_DotDelimitor()
		{
			//arrange
			char delimiter = '.';
			string input = "a.b.c";
			string[] expectedOutput = new string[] { "a", "b", "c" };
			//act
			string[] result = input.SplitIgnoreNested(delimiter);
			//assert
			Assert.AreEqual(expectedOutput.Length, result.Length);
			for(int i = 0; i < expectedOutput.Length; i++)
				Assert.AreEqual(expectedOutput[i], result[i]);
		}

		[TestMethod]
		public void TextHelper_SplitIgnoreNested_DigitDelimitor()
		{
			//arrange
			char delimiter = '0';
			string input = "a0b0c";
			string[] expectedOutput = new string[] { "a", "b", "c" };
			//act
			string[] result = input.SplitIgnoreNested(delimiter);
			//assert
			Assert.AreEqual(expectedOutput.Length, result.Length);
			for(int i = 0; i < expectedOutput.Length; i++)
				Assert.AreEqual(expectedOutput[i], result[i]);
		}
	}
}
