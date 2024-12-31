using System.Diagnostics.CodeAnalysis;
using TNT.ArgumentParser;

namespace Tests;

[ExcludeFromCodeCoverage]
public class GuidArgumentTest
{
  const string NAME = "name";
  const string DESCRIPTION = "description";
  const string GUID = "2A8B8210-DEAF-4AD1-8C80-C209AF5979E1";

  [Test]
  public void Constructor_Default()
  {
    var guidArg = new GuidArgument(NAME, DESCRIPTION);
    Assert.That(guidArg.Name, Is.EqualTo(NAME));
    Assert.That(guidArg.Description, Is.EqualTo(DESCRIPTION));
    Assert.That(guidArg.IsRequired, Is.False);
    Assert.That(guidArg.DefaultValue, Is.Null);
    Assert.That(guidArg.Value, Is.Null);
  }

  [Test]
  public void Constructor_IsRequired()
  {
    var guidArg = new GuidArgument(NAME, DESCRIPTION, true);
    Assert.That(guidArg.Name, Is.EqualTo(NAME));
    Assert.That(guidArg.Description, Is.EqualTo(DESCRIPTION));
    Assert.That(guidArg.IsRequired, Is.True);
    Assert.That(guidArg.DefaultValue, Is.Null);
    Assert.That(guidArg.Value, Is.Null);
  }


  [Test]
  public void Constructor_With_Default()
  {
    var defaultGuid = Guid.Parse(GUID);
    var guidArg = new GuidArgument(NAME, DESCRIPTION, true, defaultGuid);
    Assert.That(guidArg.Name, Is.EqualTo(NAME));
    Assert.That(guidArg.Description, Is.EqualTo(DESCRIPTION));
    Assert.That(guidArg.IsRequired, Is.False);
    Assert.That(guidArg.DefaultValue, Is.Not.Null);
    Assert.That(guidArg.DefaultValue, Is.EqualTo(defaultGuid));
    Assert.That(guidArg.Value, Is.EqualTo(defaultGuid));
  }

  [Test]
  public void Type_Test()
  {
    var guidArg = new GuidArgument(NAME, DESCRIPTION);
    Assert.That(guidArg.Type, Is.EqualTo(typeof(Guid).Name));
  }

  [Test]
  public void Transform_Test()
  {
    var guidArg = new GuidArgument(NAME, DESCRIPTION);
    var validGuid = Guid.NewGuid();

    Assert.That(() => guidArg.SetValue(null), Throws.Exception.TypeOf<ArgumentException>()
      .With.Message.EqualTo("Argument 'name' is invalid")
    );

    Assert.That(() => guidArg.SetValue("invalid guid"), Throws.Exception.TypeOf<ArgumentException>()
      .With.Message.EqualTo("Argument 'name' is invalid")
    );

    guidArg.SetValue(validGuid.ToString());
    Assert.That(guidArg.Value, Is.EqualTo(validGuid));
  }

  [Test]
  public void Usage_Test()
  {
    var guidArg = new GuidArgument(NAME, DESCRIPTION);

    Assert.That(guidArg.GetUsage(), Is.EqualTo("  /name      description"));
  }
}
