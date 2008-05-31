
using System;
using System.Diagnostics;
using MbUnit.Framework;
using Unme.MbUnit.Framework.Extensions;

namespace Unme.Common.Tests
{
	[TestFixture]
	public class StopwatchUtilityTests
	{
		[Test]
		public void ScopedStartValidatesArgument()
		{
			AssertUtility.Throws<ArgumentNullException>(() => StopwatchUtility.ScopedStart(null));
		}

		[Test]
		public void ScopedStartStartsAndStopsStopwatch()
		{
			Stopwatch stopwatch = new Stopwatch();
			Assert.IsFalse(stopwatch.IsRunning);

			using (stopwatch.ScopedStart())
				Assert.IsTrue(stopwatch.IsRunning);
			
			long n = stopwatch.ElapsedTicks;
			Assert.IsFalse(stopwatch.IsRunning);
		}
	}
}
