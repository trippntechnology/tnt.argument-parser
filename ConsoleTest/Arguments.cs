using System;
using System.Text;
using TNT.ArgumentParser;

namespace ConsoleTest
{
	public class Arguments : ArgumentParser
	{
		const string FLAG = "f";
		const string STRING = "s";
		const string INT = "i";
		const string ENUM = "e";
		const string VERSION = "v";

		public bool TheFlag => (this[FLAG] as FlagArgument).Value;
		public string TheString => (this[STRING] as StringArgument).Value;
		public int TheInt => (this[INT] as IntArgument).Value;
		public DayOfWeek TheDayOfWeek => (this[ENUM] as EnumArgument<DayOfWeek>).Value;
		public Version TheVersion => (this[VERSION] as VersionArgument).Value;

		public Arguments()
		{
			this.Add(new FlagArgument(FLAG, "flag description"));
			this.Add(new StringArgument(STRING, "string description", defaultValue: "default value"));
			this.Add(new IntArgument(INT, "int description", defaultValue: 13));
			this.Add(new EnumArgument<DayOfWeek>(ENUM, "enum description", DayOfWeek.Saturday));

			(this[ENUM] as EnumArgument<DayOfWeek>).EnumToDescription = (e) =>
			{
				return $"{e.ToString()} description";
			};
			this.Add(new VersionArgument(VERSION, "version description", true));
		}

		protected override void SetRequiredDependencies()
		{
			if (TheFlag) this[INT].IsRequired = true;
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
