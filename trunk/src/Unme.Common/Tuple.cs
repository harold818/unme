
using System;
using System.Diagnostics;

namespace Unme.Common
{
	/// <summary>
	/// A Pair Tuple.
	/// </summary>
	[DebuggerDisplay("{First}, {Second}")]
	[Serializable]
	public class Tuple<TFirst, TSecond> : IEquatable<Tuple<TFirst, TSecond>>
	{
		/// <summary>
		/// The first element
		/// </summary>
		public TFirst First { get; private set; }

		/// <summary>
		/// The second element
		/// </summary>
		public TSecond Second { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Tuple&lt;T1, T2&gt;"/> struct.
		/// </summary>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		internal Tuple(TFirst first, TSecond second)
		{
			First = first;
			Second = second;
		}

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		/// true if the current object is equal to the other parameter; otherwise, false.
		/// </returns>
		public bool Equals(Tuple<TFirst, TSecond> other)
		{
			return other != null && First.Equals(other.First) && Second.Equals(other.Second);
		}

		/// <summary>
		/// Indicates whether this instance and a specified object are equal.
		/// </summary>
		/// <param name="obj">Another object to compare to.</param>
		/// <returns>
		/// true if obj and this instance are the same type and represent the same value; otherwise, false.
		/// </returns>
		public override bool Equals(object obj)
		{
			return Equals(obj as Tuple<TFirst, TSecond>);
		}

		/// <summary>
		/// Returns the hash code for this instance.
		/// </summary>
		/// <returns>
		/// A 32-bit signed integer that is the hash code for this instance.
		/// </returns>
		public override int GetHashCode()
		{
			return First.GetHashCode() ^ Second.GetHashCode();
		}
	}

	/// <summary>
	/// A Pair Tuple.
	/// </summary>
	public static class Tuple
	{
		public static Tuple<T1, T2> Create<T1, T2>(T1 first, T2 second)
		{
			return new Tuple<T1, T2>(first, second);
		}
	}
}
