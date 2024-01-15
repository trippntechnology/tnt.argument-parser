using System;

namespace TNT.ArgumentParser
{
	/// <summary>
	/// Resents a mutable key/value pair
	/// </summary>
	/// <typeparam name="K">Type of <see cref="Key"/></typeparam>
	/// <typeparam name="V">Type of <see cref="Value"/></typeparam>
	public class Pair<K, V>
	{
		/// <summary>
		/// Key
		/// </summary>
		public K Key { get; set; }

		/// <summary>
		/// Value
		/// </summary>
		public V? Value { get; set; }

		/// <summary>
		/// Initializes a the <see cref="Key"/> and <see cref="Value"/>
		/// </summary>
		/// <param name="key"><see cref="Key"/> value</param>
		/// <param name="value"><see cref="Value"/> value</param>
		public Pair(K key, V? value)
		{
			this.Key = key;
			this.Value = value;
		}

		/// <summary>
		/// Initializes a the <see cref="Key"/> and with a <see cref="Value"/> of null
		/// </summary>
		/// <param name="key"><see cref="Key"/> value</param>
		public Pair(K key) : this(key, default)
		{

		}

		/// <summary>
		/// Formats <see cref="Pair{K, V}"/> for viewing
		/// </summary>
		/// <returns>Formatted <see cref="Pair{K, V}"/> to make viewing easier</returns>
		public override string ToString() => $"({Key}, {Value})";

		/// <summary>
		/// Converts to a <see cref="Tuple"/> with the <see cref="Key"/> and <see cref="Value"/>
		/// </summary>
		/// <returns><see cref="Tuple"/> with the <see cref="Key"/> and <see cref="Value"/></returns>
		public (K, V?) ToTuple() => (Key, Value);
	}
}
