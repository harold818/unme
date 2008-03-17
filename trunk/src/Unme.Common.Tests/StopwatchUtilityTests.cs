
using System;
using System.Diagnostics;
using Xunit;

namespace Unme.Common.Tests
{
	public class StopwatchUtilityTests
	{
		[Fact]
		public void ScopedStartValidatesArgument()
		{
			Assert.Throws<ArgumentNullException>(() => StopwatchUtility.ScopedStart(null));
		}

		[Fact]
		public void ScopedStartStartsAndStopsStopwatch()
		{
			Stopwatch stopwatch = new Stopwatch();
			Assert.False(stopwatch.IsRunning);

			using (stopwatch.ScopedStart())
				Assert.True(stopwatch.IsRunning);
			
			long n = stopwatch.ElapsedTicks;
			Assert.False(stopwatch.IsRunning);
		}
	}
}
