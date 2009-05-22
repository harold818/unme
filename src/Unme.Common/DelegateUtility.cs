
using System;
using System.Linq;

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

			return InternalCombine(delegatesDest);
		}

		/// <summary>
		/// Delegate.Combine(params Delegate[] delegates) not supported by Compact Framework.
		/// </summary>
		/// <param name="delegates">The array of delegates to combine.</param>
		private static Delegate InternalCombine(params Delegate[] delegates)
		{
			if (delegates == null || delegates.Length == 0)
				return null;
			
			Delegate combined = delegates[0];

			delegates.Skip(1).ForEach(d => combined = Delegate.Combine(combined, d));
		
			return combined;
		}
	}
}
