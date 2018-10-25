using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles;

namespace DataFilesTest
{
	[TestClass]
	public class CountersTests
	{
		[TestMethod]
		public void AlphebetCounter_New()
		{
			//arrange
			AlphabetCounter counter = new AlphabetCounter();
			//act
			string result = counter.Value;
			//assert
			Assert.AreEqual("A", result);
		}

		[TestMethod]
		public void AlphebetCounter_IncrementOne()
		{
			//arrange
			AlphabetCounter counter = new AlphabetCounter();
			int delta = 1;
			//act
			counter += delta;
			string result = counter.Value;
			//assert
			Assert.AreEqual("B", result);
		}

		[TestMethod]
		public void AlphebetCounter_IncrementNegativeOne_FromStart()
		{
			//arrange
			AlphabetCounter counter = new AlphabetCounter();
			int delta = -1;
			//act
			counter += delta;
			string result = counter.Value;
			//assert
			Assert.AreEqual("A", result);
		}

		[TestMethod]
		public void AlphebetCounter_IncrementNegativeOne()
		{
			//arrange
			AlphabetCounter counter = new AlphabetCounter();
			counter.SetValue("C");
			int delta = -1;
			//act
			counter += delta;
			string result = counter.Value;
			//assert
			Assert.AreEqual("B", result);
		}

		[TestMethod]
		public void AlphebetCounter_IncrementNegativeTwo_FromStart()
		{
			//arrange
			AlphabetCounter counter = new AlphabetCounter();
			int delta = -2;
			//act
			counter += delta;
			string result = counter.Value;
			//assert
			Assert.AreEqual("A", result);
		}

		[TestMethod]
		public void AlphebetCounter_IncrementPlace()
		{
			//arrange
			AlphabetCounter counter = new AlphabetCounter();
			int delta = 26;
			//act
			counter += delta;
			string result = counter.Value;
			//assert
			Assert.AreEqual("AA", result);
		}

		[TestMethod]
		public void AlphebetCounter_IncrementTwoPlaces()
		{
			//arrange
			AlphabetCounter counter = new AlphabetCounter();
			int delta = (26 * 27);
			//act
			counter += delta;
			string result = counter.Value;
			//assert
			Assert.AreEqual("AAA", result);
		}

		[TestMethod]
		public void AlphebetCounter_DecrementOne()
		{
			//arrange
			AlphabetCounter counter = new AlphabetCounter();
			counter.SetValue("Z");
			int delta = 1;
			//act
			counter -= delta;
			string result = counter.Value;
			//assert
			Assert.AreEqual("Y", result);
		}

		[TestMethod]
		public void AlphebetCounter_DecrementOne_FromStart()
		{
			//arrange
			AlphabetCounter counter = new AlphabetCounter();
			int delta = 1;
			//act
			counter -= delta;
			string result = counter.Value;
			//assert
			Assert.AreEqual("A", result);
		}

		[TestMethod]
		public void AlphebetCounter_DecrementNegativeOne()
		{
			//arrange
			AlphabetCounter counter = new AlphabetCounter();
			counter.SetValue("Z");
			int delta = -1;
			//act
			counter -= delta;
			string result = counter.Value;
			//assert
			Assert.AreEqual("AA", result);
		}

		[TestMethod]
		public void AlphebetCounter_DecrementPlace()
		{
			//arrange
			AlphabetCounter counter = new AlphabetCounter();
			counter.SetValue("AA");
			int delta = 26;
			//act
			counter -= delta;
			string result = counter.Value;
			//assert
			Assert.AreEqual("A", result);
		}

		[TestMethod]
		public void AlphebetCounter_DecrementTwoPlaces()
		{
			//arrange
			AlphabetCounter counter = new AlphabetCounter();
			counter.SetValue("AAA");
			int delta = (26 * 27);
			//act
			counter -= delta;
			string result = counter.Value;
			//assert
			Assert.AreEqual("A", result);
		}
	}
}
