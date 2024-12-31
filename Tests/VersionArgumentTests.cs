using System.Diagnostics.CodeAnalysis;
using TNT.ArgumentParser;

namespace Tests;

[ExcludeFromCodeCoverage]
public class VersionArgumentTests
{
  const string NAME = "name";
  const string DESCRIPTION = "description";
  const string VERSION = "1.2.3.4";

  [Test]
  public void Constructor_Defaults()
  {
    var sut = new VersionArgument(NAME, DESCRIPTION);
    Assert.That(sut.Name, Is.EqualTo(NAME));
    Assert.That(sut.Description, Is.EqualTo(DESCRIPTION));
    Assert.That(sut.DefaultValue, Is.Null);
    Assert.That(sut.GetUsage(), Is.EqualTo("  /name      description"));
    Assert.That(sut.IsRequired, Is.False);
    Assert.That(sut.Syntax, Is.EqualTo("[/name <Version>]"));
    Assert.That(sut.Type, Is.EqualTo("Version"));
    Assert.That(sut.Value, Is.Null);
  }

  [Test]
  public void Constructor_Required()
  {
    var sut = new VersionArgument(NAME, DESCRIPTION, true);
    Assert.That(sut.Name, Is.EqualTo(NAME));
    Assert.That(sut.Description, Is.EqualTo(DESCRIPTION));
    Assert.That(sut.DefaultValue, Is.Null);
    Assert.That(sut.GetUsage(), Is.EqualTo("  /name      description"));
    Assert.That(sut.IsRequired, Is.True);
    Assert.That(sut.Syntax, Is.EqualTo("/name <Version>"));
    Assert.That(sut.Type, Is.EqualTo("Version"));
    Assert.That(sut.Value, Is.Null);
  }

  [Test]
  public void Constructor_With_Default()
  {
    var sut = new VersionArgument(NAME, DESCRIPTION, true, Version.Parse(VERSION));
    Assert.That(sut.Name, Is.EqualTo(NAME));
    Assert.That(sut.Description, Is.EqualTo(DESCRIPTION));
    Assert.That(sut.DefaultValue, Is.EqualTo(Version.Parse(VERSION)));
    Assert.That(sut.GetUsage(), Is.EqualTo("  /name      description (default: 1.2.3.4)"));
    Assert.That(sut.IsRequired, Is.False  );
    Assert.That(sut.Syntax, Is.EqualTo("[/name <Version>]"));
    Assert.That(sut.Type, Is.EqualTo("Version"));
    Assert.That(sut.Value, Is.EqualTo(Version.Parse(VERSION)));
  }

  [Test]
  public void SetValue_Valid()
  {
    var sut = new VersionArgument(NAME, DESCRIPTION);
    sut.SetValue(VERSION);
    Assert.That(sut.Value, Is.EqualTo(Version.Parse(VERSION)));
  }

  [Test]
  public void SetValue_Null()
  {
    Assert.Throws<ArgumentException>(() =>
    {
      try
      {
        var sut = new VersionArgument(NAME, DESCRIPTION);
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
  public void SetValue_Invalid()
  {
    Assert.Throws<ArgumentException>(() =>
    {
      try
      {
        var sut = new VersionArgument(NAME, DESCRIPTION);
        sut.SetValue("bogus");
      }
      catch (Exception ex)
      {
        Assert.That(ex.Message, Is.EqualTo("Argument 'name' is invalid"));
        throw;
      }
    });
  }
}
