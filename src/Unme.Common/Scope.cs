
using System;
using System.Threading;

namespace Unme.Common
{
	/// <summary>
	/// Enables invocation of an arbitrary <see cref="Action"/> when exiting a using block.
	/// </summary>
	public sealed class Scope : IDisposable
	{
		private Action _onDispose;

		private Scope() 
		{ 
		}

		/// <summary>
		/// Returns an IDisposable instance that will invoke the specified action when disposed.
		/// </summary>
		/// <param name="onDispose">The action to invoke.</param>
		/// <returns>An IDisposable instance.</returns>
		public static Scope Create(Action onDispose)
		{
			if (onDispose == null)
				throw new ArgumentNullException();

			return new Scope { _onDispose = onDispose };
		}

		/// <summary>
		/// Invokes the action if this is the first call to Dispose.
		/// Subsequent calls do nothing. This method is thread-safe.
		/// </summary>
		public void Dispose()
		{
			Action dispose;
			if ((dispose = Interlocked.Exchange(ref _onDispose, null)) != null)
				dispose();
		}
	}
}
