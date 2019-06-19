using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TNT.ArgumentParser;

namespace Tests
{
	[TestClass]
	public class IntArgumentTests
	{
		const string NAME = "name";
		const string DESCRIPTION = "description";

		[TestMethod]
		public void Constructor_Defaults()
		{
			var sut = new IntArgument(NAME, DESCRIPTION);
			Assert.AreEqual(NAME, sut.Name);
			Assert.AreEqual(DESCRIPTION, sut.Description);
			Assert.IsNull(sut.DefaultValue);
			Assert.IsFalse(sut.IsRequired);
			Assert.AreEqual(typeof(Int32).Name, sut.Type);
		}

		[TestMethod]
		public void SetValue_Valid()
		{
			var sut = new IntArgument(NAME, DESCRIPTION);
			sut.SetValue("13");
			Assert.AreEqual(13, sut.Value);
		}

		[ExpectedException(typeof(FormatException))]
		[TestMethod]
		public void SetValue_Invalid()
		{
			try
			{
				var sut = new IntArgument(NAME, DESCRIPTION);
				sut.SetValue("ab");
			}
			catch (Exception ex)
			{
				Assert.AreEqual("Input string was not in a correct format.", ex.Message);
				throw;
			}
		}
	}
}
