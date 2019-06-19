namespace TNT.ArgumentParser
{
	/// <summary>
	/// Represents a <see cref="bool"/> <see cref="Argument"/>
	/// </summary>
	public class FlagArgument : Argument
	{
		/// <summary>
		/// Casts <see cref="Argument.Value"/> to a <see cref="bool"/>
		/// </summary>
		public new bool Value => (bool)base.Value;

		/// <summary>
		/// The syntax used in a command argument
		/// </summary>
		public override string Syntax => $"[/{Name}]";

		/// <summary>
		/// Type represented by this argument
		/// </summary>
		public override string Type => typeof(bool).Name;

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
		/// Hides base class to send in value of true
		/// </summary>
		public void SetValue()
		{
			base.SetValue(true.ToString());
		}

		/// <summary>
		/// Always returns true
		/// </summary>
		/// <param name="value">Not used</param>
		/// <returns>True</returns>
		protected override object Transform(string value) => true;
	}
}
