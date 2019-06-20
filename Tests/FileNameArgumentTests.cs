using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TNT.ArgumentParser;

namespace Tests
{
	[TestClass]
	public class FileNameArgumentTests
	{
		const string NAME = "f";
		const string DESCRIPTION = "description";
		const string VALID_FILE_NAME = "file1.txt";
		const string INVALID_FILE_NAME = "bogus.txt";

		[TestMethod]
		public void Constructor()
		{
			var sut = new FileNameArgument(NAME, DESCRIPTION);
			Assert.IsFalse(sut.MustExist);
		}

		[TestMethod]
		public void Constructor_MustExist()
		{
			var sut = new FileNameArgument(NAME, DESCRIPTION, mustExist: true);
			Assert.IsTrue(sut.MustExist);
		}

		[TestMethod]
		public void Syntax()
		{
			var sut = new FileNameArgument(NAME, DESCRIPTION, true);
			Assert.AreEqual("/f <File>", sut.Syntax);
		}

		[TestMethod]
		public void Setvalue()
		{
			var sut = new FileNameArgument(NAME, DESCRIPTION);
			sut.SetValue(VALID_FILE_NAME);
			Assert.AreEqual(VALID_FILE_NAME, sut.Value);
		}

		[ExpectedException(typeof(ArgumentException))]
		[TestMethod]
		public void Setvalue_InvalidFileName()
		{
			try
			{
				var sut = new FileNameArgument(NAME, DESCRIPTION, mustExist: true);
				sut.SetValue(INVALID_FILE_NAME);
			}
			catch (Exception ex)
			{
				Assert.AreEqual("Argument 'f' is invalid", ex.Message);
				throw;
			}
		}
	}
}
