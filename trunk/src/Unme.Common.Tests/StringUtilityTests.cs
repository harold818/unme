
using Xunit;
using XunitExt;
using System;

namespace Unme.Common.Tests
{
	public class StringUtilityTests
	{
		[Theory,
		InlineData(null, true),
		InlineData("", true),
		InlineData("foo", false)]
		public void IsNullOrEmpty(string value, bool expectedResult)
		{
			Assert.Equal(expectedResult, value.IsNullOrEmpty());
		}
	}
}
