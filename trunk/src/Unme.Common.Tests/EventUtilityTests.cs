
using System;
using MbUnit.Framework;

namespace Unme.Common.Tests
{
	[TestFixture]
	public class EventUtilityTests
	{
		[Test]
		public void Raise()
		{
			bool eventFired = false;
			var foo = new Foo();

			foo.MyEvent += (sender, args) =>
			{
				eventFired = true;
				Assert.AreEqual(foo, sender);
				Assert.AreEqual(EventArgs.Empty, args);
			};

			foo.FireEvent();

			Assert.IsTrue(eventFired);
		}

		[Test]
		public void Raise_Generic()
		{
			bool eventFired = false;		
			var foo = new Foo();

			foo.MyGenericEvent += (sender, args) =>
			{
				eventFired = true;
				Assert.AreEqual(foo, sender);
				Assert.AreEqual("Custom", ((MyEventArgs)args).CustomProperty);
			};

			foo.FireGenericEvent();

			Assert.IsTrue(eventFired);			
		}

		class Foo
		{
			public event EventHandler MyEvent;
			public event EventHandler<MyEventArgs> MyGenericEvent;

			public void FireEvent()
			{
				MyEvent.Raise(this);
			}

			public void FireGenericEvent()
			{
				MyGenericEvent.Raise(this, new MyEventArgs() { CustomProperty = "Custom" });
			}
		}

		class MyEventArgs : EventArgs
		{
			public string CustomProperty { get; set; }
		}
	}
}
