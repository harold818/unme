
using System;
using Xunit;

namespace Unme.Common.Tests
{
	public class ReflectTests
	{
		[Fact]
		public void GetPropertyNameValidatesArgument()
		{
			// method access expression
			Assert.Throws<ArgumentException>(() => Reflect<string>.GetPropertyName(s => s.ToString()));

			// field access expression
			Assert.Throws<ArgumentException>(() => Reflect<int>.GetPropertyName(s => String.Empty));
		}

		[Fact]
		public void GetPropertyName()
		{
			Assert.Equal("Length", Reflect<string>.GetPropertyName(s => s.Length));
		}
	}
}
