using System;

namespace TNT.ArgumentParser
{
	/// <summary>
	/// Represents a <see cref="bool"/> <see cref="Argument{T}"/>
	/// </summary>
	public class FlagArgument : Argument<bool?>
	{
		/// <summary>
		/// Initializes a <see cref="bool"/> argument parser
		/// </summary>
		/// <param name="name">Name associated with the argument</param>
		/// <param name="description">Description associated with the argument</param>
		public FlagArgument(string name, string description)
			: base(name, description, false, false)
		{
		}

		/// <summary>
		/// Sets the value to true
		/// </summary>
		public void SetValue() => base.SetValue(true);

		/// <summary>
		/// Always returns true
		/// </summary>
		/// <param name="value">Not used</param>
		/// <param name="msg">Not set</param>
		/// <returns>True</returns>
		protected override bool IsValid(bool? value, out string msg)
		{
			msg = String.Empty;
			return true;
		}
	}
}
