﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TNT.ArgumentParser;

namespace Tests
{
	[TestClass]
	public class EnumArgumentTests
	{
		const string NAME = "name";
		const string DESC = "description";
		const TestEnum THREE = TestEnum.THREE;

		[TestMethod]
		public void Constructor_DefaultValues()
		{
			var sut = new EnumArgument<TestEnum>(NAME, DESC);
			Assert.IsNull(sut.DefaultValue);
			Assert.AreEqual(DESC, sut.Description);
			Assert.IsFalse(sut.IsRequired);
			Assert.AreEqual(NAME, sut.Name);
			Assert.AreEqual("[/name <TestEnum>]", sut.Syntax);
			Assert.AreEqual(typeof(TestEnum).Name, sut.Type);
			//Assert.IsNull(sut.Value);
		}

		[TestMethod]
		public void Constructor_WithDefaultValues()
		{
			var sut = new EnumArgument<TestEnum>(NAME, DESC, THREE);
			Assert.AreEqual(THREE, sut.DefaultValue);
			Assert.AreEqual(DESC, sut.Description);
			Assert.IsFalse(sut.IsRequired);
			Assert.AreEqual(NAME, sut.Name);
			Assert.AreEqual("[/name <TestEnum>]", sut.Syntax);
			Assert.AreEqual(typeof(TestEnum).Name, sut.Type);
			Assert.AreEqual(THREE, sut.Value);
		}

		[TestMethod]
		public void SetValue_Valid()
		{
			var sut = new EnumArgument<TestEnum>(NAME, DESC, true);
			Assert.IsTrue(sut.IsRequired);
			sut.SetValue(THREE.ToString());
			Assert.AreEqual(THREE, sut.Value);
		}

		[ExpectedException(typeof(ArgumentException))]
		[TestMethod]
		public void SetValue_Invalid()
		{
			try
			{
				var sut = new EnumArgument<TestEnum>(NAME, DESC, true);
				Assert.IsTrue(sut.IsRequired);
				sut.SetValue("foo");
			}
			catch (Exception ex)
			{
				Assert.AreEqual("Argument 'name' is invalid", ex.Message);
				throw;
			}
		}

		[TestMethod]
		public void GetUsage()
		{
			var sut = new EnumArgument<TestEnum>(NAME, DESC, true);
			Assert.AreEqual("  /name      description\r\n\r\n               ONE\r\n               TWO\r\n               THREE\r\n               FOUR\r\n", sut.GetUsage());
		}

		[TestMethod]
		public void GetUsage_EnumToDescription()
		{
			var sut = new EnumArgument<TestEnum>(NAME, DESC, true);
			sut.EnumToDescription = (e) => $"{e.ToString()} description";
			var expected = "  /name      description\r\n\r\n               ONE - ONE description\r\n               TWO - TWO description\r\n               THREE - THREE description\r\n               FOUR - FOUR description\r\n";
			var result = sut.GetUsage();
			Assert.AreEqual(expected, result);
		}
	}

	public enum TestEnum
	{
		ONE, TWO, THREE, FOUR
	}
}
