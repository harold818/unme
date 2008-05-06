
using System;

namespace Unme.Common
{
	public static class DisposableUtility
	{
		/// <summary>
		/// Disposes the specified item and sets its value to null.
		/// </summary>
		public static void Dispose<T>(ref T item) where T : class, IDisposable
		{
			if (item != null)
			{
				item.Dispose();
				item = null;
			}
		}
	}
}
