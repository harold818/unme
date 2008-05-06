
using System;
using Unme.Common.NullReferenceExtension;
using NUnit.Framework;

namespace Unme.Common.Tests
{
	[TestFixture]
	public class NullReferenceExtensionTests
	{
		[Test]
		public void IfNotNullAction()
		{
			string foo = "bar";
			bool actionExecuted = false;
			foo.IfNotNull((Action<string>) (s => actionExecuted = true));

			Assert.IsTrue(actionExecuted);
		}

		[Test]
		public void IfNotNullActionWithNullValue()
		{
			string foo = null;
			bool actionExecuted = false;
			foo.IfNotNull((Action<string>) (s => actionExecuted = true));

			Assert.IsFalse(actionExecuted);
		}

		[Test]
		public void IfNotNullFunc()
		{
			string foo = "Foo";
			Assert.AreEqual("FooBar", foo.IfNotNull(s => String.Concat(s, "Bar")));
		}

		[Test]
		public void IfNotNullFuncWithNullValue()
		{
			string foo = null;
			Assert.IsNull(foo.IfNotNull(s => String.Concat(s, "Bar")));
		}
	}
}
