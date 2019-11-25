using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TNT.ArgumentParser;

namespace Tests
{
	[TestClass]
	public class StringArgumentTests
	{
		const string NAME = "name";
		const string DESCRIPTION = "description";
		const string LONG_DESCRIPTION = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.";
		const string DEFAULT_VALUE = "default_value";
		const string VALUE = "value";
		const string NEW_VALUE = "new_value";

		[TestMethod]
		public void Constructor_UsingDefaultValues()
		{
			var sut = new StringArgument(NAME, DESCRIPTION);

			Assert.AreEqual(NAME, sut.Name);
			Assert.AreEqual(DESCRIPTION, sut.Description);
			Assert.IsFalse(sut.IsRequired);
			Assert.IsNull(sut.DefaultValue);
			Assert.AreEqual("[/name <String>]", sut.Syntax);
			Assert.AreEqual("  /name      description", sut.GetUsage());
		}

		[TestMethod]
		public void Constructor_RequiredTrue()
		{
			var sut = new StringArgument(NAME, DESCRIPTION, true);

			Assert.AreEqual(NAME, sut.Name);
			Assert.AreEqual(DESCRIPTION, sut.Description);
			Assert.IsTrue(sut.IsRequired);
			Assert.IsNull(sut.DefaultValue);
			Assert.AreEqual("/name <String>", sut.Syntax);
			Assert.AreEqual("  /name      description", sut.GetUsage());
		}

		[TestMethod]
		public void Constructor_WithDefault()
		{
			var sut = new StringArgument(NAME, DESCRIPTION, true, DEFAULT_VALUE);

			Assert.AreEqual(NAME, sut.Name);
			Assert.AreEqual(DESCRIPTION, sut.Description);
			Assert.IsFalse(sut.IsRequired);
			Assert.AreEqual(DEFAULT_VALUE, sut.DefaultValue);
			Assert.AreEqual("[/name <String>]", sut.Syntax);
			Assert.AreEqual("  /name      description (default: default_value)", sut.GetUsage());
			Assert.AreEqual(DEFAULT_VALUE, sut.Value);
		}

		[ExpectedException(typeof(ArgumentException))]
		[TestMethod]
		public void SetValue_NoValue()
		{
			var sut = new StringArgument(NAME, DESCRIPTION);

			try
			{
				sut.SetValue(String.Empty);
			}
			catch (Exception ex)
			{
				Assert.AreEqual("Argument 'name' is invalid", ex.Message);
				throw;
			}
		}

		[ExpectedException(typeof(ArgumentException))]
		[TestMethod]
		public void SetValue_Null()
		{
			var sut = new StringArgument(NAME, DESCRIPTION);

			try
			{
				sut.SetValue(null);
			}
			catch (Exception ex)
			{
				Assert.AreEqual("Argument 'name' is invalid", ex.Message);
				throw;
			}
		}

		[ExpectedException(typeof(ArgumentException))]
		[TestMethod]
		public void SetValue_AlreadySet()
		{
			var sut = new StringArgument(NAME, DESCRIPTION);

			try
			{
				sut.SetValue(VALUE);
				sut.SetValue(NEW_VALUE);
			}
			catch (Exception ex)
			{
				Assert.AreEqual("Argument was already provided", ex.Message);
				throw;
			}
		}

		[TestMethod]
		public void SetValue_Valid()
		{
			var sut = new StringArgument(NAME, DESCRIPTION);
			sut.SetValue(VALUE);
			Assert.AreEqual(VALUE, sut.Value);
		}

		[TestMethod]
		public void GetUsage_LongDescription()
		{
			var sut = new StringArgument(NAME, LONG_DESCRIPTION);
			var usage = sut.GetUsage();
			var expected = "  /name      Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam\n             nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam\n             erat, sed diam voluptua.";
			Assert.AreEqual(expected, sut.GetUsage());
		}
	}
}
