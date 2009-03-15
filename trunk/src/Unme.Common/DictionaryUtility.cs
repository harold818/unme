
using System;
using System.Collections.Generic;

namespace Unme.Common
{
	/// <summary>
	/// IDictinary&lt;TKey, TValue&gt; utility methods.
	/// </summary>
	/// <remarks>
	/// Implementations of IDictionary&lt;TKey, TValue&gt; can vary in whether they allow key 
	/// to be a null reference. See: http://msdn2.microsoft.com/en-us/library/zyxt2e2h.aspx
	/// </remarks>
	public static class DictionaryUtility
	{
		public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
		{
			if (dictionary == null)
				throw new ArgumentNullException("dictionary");

			TValue value;
			dictionary.TryGetValue(key, out value);
			return value;
		}

		// null coalescing (??) should be used to provide a non-standard default value for reference types
		public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
			where TValue : struct
		{
			if (dictionary == null)
				throw new ArgumentNullException("dictionary");

			TValue value;
			return dictionary.TryGetValue(key, out value) ? value : defaultValue;				
		}
	}
}
