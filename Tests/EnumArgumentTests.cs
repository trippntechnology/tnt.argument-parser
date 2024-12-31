using System.Diagnostics.CodeAnalysis;
using TNT.ArgumentParser;

namespace Tests;

[ExcludeFromCodeCoverage]
public class EnumArgumentTests
{
  const string NAME = "name";
  const string DESC = "description";
  const TestEnum THREE = TestEnum.THREE;

  [Test]
  public void Constructor_DefaultValues()
  {
    var sut = new EnumArgument<TestEnum>(NAME, DESC);
    Assert.That(sut.DefaultValue, Is.Null);
    Assert.That(sut.Description, Is.EqualTo(DESC));
    Assert.That(sut.IsRequired, Is.False);
    Assert.That(sut.Name, Is.EqualTo(NAME));
    Assert.That(sut.Syntax, Is.EqualTo("[/name <TestEnum>]"));
    Assert.That(sut.Type, Is.EqualTo(typeof(TestEnum).Name));
  }

  [Test]
  public void Constructor_WithDefaultValues()
  {
    var sut = new EnumArgument<TestEnum>(NAME, DESC, THREE);
    Assert.That(sut.DefaultValue, Is.EqualTo(THREE));
    Assert.That(sut.Description, Is.EqualTo(DESC));
    Assert.That(sut.IsRequired, Is.False);
    Assert.That(sut.Name, Is.EqualTo(NAME));
    Assert.That(sut.Syntax, Is.EqualTo("[/name <TestEnum>]"));
    Assert.That(sut.Type, Is.EqualTo(typeof(TestEnum).Name));
    Assert.That(sut.Value, Is.EqualTo(THREE));
  }

  [Test]
  public void SetValue_Valid()
  {
    var sut = new EnumArgument<TestEnum>(NAME, DESC, true);
    Assert.That(sut.IsRequired, Is.True);
    sut.SetValue(THREE.ToString());
    Assert.That(sut.Value, Is.EqualTo(THREE));
  }

  [Test]
  public void SetValue_Invalid()
  {
    Assert.Throws<ArgumentException>(() =>
    {
      try
      {
        var sut = new EnumArgument<TestEnum>(NAME, DESC, true);
        Assert.That(sut.IsRequired, Is.True);
        sut.SetValue("foo");
      }
      catch (Exception ex)
      {
        Assert.That(ex.Message, Is.EqualTo("Argument 'name' is invalid"));
        throw;
      }
    });
  }

  [Test]
  public void GetUsage()
  {
    var sut = new EnumArgument<TestEnum>(NAME, DESC, true);
    Assert.That(sut.GetUsage(), Is.EqualTo("  /name      description\r\n\r\n               ONE\r\n               TWO\r\n               THREE\r\n               FOUR\r\n"));
  }

  [Test]
  public void GetUsage_EnumToDescription()
  {
    var sut = new EnumArgument<TestEnum>(NAME, DESC, true);
    sut.EnumToDescription = (e) => $"{e.ToString()} description";
    var expected = "  /name      description\r\n\r\n               ONE - ONE description\r\n               TWO - TWO description\r\n               THREE - THREE description\r\n               FOUR - FOUR description\r\n";
    var result = sut.GetUsage();
    Assert.That(result, Is.EqualTo(expected));
  }
}

public enum TestEnum
{
  ONE, TWO, THREE, FOUR
}
