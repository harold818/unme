
using MbUnit.Framework;

namespace Unme.Common.Tests
{
	[TestFixture]
	public class FunctionalUtilityTests
	{
		[Test]
		public void Memoize()
		{
			var getter = FunctionalUtility.Memoize(() => new Foo() { Id = 1, Value = "Test" });

			Assert.AreEqual(getter(), getter());		
		}

		[Test]
		public void Memoize_SingleArgument()
		{
			var getter = FunctionalUtility.Memoize((Foo foo) => new FooBar() { Value = foo.Value });
			
			var firstFoo = new Foo() { Id = 1, Value = "FirstFoo" };
			var secondFoo = new Foo() { Id = 2, Value = "SecondFoo" };
			var duplicateFoo = new Foo() { Id = 2, Value = "DuplicateFoo" };

			Assert.AreEqual(getter(firstFoo), getter(firstFoo));	
			Assert.AreEqual("FirstFoo", getter(firstFoo).Value);
			Assert.AreEqual("SecondFoo", getter(secondFoo).Value);
			Assert.AreEqual("DuplicateFoo", getter(duplicateFoo).Value);
		}

		[Test]
		public void Memoize_SingleArgumentWithSelector()
		{
			var getter = FunctionalUtility.Memoize((Foo foo) => new FooBar() { Value = foo.Value }, foo => foo.Id);

			var firstFoo = new Foo() { Id = 1, Value = "FirstFoo" };
			var secondFoo = new Foo() { Id = 2, Value = "SecondFoo" };
			var duplicateFoo = new Foo() { Id = 2, Value = "DuplicateFoo" };

			Assert.AreEqual(getter(firstFoo), getter(firstFoo));
			Assert.AreEqual("FirstFoo", getter(firstFoo).Value);
			Assert.AreEqual("SecondFoo", getter(secondFoo).Value);
			Assert.AreEqual("SecondFoo", getter(duplicateFoo).Value);
		}

		[Test]
		public void Memoize_TwoArguments()
		{
			var getter = FunctionalUtility.Memoize((Foo foo, Bar bar) => new FooBar() { Value = foo.Value + bar.Value });

			var firstFoo = new Foo() { Id = 1, Value = "FirstFoo" };
			var secondFoo = new Foo() { Id = 2, Value = "SecondFoo" };
			var duplicateFoo = new Foo() { Id = 2, Value = "DuplicateFoo" };
			var onlyBar = new Bar() { Value = "Bar" };

			Assert.AreEqual(getter(firstFoo, onlyBar), getter(firstFoo, onlyBar));
			Assert.AreEqual("FirstFooBar", getter(firstFoo, onlyBar).Value);
			Assert.AreEqual("SecondFooBar", getter(secondFoo, onlyBar).Value);
			Assert.AreEqual("DuplicateFooBar", getter(duplicateFoo, onlyBar).Value);
		}

		[Test]
		public void Memoize_TwoArgumentsWithKeySelector()
		{
			var getter = FunctionalUtility.Memoize((Foo foo, Bar bar) => new FooBar() { Value = foo.Value + bar.Value },
				foo => foo.Id);

			var firstFoo = new Foo() { Id = 1, Value = "FirstFoo" };
			var secondFoo = new Foo() { Id = 2, Value = "SecondFoo" };
			var duplicateFoo = new Foo() { Id = 2, Value = "DuplicateFoo" };
			var onlyBar = new Bar() { Value = "Bar" };

			Assert.AreEqual(getter(firstFoo, onlyBar), getter(firstFoo, onlyBar));
			Assert.AreEqual("FirstFooBar", getter(firstFoo, onlyBar).Value);
			Assert.AreEqual("SecondFooBar", getter(secondFoo, onlyBar).Value);
			Assert.AreEqual("SecondFooBar", getter(duplicateFoo, onlyBar).Value);
		}

		class Foo
		{			
			public int Id { get; set; }
			public string Value { get; set; }
		}

		class Bar
		{
			public string Value { get; set; }
		}

		class FooBar
		{
			public string Value { get; set; }
		}
	}
}
