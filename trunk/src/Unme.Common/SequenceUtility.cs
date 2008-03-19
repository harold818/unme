
using System;
using System.Collections.Generic;

namespace Unme.Common
{
	/// <summary>
	/// Provides convenience methods for working with sequences of elements (IEnumerable&lt;T&gt;)/>.
	/// </summary>
	public static class SequenceUtility
	{
		/// <summary>
		/// Invokes specified action with each element in the source sequence.
		/// </summary>
		/// <typeparam name="T">The type of element in the sequence.</typeparam>
		/// <param name="source">The source sequence.</param>
		/// <param name="action">The action to invoke.</param>
		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			if (source == null)
				throw new ArgumentNullException("source");
			if (action == null)
				throw new ArgumentNullException("action");

			foreach (var item in source)
				action(item);
		}

		/// <summary>
		/// Invokes the specified generator the specified number of times yielding a sequence of generated elements.
		/// </summary>
		/// <typeparam name="T">The type of element in the sequence.</typeparam>
		/// <param name="generator">Returns a single element.</param>
		/// <param name="length">The number of elements to include in the generated sequence.</param>
		/// <returns></returns>
		public static IEnumerable<T> Repeat<T>(Func<T> generator, int length)
		{
			if (generator == null)
				throw new ArgumentNullException("generator");
			if (length <= 0)
				throw new ArgumentOutOfRangeException("length");

			return RepeatIterator(generator, length);
		}		

		/// <summary>
		/// Creates a sequence containing the specified element, followed by all additional elements.
		/// </summary>
		/// <typeparam name="T">The type of element.</typeparam>
		/// <param name="element">The element.</param>
		/// <param name="others">The others.</param>
		/// <returns></returns>
		public static IEnumerable<T> ToSequence<T>(this T element, params T[] additionalElements)
		{
			yield return element;

			foreach (T other in additionalElements)
				yield return other;
		}

		private static IEnumerable<T> RepeatIterator<T>(Func<T> generator, int length)
		{
			for (int i = 0; i < length; i++)
				yield return generator();
		}
	}
}
