using System;

namespace TNT.ArgumentParser
{
	/// <summary>
	/// Represents a <see cref="string"/> <see cref="Argument{T}"/>
	/// </summary>
	public class StringArgument : Argument<string>
	{
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
		/// Validates the string parsed by the <see cref="Argument{T}"/>
		/// </summary>
		/// <param name="value">Value that is parsed</param>
		/// <param name="msg">Message that can be return if not valid</param>
		/// <returns>True if <paramref name="value"/> is valid, false otherwise</returns>
		protected override bool IsValid(string value, out string msg)
		{
			msg = string.Empty;

			if (String.IsNullOrWhiteSpace(value)) { msg = Resources.ARGUMENT_MUST_NOT_BE_EMPTY; }

			return String.IsNullOrWhiteSpace(msg);
		}
	}
}
