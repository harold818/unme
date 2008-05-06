
using Unme.Common;
using NUnit.Framework;
using NUnit.Framework.Extensions;

namespace Unme.Common.Tests
{
	[TestFixture]
	public class TupleTests
	{
		[Test]
		public void CreateTuple()
		{
			var tuple = Tuple.Create(1, "foo");

			Assert.AreEqual(tuple.First, 1);
			Assert.AreEqual(tuple.Second, "foo");
		}

		[Test]
		public void TupleEquals()
		{
			var tuple1 = Tuple.Create("foo", "bar");
			var tuple2 = Tuple.Create("foo", "bar");

			Assert.AreEqual(tuple1, tuple2);
		}

		[Test]
		public void TupleHashCode()
		{
			var tuple1 = Tuple.Create("foo", "bar");
			var tuple2 = Tuple.Create("foo", "bar");

			Assert.AreEqual(tuple1.GetHashCode(), tuple2.GetHashCode());
		}
	}
}
