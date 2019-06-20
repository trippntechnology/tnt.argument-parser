using System;
using System.IO;

namespace TNT.ArgumentParser
{
	public class FileNameArgument : Argument
	{
		public override string Type => "File";

		public bool MustExist { get; protected set; }

		/// <summary>
		/// Initializes a <see cref="FileNameArgument"/>
		/// </summary>
		/// <param name="name">Name associated with argument</param>
		/// <param name="description">Description associated with argument</param>
		/// <param name="required">Indicates the argument is required (default = false</param>
		/// <param name="mustExist">Indicates whether the file must exist</param>
		public FileNameArgument(string name, string description, bool required = false, bool mustExist = false)
			: base(name, description, required)
		{
			this.MustExist = mustExist;
		}

		protected override object Transform(string value)
		{
			if (MustExist && !File.Exists(value)) throw new ArgumentException(String.Format(Resources.INVALID_ARGUMENT_NAME, Name));
			return value;
		}
	}
}
