
using System;
using System.Runtime.CompilerServices;

namespace Unme.Common
{
	public static class EventUtility
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Raise(this EventHandler handler, object sender)
		{
			if (handler != null)
				handler(sender, EventArgs.Empty);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Raise<T>(this EventHandler<T> handler, object sender, T e) where T : EventArgs
		{
			if (handler != null)
				handler(sender, e);
		}
	}
}
