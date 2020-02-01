using System;
using System.Collections.Generic;
using System.Text;

namespace TNT.ArgumentParser
{
	class Pair<K, V>
	{
		public K Key { get; set; }
		public V Value { get; set; }

		public Pair(K key, V value)
		{
			this.Key = key;
			this.Value = value;
		}

		public Pair(K key): this(key, default)
		{

		}

		public override string ToString() => $"{Key}:{Value}";

		public (K, V) ToTuple() => (Key, Value);
	}
}
