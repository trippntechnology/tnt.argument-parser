using System.Text;
using TNT.ArgumentParser;

namespace ConsoleTest
{
  public class Arguments : ArgumentParser
  {
    const string FLAG = "f";
    const string STRING = "s";
    const string STRING_DEFAULT = "default value";
    const string INT = "i";
    const int INT_DEFAULT = 13;
    const string ENUM = "e";
    const DayOfWeek ENUM_DEFAULT = DayOfWeek.Sunday;
    const string VERSION = "v";
    const string GUID = "g";

    public bool TheFlag => (this[FLAG] as FlagArgument)?.Value ?? false;
    public string TheString => (this[STRING] as StringArgument)?.Value ?? STRING_DEFAULT;
    public int TheInt => (this[INT] as IntArgument)?.Value ?? INT_DEFAULT;
    public DayOfWeek TheDayOfWeek => (this[ENUM] as EnumArgument<DayOfWeek>)?.Value ?? ENUM_DEFAULT;
    public Version? TheVersion => (this[VERSION] as VersionArgument)?.Value;
    public Guid? TheGuid => (this[GUID] as GuidArgument)?.Value;

    public Arguments()
    {
      this.Add(new FlagArgument(FLAG, "flag description"));
      this.Add(new StringArgument(STRING, "string description", defaultValue: STRING_DEFAULT));
      this.Add(new IntArgument(INT, "int description", defaultValue: INT_DEFAULT));
      this.Add(new EnumArgument<DayOfWeek>(ENUM, "enum description", ENUM_DEFAULT));
      this.Add(new GuidArgument(GUID, "guid description"));

      var enumArg = (this[ENUM] as EnumArgument<DayOfWeek>);

      if (enumArg != null)
      {
        enumArg.EnumToDescription = (e) =>
        {
          return $"{e.ToString()} description";
        };
        this.Add(new VersionArgument(VERSION, "version description", true));
      }
    }

    protected override void SetRequiredDependencies()
    {
      var intValue = this[INT];
      if (TheFlag && intValue != null) intValue.IsRequired = true;
    }

    public override string ToString()
    {
      var sb = new StringBuilder();
      sb.AppendLine($"TheFlag: {TheFlag}");
      sb.AppendLine($"TheString: {TheString}");
      sb.AppendLine($"TheInt: {TheInt}");
      sb.AppendLine($"TheDayOfWeek: {TheDayOfWeek}");
      sb.AppendLine($"TheVersion: {TheVersion}");

      return sb.ToString();
    }
  }
}
