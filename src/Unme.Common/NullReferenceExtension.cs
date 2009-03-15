
using System;

namespace Unme.Common.NullReferenceExtension
{
	/// <summary>
	/// Convenience methods for dealing with null references.
	/// </summary>
	public static class NullReferenceExtension
	{
		/// <summary>
		/// Performs the specified action if the element is not null.
		/// </summary>
		/// <typeparam name="T">Type of the element.</typeparam>
		/// <param name="element">The element.</param>
		/// <param name="action">The action to perform.</param>
		public static void IfNotNull<T>(this T element, Action<T> action)
		{
			if (element != null)
				action(element);
		}

		/// <summary>
		/// Performs the specified func and returns the result if the element is not null.
		/// Returns the default value of the func's return type if the element is null.
		/// </summary>
		/// <typeparam name="T">Type of the element.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="element">The element.</param>
		/// <param name="func">The func to perform.</param>
		/// <returns></returns>
		public static TResult IfNotNull<T, TResult>(this T element, Func<T, TResult> func)
		{
			return element != null ? func(element) : default(TResult);
		}

		/// <summary>
		/// Performs the specified func and returns the result if the element is not null.
		/// Returns the specified default value if the element is null.
		/// </summary>
		/// <typeparam name="T">Type of the element.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="element">The element.</param>
		/// <param name="func">The func to perform.</param>
		/// <param name="defaultValue">The value to return if the element is null.</param>
		/// <returns></returns>
		public static TResult IfNotNull<T, TResult>(this T element, Func<T, TResult> func, TResult defaultValue)
		{
			return element != null ? func(element) : defaultValue;
		}

		/// <summary>
		/// Perfom the specified action on the element if it is convertible to type TTarget.
		/// </summary>
		public static void Is<TTarget>(this object element, Action<TTarget> action) where TTarget : class
		{
			if (action == null)
				throw new ArgumentNullException("action");

			var convertedItem = element as TTarget;
			convertedItem.IfNotNull(item => action(item));
		}
	}
}
