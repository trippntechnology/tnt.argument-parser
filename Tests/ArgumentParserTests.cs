using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using TNT.ArgumentParser;

namespace Tests
{
	[TestClass]
	public class ArgumentParserTests
	{
		const string STRING_ARG = "s";
		const string STRING_DESC = "The string description";
		const string FLAG_ARG = "f";
		const string FLAG_DESC = "The flag description";
		const string INT_ARG = "i";
		const string INT_DESC = "The int description";

		[TestMethod]
		public void Add_UniqueArguments()
		{
			var sut = new TestParser();
			Assert.AreEqual(4, sut.Count());
		}

		[ExpectedException(typeof(ArgumentException))]
		[TestMethod]
		public void Add_DuplicateArguments()
		{
			try
			{
				var sut = new TestParser();
				sut.Add(new IntArgument(INT_ARG, INT_DESC));
			}
			catch (Exception ex)
			{
				Assert.AreEqual("Argument 'i' already exists", ex.Message);
				throw;
			}
		}

		[TestMethod]
		public void ToPairs()
		{
			var args = new string[] { "/s", "Apple Sauce", "/f", "/i", "57" };
			var sut = new TestParser();

			var result = sut.ToPairs(args);
			var expected = new List<(string name, string value)>() { ("s", "Apple Sauce"), ("f", null), ("i", "57") };
			CollectionAssert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Parse_HappyPath()
		{
			var sut = new TestParser();
			var args = new string[] { "/s", "Apple Sauce", "/f", "/i", "57" };

			Assert.IsTrue(sut.Parse(args));
		}

		[ExpectedException(typeof(ArgumentException))]
		[TestMethod]
		public void Parse_InvalidArgument()
		{
			try
			{
				var sut = new TestParser();
				var args = new string[] { "/s", "Apple Sauce", "/f", "/i", "57", "/j" };
				sut.Parse(args, false);
			}
			catch (Exception ex)
			{
				Assert.AreEqual("Argument 'j' is invalid", ex.Message);
				throw;
			}
		}

		[TestMethod]
		public void Parse_ShowUsage()
		{
			var sut = new TestParser();
			var args = new string[] { "/s", "Apple Sauce", "/f", "/i", "57", "/h" };
			Assert.IsFalse(sut.UsageShown);
			sut.Parse(args);
			Assert.IsTrue(sut.UsageShown);
		}

		[ExpectedException(typeof(ArgumentNullException))]
		[TestMethod]
		public void Parse_NullArgs()
		{
			try
			{
				var sut = new TestParser();
				sut.Parse(null, false);
			}
			catch (Exception ex)
			{
				Assert.AreEqual("Value cannot be null.\r\nParameter name: value", ex.Message);
				throw;
			}
		}

		[ExpectedException(typeof(ArgumentException))]
		[TestMethod]
		public void Parse_UnassignedRequiredArgs()
		{
			try
			{
				var sut = new TestParser();
				sut["s"].IsRequired = true;
				var args = new string[] { "/f", "/i", "57" };
				sut.Parse(args, false);
			}
			catch (Exception ex)
			{
				Assert.AreEqual("Argument 's' is required", ex.Message);
				throw;
			}
		}
	}

	public class TestParser : ArgumentParser
	{
		const string STRING_ARG = "s";
		const string STRING_DESC = "The string description";
		const string FLAG_ARG = "f";
		const string FLAG_DESC = "The flag description";
		const string INT_ARG = "i";
		const string INT_DESC = "The int description";

		public bool UsageShown { get; set; } = false;

		public TestParser()
		{
			Add(new StringArgument(STRING_ARG, STRING_DESC));
			Add(new FlagArgument(FLAG_ARG, FLAG_DESC));
			Add(new IntArgument(INT_ARG, INT_DESC));
		}

		public new List<(string name, string value)> ToPairs(string[] args) => base.ToPairs(args);

		protected override string Usage()
		{
			UsageShown = true;
			return base.Usage();
		}
	}
}
