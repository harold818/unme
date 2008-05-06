
using System;
using NUnit.Framework;
using System.Threading;
using Unme.NUnit.Framework.Extensions;

namespace Unme.Common.Tests
{
	[TestFixture]
	public class TimedLockTests
	{
		[Test]
		public void TimedLockTimeou()
		{
			var manualResetEvent = new ManualResetEvent(false);
			var syncObject = new object();

			var newThread = new Thread(() =>
			{
				using (TimedLock.Lock(syncObject))
				{
					manualResetEvent.Set();
					// deadlock
					Thread.Sleep(Timeout.Infinite);
				}
			});

			newThread.Start();
			manualResetEvent.WaitOne();

			AssertUtility.Throws<LockTimeoutException>(() =>
			{
				using (TimedLock.Lock(syncObject, TimeSpan.FromMilliseconds(1))) { }
			});
		}
	}
}
