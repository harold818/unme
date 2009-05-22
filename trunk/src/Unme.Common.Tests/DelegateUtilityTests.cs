using MbUnit.Framework;
using System;

namespace Unme.Common.Tests
{
	[TestFixture]
	public class DelegateUtilityTests
	{
		[Test]
		public void Cast()
		{
			Predicate<int> isPositive = n => n > 0;
			Func<int, bool> isPositive2 = isPositive.Cast(typeof(Func<int, bool>)) as Func<int, bool>;

			Assert.IsNotNull(isPositive2);
			Assert.AreEqual(isPositive(3), isPositive2(3));
			Assert.AreEqual(isPositive(-3), isPositive2(-3));
		}

		[Test]
		public void Cast_Generic()
		{
			Predicate<int> isPositive = n => n > 0;
			Func<int, bool> isPositive2 = isPositive.Cast<Func<int, bool>>();

			Assert.AreEqual(isPositive(3), isPositive2(3));
			Assert.AreEqual(isPositive(-3), isPositive2(-3));
		}
	}
}
