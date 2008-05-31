
using System;
using MbUnit.Framework;
using Unme.MbUnit.Framework.Extensions;

namespace Unme.Common.Tests
{
	[TestFixture]
	public class ReflectTests
	{
		[Test]
		public void GetPropertyNameValidatesArgument()
		{
			// method access expression
			AssertUtility.Throws<ArgumentException>(() => Reflect<string>.GetPropertyName(s => s.ToString()));

			// field access expression
			AssertUtility.Throws<ArgumentException>(() => Reflect<int>.GetPropertyName(s => String.Empty));
		}

		[Test]
		public void GetPropertyName()
		{
			Assert.AreEqual("Length", Reflect<string>.GetPropertyName(s => s.Length));
		}
	}
}
