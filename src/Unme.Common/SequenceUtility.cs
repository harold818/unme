
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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

		public static void ForEach<T1, T2>(this IEnumerable<T1> first, IEnumerable<T2> second, Action<T1, T2> action)
		{
			if (first == null)
				throw new ArgumentNullException("first");
			if (second == null)
				throw new ArgumentNullException("second");
			if (action == null)
				throw new ArgumentNullException("action");

			ForEach(first, second, (left, right) => { action(left, right); return true; });
		}

		public static void ForEach<T1, T2>(this IEnumerable<T1> first, IEnumerable<T2> second, Func<T1, T2, bool> func)
		{
			if (first == null)
				throw new ArgumentNullException("first");
			if (second == null)
				throw new ArgumentNullException("second");
			if (func == null)
				throw new ArgumentNullException("func");

			var enumerator1 = first.GetEnumerator();
			var enumerator2 = second.GetEnumerator();
			bool continueIterating = true;

			while (continueIterating)
			{
				switch (Convert.ToByte(enumerator1.MoveNext()) | (Convert.ToByte(enumerator2.MoveNext()) << 1))
				{
					case 0:
						return;
					case 3:
						continueIterating = func(enumerator1.Current, enumerator2.Current);
						break;
					default:
						throw new ArgumentException("Sequences were of differing lengths.");
				}
			}
		}

		/// <summary>
		/// Iterates the source applying the action with an index.
		/// </summary>
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
		/// <param name="source">The source.</param>
		/// <returns></returns>
		public static IEnumerable<Tuple<int, T>> WithIndex<T>(this IEnumerable<T> source)
		{
			if (source == null)
				throw new ArgumentNullException("source");

			int position = 0;
			return source.Select(value => Tuple.Create(position++, value));
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
		/// <param name="additional">Additional elements.</param>
		/// <returns>An enumerable sequence of the specified elements.</returns>
		public static IEnumerable<T> ToSequence<T>(T element, params T[] additional)
		{
			yield return element;

			foreach (T other in additional)
				yield return other;
		}

		/// <summary>
		/// Concatenates a specified separator String between each converted element of a specified collection, 
		/// yielding a single concatenated string. 
		/// </summary>
		public static string Join<T>(this IEnumerable<T> sequence, string separator)
		{
			return sequence.Join(separator, item => item.ToString());
		}

		/// <summary>
		/// Concatenates a specified separator String between each converted element of a specified collection, 
		/// yielding a single concatenated string. 
		/// </summary>
		public static string Join<T>(this IEnumerable<T> sequence, string separator, Func<T, string> conversion)
		{
			if (separator == null)
				throw new ArgumentNullException("separator");
			if (conversion == null)
				throw new ArgumentNullException("conversion");

			return String.Join(separator, sequence.Select(conversion).ToArray());
		}

		/// <summary>
		/// Converts a sequence to a readonly collection of T.
		/// </summary>
		public static ReadOnlyCollection<T> ToReadOnly<T>(this IEnumerable<T> sequence)
		{
			if (sequence == null)
				throw new ArgumentNullException("sequence");

			return Array.AsReadOnly(sequence.ToArray());
		}

		/// <summary>
		/// Concatenates the specified sequences.
		/// </summary>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		/// <param name="additionalItems">The additional items.</param>
		public static IEnumerable<T> Concat<T>(this IEnumerable<T> first, IEnumerable<T> second, params IEnumerable<T>[] additionalItems)
		{
			if (first == null)
				throw new ArgumentNullException("first");
			if (second == null)
				throw new ArgumentNullException("second");
			if (additionalItems == null)
				throw new ArgumentNullException("additionalItems");

			first = Enumerable.Concat(first, second);
			additionalItems.ForEach((item) => first = first.Concat((IEnumerable<T>) item));

			return first;
		}

		/// <summary>
		/// Returns a slice of the given source.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="startIndex">The start index.</param>
		/// <param name="size">The size.</param>
		public static IEnumerable<T> Slice<T>(this IEnumerable<T> source, int startIndex, int size)
		{
			if (source == null)
				throw new ArgumentNullException("source");

			int count = source.Count();
			if (startIndex < 0 || count < startIndex)
				throw new ArgumentOutOfRangeException("startIndex");

			if (size < 0 || startIndex + size > count)
				throw new ArgumentOutOfRangeException("size");

			return source.Skip(startIndex).Take(size);
		}

		/// <summary>
		/// Iterates the specified sequence returning arrays of each slice of <paramref name="size"/> elements.
		/// The last array may contain fewer that <paramref name="size"/> elements.
		/// </summary>
		/// <typeparam name="T">The sequence element type.</typeparam>
		/// <param name="sequence">The source sequence.</param>
		/// <param name="size">The desired slice size.</param>
		/// <returns>A sequence of arrays containing the elements from the specified sequence.</returns>
		public static IEnumerable<T[]> Slices<T>(this IEnumerable<T> sequence, int size)
		{
			// validate arguments
			if (sequence == null)
				throw new ArgumentNullException("sequence");
			if (size <= 0)
				throw new ArgumentOutOfRangeException("size");

			// return lazily evaluated iterator
			return SliceIterator(sequence, size);
		}

		// SliceIterator: iterator implementation of Slice
		private static IEnumerable<T[]> SliceIterator<T>(IEnumerable<T> sequence, int size)
		{
			// prepare the result array
			int position = 0;
			T[] resultArr = new T[size];

			foreach (T item in sequence)
			{
				// NOTE: performing the following test at the beginning of the loop ensures that we do not needlessly
				// create empty result arrays for sequences with even numbers of elements [(sequence.Count() % size) == 0]
				if (position == size)
				{
					// full result array; return to caller
					yield return resultArr;

					// create a new result array and reset position
					resultArr = new T[size];
					position = 0;
				}

				// store the current element in the result array
				resultArr[position++] = item;
			}

			// no elements in source sequence
			if (position == 0)
				yield break;

			// resize partial final slice
			if (position < size)
				Array.Resize(ref resultArr, position);

			// return final slice
			yield return resultArr;
		}

		private static IEnumerable<T> RepeatIterator<T>(Func<T> generator)
		{
			while (true)
				yield return generator();
		}
	}
}
