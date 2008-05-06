
using System;
using NUnit.Framework;
using Unme.NUnit.Framework.Extensions;

namespace Unme.Common.Tests
{
	[TestFixture]
	public class ScopeTests
	{
		[Test]
		public void ScopeCreateValidatesArgument()
		{
			AssertUtility.Throws<ArgumentNullException>(() => Scope.Create(null));
		}

		[Test]
		public void ScopeCallsActionOnDispose()
		{
			bool called = false;
			Action onDispose = () => called = true;
			
			var scope = Scope.Create(onDispose);
			Assert.IsFalse(called);
			
			scope.Dispose();
			Assert.IsTrue(called);
		}

		[Test]
		public void ScopeCallsActionOnlyOnce()
		{
			int called = 0;
			Action onDispose = () =>
			{
				Assert.IsTrue(called < 1);
				called++;
			};
			
			var scope = Scope.Create(onDispose);
			Assert.AreEqual(0, called);

			scope.Dispose();
			Assert.AreEqual(1, called);

			scope.Dispose(); scope.Dispose(); scope.Dispose();
			Assert.AreEqual(1, called);
		}
	}
}
