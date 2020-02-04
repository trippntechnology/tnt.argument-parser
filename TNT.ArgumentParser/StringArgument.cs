using System;

namespace TNT.ArgumentParser
{
	/// <summary>
	/// Represents a <see cref="string"/> <see cref="Argument"/>
	/// </summary>
	public class StringArgument : Argument
	{
		/// <summary>
		/// The value of the <see cref="StringArgument"/>
		/// </summary>
		public new string Value => (string)base.Value;

		/// <summary>
		/// Type represented by this <see cref="Argument"/>
		/// </summary>
		public override string Type => typeof(string).Name;

		/// <summary>
		/// Initializes a <see cref="string"/> argument parser 
		/// </summary>
		/// <param name="name">Name associate with the argument</param>
		/// <param name="description">Description of the argument</param>
		/// <param name="required">Indicates the argument is required. (default: false)</param>
		/// <param name="defaultValue">Provides a default value to use. Argument also is not required when this is 
		/// provided</param>
		public StringArgument(string name, string description, bool required = false, string defaultValue = null)
			: base(name, description, required, defaultValue)
		{
		}

		/// <summary>
		/// Sets the <see cref="string"/> on this <see cref="Argument.Value"/>
		/// </summary>
		/// <param name="value">Value that is parsed</param>
		/// <returns>True if <paramref name="value"/> is valid, false otherwise</returns>
		/// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is null or white space</exception>
		protected override object Transform(string value)
		{
			if (String.IsNullOrWhiteSpace(value)) { throw new ArgumentException(); }
			return value;
		}
	}
}
