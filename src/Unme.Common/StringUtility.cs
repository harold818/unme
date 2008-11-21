
using System;
using System.Globalization;

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

        /// <summary>
        /// Wraps the specified string in double quotes.
        /// </summary>
        public static string DoubleQuote(this string str)
        {
            return String.Format(CultureInfo.InvariantCulture, @"""{0}""", str);
        }

        /// <summary>
        /// Wraps the specified string in single quotes.
        /// </summary>
        public static string SingleQuote(this string str)
        {
            return String.Format(CultureInfo.InvariantCulture, "'{0}'", str);
        }
	}
}
