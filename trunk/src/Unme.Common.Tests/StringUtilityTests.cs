
using System;
using MbUnit.Framework;

namespace Unme.Common.Tests
{
	[TestFixture]
	public class StringUtilityTests
	{
		[RowTest,
		Row(null, true),
		Row("", true),
		Row("foo", false)]
		public void IsNullOrEmpty(string value, bool expectedResult)
		{
			Assert.AreEqual(expectedResult, value.IsNullOrEmpty());
		}
	}
}
