using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TNT.ArgumentParser;

namespace Tests
{
	[TestClass]
	public class VersionArgumentTests
	{
		const string NAME = "name";
		const string DESCRIPTION = "description";
		const string VERSION = "1.2.3.4";

		[TestMethod]
		public void Constructor_Defaults()
		{
			var sut = new VersionArgument(NAME, DESCRIPTION);
			Assert.AreEqual(sut.Name, NAME);
			Assert.AreEqual(sut.Description, DESCRIPTION);
			Assert.IsNull(sut.DefaultValue);
			Assert.AreEqual("  /name      description", sut.GetUsage());
			Assert.IsFalse(sut.IsRequired);
			Assert.AreEqual("[/name <Version>]", sut.Syntax);
			Assert.AreEqual("Version", sut.Type);
			Assert.IsNull(sut.Value);
		}

		[TestMethod]
		public void Constructor_Required()
		{
			var sut = new VersionArgument(NAME, DESCRIPTION, true);
			Assert.AreEqual(sut.Name, NAME);
			Assert.AreEqual(sut.Description, DESCRIPTION);
			Assert.IsNull(sut.DefaultValue);
			Assert.AreEqual("  /name      description", sut.GetUsage());
			Assert.IsTrue(sut.IsRequired);
			Assert.AreEqual("/name <Version>", sut.Syntax);
			Assert.AreEqual("Version", sut.Type);
			Assert.IsNull(sut.Value);
		}

		[TestMethod]
		public void Constructor_With_Default()
		{
			var sut = new VersionArgument(NAME, DESCRIPTION, true, Version.Parse(VERSION));
			Assert.AreEqual(sut.Name, NAME);
			Assert.AreEqual(sut.Description, DESCRIPTION);
			Assert.AreEqual(Version.Parse(VERSION), sut.DefaultValue);
			Assert.AreEqual("  /name      description (default: 1.2.3.4)", sut.GetUsage());
			Assert.IsFalse(sut.IsRequired);
			Assert.AreEqual("[/name <Version>]", sut.Syntax);
			Assert.AreEqual("Version", sut.Type);
			Assert.AreEqual(Version.Parse(VERSION), sut.Value);
		}

		[TestMethod]
		public void SetValue_Valid()
		{
			var sut = new VersionArgument(NAME, DESCRIPTION);
			sut.SetValue(VERSION);
			Assert.AreEqual(Version.Parse(VERSION), sut.Value);
		}

		[ExpectedException(typeof(ArgumentException))]
		[TestMethod]
		public void SetValue_Null()
		{
			try
			{
				var sut = new VersionArgument(NAME, DESCRIPTION);
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
		public void SetValue_Invalid()
		{
			try
			{
				var sut = new VersionArgument(NAME, DESCRIPTION);
				sut.SetValue("bogus");
			}
			catch (Exception ex)
			{
				Assert.AreEqual("Argument 'name' is invalid", ex.Message);
				throw;
			}
		}
	}
}
