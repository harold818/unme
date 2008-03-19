
using System;
using Unme.Common.NullReferenceExtension;
using Xunit;

namespace Unme.Common.Tests
{
	public class NullReferenceExtensionTests
	{
		[Fact]
		public void IfNotNullAction()
		{
			string foo = "bar";
			int i = 0;
			bool actionExecuted = false;
			foo.IfNotNull((Action<string>) (s => actionExecuted = true));

			Assert.True(actionExecuted);
		}

		[Fact]
		public void IfNotNullActionWithNullValue()
		{
			string foo = null;
			bool actionExecuted = false;
			foo.IfNotNull((Action<string>) (s => actionExecuted = true));

			Assert.False(actionExecuted);
		}

		[Fact]
		public void IfNotNullFunc()
		{
			string foo = "Foo";
			Assert.Equal("FooBar", foo.IfNotNull(s => String.Concat(s, "Bar")));
		}

		[Fact]
		public void IfNotNullFuncWithNullValue()
		{
			string foo = null;
			Assert.Null(foo.IfNotNull(s => String.Concat(s, "Bar")));
		}
	}
}
