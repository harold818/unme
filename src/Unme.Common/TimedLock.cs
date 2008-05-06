
using System;
using System.Threading;

namespace Unme.Common
{
	/// <summary>
	/// A lock that will block only for the specified TimeSpan value.
	/// </summary>
	public class TimedLock : IDisposable
	{
		private object _target;

		public static TimedLock Lock(object o)
		{
			return Lock(o, TimeSpan.FromSeconds(10));
		}

		public static TimedLock Lock(object o, TimeSpan timeout)
		{
			TimedLock tl = new TimedLock(o);

			if (!Monitor.TryEnter(o, timeout))
				throw new LockTimeoutException();

			return tl;
		}

		public void Dispose()
		{
			Monitor.Exit(_target);
		}

		private TimedLock(object o)
		{
			_target = o;
		}
	}	
}
