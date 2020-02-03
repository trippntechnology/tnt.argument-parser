using System;
using System.IO;

namespace TNT.ArgumentParser
{
	/// <summary>
	/// An <see cref="Argument"/> that represents a file name.
	/// </summary>
	public class FileArgument : Argument
	{
		/// <summary>
		/// Type associated with this <see cref="Argument"/>
		/// </summary>
		public override string Type => "File";

		/// <summary>
		/// Gets <see cref="Argument.Value"/> as a <see cref="string"/>
		/// </summary>
		public new string Value => (string)base.Value;

		/// <summary>
		/// Indicates whether the file must exist
		/// </summary>
		public bool MustExist { get; set; }

		/// <summary>
		/// Initializes a <see cref="FileArgument"/>
		/// </summary>
		/// <param name="name">Name associated with argument</param>
		/// <param name="description">Description associated with argument</param>
		/// <param name="required">Indicates the argument is required (default = false</param>
		/// <param name="mustExist">Indicates whether the file must exist</param>
		public FileArgument(string name, string description, bool required = false, bool mustExist = false)
			: base(name, description, required)
		{
			this.MustExist = mustExist;
		}

		/// <summary>
		/// Transforms <paramref name="value"/> into a <see cref="string"/> that represents
		/// a file name
		/// </summary>
		/// <param name="value">File name</param>
		/// <returns><see cref="string"/> representing a file name</returns>
		/// <exception cref="ArgumentException">Thrown if <see cref="MustExist"/> is true, and it doesn't exist.</exception>
		protected override object Transform(string value)
		{
			if (MustExist && !File.Exists(value)) throw new ArgumentException();
			return value;
		}
	}
}
