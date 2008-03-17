
using System;
using System.Diagnostics;

namespace Unme.Common
{
	public static class StopwatchUtility
	{
		// TODO: does Scope's Interlocked.Exchange-utilizing Dispose implementation make this ScopedStart unreliable for timing?
		public static Scope ScopedStart(this Stopwatch stopwatch)
		{
			if (stopwatch == null)
				throw new ArgumentNullException("stopwatch");

			stopwatch.Reset();
			var result = Scope.Create(stopwatch.Stop);
			stopwatch.Start();
			return result;
		}
	}
}
