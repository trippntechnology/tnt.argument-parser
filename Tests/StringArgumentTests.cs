using System.Diagnostics.CodeAnalysis;
using TNT.ArgumentParser;

namespace Tests;

[ExcludeFromCodeCoverage]
public class StringArgumentTests
{
	const string NAME = "name";
	const string DESCRIPTION = "description";
	const string LONG_DESCRIPTION = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.";
	const string DEFAULT_VALUE = "default_value";
	const string VALUE = "value";
	const string NEW_VALUE = "new_value";

	[Test]
	public void Constructor_UsingDefaultValues()
	{
		var sut = new StringArgument(NAME, DESCRIPTION);

		Assert.That(sut.Name, Is.EqualTo(NAME));
		Assert.That(sut.Description, Is.EqualTo(DESCRIPTION));
		Assert.IsFalse(sut.IsRequired);
		Assert.IsNull(sut.DefaultValue);
		Assert.That(sut.Syntax, Is.EqualTo("[/name <String>]"));
		Assert.That(sut.GetUsage(), Is.EqualTo("  /name      description"));
	}

	[Test]
	public void Constructor_RequiredTrue()
	{
		var sut = new StringArgument(NAME, DESCRIPTION, true);

		Assert.That(sut.Name, Is.EqualTo(NAME));
		Assert.That(sut.Description, Is.EqualTo(DESCRIPTION));
		Assert.IsTrue(sut.IsRequired);
		Assert.IsNull(sut.DefaultValue);
		Assert.That(sut.Syntax, Is.EqualTo("/name <String>"));
		Assert.That(sut.GetUsage(), Is.EqualTo("  /name      description"));
	}

	[Test]
	public void Constructor_WithDefault()
	{
		var sut = new StringArgument(NAME, DESCRIPTION, true, DEFAULT_VALUE);

		Assert.That(sut.Name, Is.EqualTo(NAME));
		Assert.That(sut.Description, Is.EqualTo(DESCRIPTION));
		Assert.IsFalse(sut.IsRequired);
		Assert.That(sut.DefaultValue, Is.EqualTo(DEFAULT_VALUE));
		Assert.That(sut.Syntax, Is.EqualTo("[/name <String>]"));
		Assert.That(sut.GetUsage(), Is.EqualTo("  /name      description (default: default_value)"));
		Assert.That(sut.Value, Is.EqualTo(DEFAULT_VALUE));
	}

	[Test]
	public void SetValue_NoValue()
	{
		Assert.Throws<ArgumentException>(() =>
		{
			var sut = new StringArgument(NAME, DESCRIPTION);

			try
			{
				sut.SetValue(String.Empty);
			}
			catch (Exception ex)
			{
				Assert.That(ex.Message, Is.EqualTo("Argument 'name' is invalid"));
				throw;
			}
		});
	}

	[Test]
	public void SetValue_Null()
	{
		Assert.Throws<ArgumentException>(() =>
		{
      StringArgument sut = new StringArgument(NAME, DESCRIPTION);

			try
			{
				sut.SetValue(null);
			}
			catch (Exception ex)
			{
				Assert.That(ex.Message, Is.EqualTo("Argument 'name' is invalid"));
				throw;
			}
		});
	}

	[Test]
	public void SetValue_AlreadySet()
	{
		Assert.Throws<ArgumentException>(() =>
		{
			var sut = new StringArgument(NAME, DESCRIPTION);

			try
			{
				sut.SetValue(VALUE);
				sut.SetValue(NEW_VALUE);
			}
			catch (Exception ex)
			{
				Assert.That(ex.Message, Is.EqualTo("Argument was already provided"));
				throw;
			}
		});
	}

	[Test]
	public void SetValue_Valid()
	{
		var sut = new StringArgument(NAME, DESCRIPTION);
		sut.SetValue(VALUE);
		Assert.That(sut.Value, Is.EqualTo(VALUE));
	}

	[Test]
	public void GetUsage_LongDescription()
	{
		var sut = new StringArgument(NAME, LONG_DESCRIPTION);
		var usage = sut.GetUsage();
		var expected = "  /name      Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam\n             nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam\n             erat, sed diam voluptua.";
		Assert.That(sut.GetUsage(), Is.EqualTo(expected));
	}
}
