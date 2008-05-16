
using System;
using System.Linq;
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
		/// Iterates the source applying the action with an index.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">The source.</param>
		/// <param name="action">The action.</param>
		public static void ForEachWithIndex<T>(this IEnumerable<T> source, Action<T, int> action)
		{
			if (source == null)
				throw new ArgumentNullException("source");

			if (action == null)
				throw new ArgumentNullException("action");

			WithIndex(source).ForEach(pair => action(pair.Second, pair.First));
		}

		/// <summary>
		/// Iterates the source returning values with an index.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">The source.</param>
		/// <returns></returns>
		public static IEnumerable<Tuple<int, T>> WithIndex<T>(this IEnumerable<T> source)
		{
			if (source == null)
				throw new ArgumentNullException("source");

			int position = 0;
			return source.Select((value) => Tuple.Create(position++, value));
		}

		/// <summary>
		/// Invokes the specified generator yielding a sequence of generated elements. To limit the number of results combine with <see cref="Enumerable.Take"/>.
		/// </summary>
		/// <typeparam name="T">The type of element in the sequence.</typeparam>
		/// <param name="generator">Returns a single element.</param>
		/// <returns>An infinite sequence of elements produced by calling specified generator.</returns>
		public static IEnumerable<T> Repeat<T>(Func<T> generator)
		{
			if (generator == null)
				throw new ArgumentNullException("generator");

			return RepeatIterator(generator);
		}		

		/// <summary>
		/// Creates a sequence containing the specified element.
		/// </summary>
		/// <typeparam name="T">The type of element.</typeparam>
		/// <param name="element">The element.</param>
		/// <param name="others">The others.</param>
		/// <returns>An enumerable sequence of the specified elements</returns>
		public static IEnumerable<T> ToSequence<T>(this T element)
		{
			yield return element;
		}

		/// <summary>
		/// Creates a sequence containing the specified elements.
		/// </summary>
		/// <typeparam name="T">The type of elements.</typeparam>
		/// <param name="element">The first element.</param>
		/// <param name="others">Additional elements.</param>
		/// <returns>An enumerable sequence of the specified elements.</returns>
		public static IEnumerable<T> ToSequence<T>(T element, params T[] additional)
		{
			yield return element;

			foreach (T other in additional)
				yield return other;
		}

		private static IEnumerable<T> RepeatIterator<T>(Func<T> generator)
		{
			while (true)
				yield return generator();
		}
	}
}
