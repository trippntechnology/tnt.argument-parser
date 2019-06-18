using System;
using System.Collections.Generic;
using System.Text;

namespace TNT.ArgumentParser
{
	/// <summary>
	/// Abstract class that represents an argument
	/// </summary>
	/// <typeparam name="T">Type associated with this argument</typeparam>
	public abstract class Argument<T> : IArgument
	{
		private const int USAGE_LINE_LENGTH = 70;

		private T m_Value = default(T);

		/// <summary>
		/// Argument name
		/// </summary>
		//public virtual string Name { get; internal set; }

		/// <summary>
		/// Argument description
		/// </summary>
		public virtual string Description { get; internal set; }

		/// <summary>
		/// Indicates if the argument is required
		/// </summary>
		public virtual bool IsRequired { get; protected set; }

		/// <summary>
		/// Contains the value associated with the argument name
		/// </summary>
		public virtual T Value { get { return m_Value == null ? DefaultValue : m_Value; } }

		/// <summary>
		/// Default value
		/// </summary>
		public virtual T DefaultValue { get; internal set; }

		/// <summary>
		/// Get the syntax of the argument as displayed on the command-line
		/// </summary>
		public virtual string Syntax
		{
			get
			{
				string syntax = $"/{Name} <{typeof(T).Name}>";

				if (!IsRequired)
				{
					syntax = $"[{syntax}]";
				}

				return syntax;
			}
		}

		/// <summary>
		/// Initializes an <see cref="Argument{T}"/>
		/// </summary>
		/// <param name="name">Name associated with argument</param>
		/// <param name="description">Description associated with argument</param>
		/// <param name="required">Indicates the argument is required (default = false). Note, if a <paramref name="defaultValue"/>
		/// is provided, the argument will not be required, i.e. set to false.</param>
		/// <param name="defaultValue">Indicates the default value of the argument (default = default(T))</param>
		protected Argument(string name, string description, bool required = false, T defaultValue = default(T))
		{
			Name = name;
			Description = description;
			DefaultValue = defaultValue;
			IsRequired = DefaultValue == null || DefaultValue.Equals(default(T)) == true ? required : false;
		}

		/// <summary>
		/// Implement by subclass to validate the <see cref="Value"/>
		/// </summary>
		/// <param name="value">Value of type <typeparamref name="T"/></param>
		/// <param name="msg">Message that can be returned by subclass if <paramref name="value"/> is
		/// not valid</param>
		/// <returns>True if valid, otherwise false. <paramref name="msg"/> should return a descriptive message
		/// as to why <paramref name="value"/> is not valid</returns>
		protected abstract bool IsValid(T value, out string msg);

		/// <summary>
		/// Sets the argument value
		/// </summary>
		/// <param name="value"></param>
		/// <exception cref="ArgumentException">Thrown if the value is not valid</exception>
		public virtual void SetValue(T value)
		{
			if (m_Value != null) throw new ArgumentException(Resources.ARGUMENT_ALREADY_SET);
			if (!IsValid(value, out string msg)) throw new ArgumentException(msg);

			m_Value = value;
		}

		/// <summary>
		/// Gets the argument usage
		/// </summary>
		/// <returns>Argument usage</returns>
		public virtual string GetUsage()
		{
			StringBuilder usage = new StringBuilder();
			var defaultText = String.Empty;
			if (this.DefaultValue?.Equals(default(T)) == false) { defaultText = $" (default: {this.DefaultValue.ToString()})"; }
			//if (this.DefaultValue != null || this.DefaultValue?.Equals(default(T)) == true) { defaultText = $" (default: {this.DefaultValue.ToString()})"; }
			string description = $"{this.Description}{defaultText}";
			string[] lines = WrapLines(description);

			for (int index = 0; index < lines.Length; index++)
			{
				usage.AppendFormat("{0}{1,-10}{2}", index == 0 ? "  /" : "\n   ", index == 0 ? Name : string.Empty, lines[index]);
			}

			return usage.ToString();
		}

		/// <summary>
		/// Creates an array of <see cref="string"/> given a <paramref name="line"/> that exceeds the <see cref="USAGE_LINE_LENGTH"/>
		/// </summary>
		/// <param name="line">Line to wrap</param>
		/// <returns>Array of <see cref="string"/></returns>
		protected virtual string[] WrapLines(string line)
		{
			var lines = new List<string>();

			while (line.Length > 0)
			{
				if (line.Length <= USAGE_LINE_LENGTH)
				{
					lines.Add(line);
					break;
				}
				else
				{
					string currentLine = line.Substring(0, USAGE_LINE_LENGTH);
					int lastIndexOf = currentLine.LastIndexOf(' ');

					if (lastIndexOf > 0)
					{
						currentLine = currentLine.Substring(0, lastIndexOf);
					}
					else
					{
						lastIndexOf = line.Length - 1;
					}

					lines.Add(currentLine);
					line = line.Substring(lastIndexOf + 1);
				}
			}

			return lines.ToArray();
		}
	}
}
