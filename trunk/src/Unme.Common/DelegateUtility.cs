
using System;

namespace Unme.Common
{
	/// <summary>
	/// Delegate utlility methods.
	/// </summary>
	public static class DelegateUtility
	{
		/// <summary>
		/// Casts the delegate to the specified type.
		/// </summary>
		public static T Cast<T>(this Delegate source) where T : class
		{
			return Cast(source, typeof(T)) as T;
		}

		/// <summary>
		/// Casts the delegate to the specified type.
		/// </summary>
		public static Delegate Cast(this Delegate source, Type type)
		{
			if (source == null)
				return null;

			Delegate[] delegates = source.GetInvocationList();

			if (delegates.Length == 1)
				return Delegate.CreateDelegate(type, delegates[0].Target, delegates[0].Method);

			Delegate[] delegatesDest = new Delegate[delegates.Length];
			for (int nDelegate = 0; nDelegate < delegates.Length; nDelegate++)
				delegatesDest[nDelegate] = Delegate.CreateDelegate(type, delegates[nDelegate].Target, delegates[nDelegate].Method);

			return Delegate.Combine(delegatesDest);
		}
	}
}
