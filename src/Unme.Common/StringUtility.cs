
using System;

namespace Unme.Common
{
	/// <summary>
	/// Utility methods for strings.
	/// </summary>
	public static class StringUtility
	{
		/// <summary>
		/// Indicates whether the specified System.String object is null or an System.String.Empty string.
		/// </summary>
		/// <param name="str">A System.String reference.</param>
		/// <returns>
		/// true if the value parameter is null or an empty string (""); otherwise, false.
		/// </returns>
		public static bool IsNullOrEmpty(this string str)
		{
			return String.IsNullOrEmpty(str);
		}
	}
}
