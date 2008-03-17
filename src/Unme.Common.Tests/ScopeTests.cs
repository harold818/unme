
using System;
using Xunit;

namespace Unme.Common.Tests
{
	public class ScopeTests
	{
		[Fact]
		public void ScopeCreateValidatesArgument()
		{
			Assert.Throws<ArgumentNullException>(() => Scope.Create(null));
		}

		[Fact]
		public void ScopeCallsActionOnDispose()
		{
			bool called = false;
			Action onDispose = () => called = true;
			
			var scope = Scope.Create(onDispose);
			Assert.False(called);
			
			scope.Dispose();
			Assert.True(called);
		}

		[Fact]
		public void ScopeCallsActionOnlyOnce()
		{
			int called = 0;
			Action onDispose = () =>
			{
				Assert.True(called < 1);
				called++;
			};
			
			var scope = Scope.Create(onDispose);
			Assert.Equal(0, called);

			scope.Dispose();
			Assert.Equal(1, called);

			scope.Dispose(); scope.Dispose(); scope.Dispose();
			Assert.Equal(1, called);
		}
	}
}
