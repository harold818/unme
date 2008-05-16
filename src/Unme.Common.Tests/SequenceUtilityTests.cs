
using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Extensions;
using Unme.NUnit.Framework.Extensions;

namespace Unme.Common.Tests
{
	[TestFixture]
	public class SequenceUtilityTests
	{
		[Test]
		public void ForEachValidatesArguments()
		{
			AssertUtility.Throws<ArgumentNullException>(() => ((IEnumerable<int>) null).ForEach(n => Console.WriteLine(n)));
			AssertUtility.Throws<ArgumentNullException>(() => (new[] { 1, 2, 3 }).ForEach(null));
		}

		[Test]
		public void ForEachEnumeratesEntireCollection()
		{
			IEnumerable<Item> sequence = SequenceUtility.Repeat(() => new Item()).Take(3).ToList();
			Assert.IsTrue(sequence.All(item => !item.Visited));
			
			sequence.ForEach(item => item.Visited = true);
			Assert.IsTrue(sequence.All(item => item.Visited));
		}

		[Test]
		public void RepeatValidatesArguments()
		{
			AssertUtility.Throws<ArgumentNullException>(() => SequenceUtility.Repeat<int>(null));
		}

		[RowTest,
		Row(1),
		Row(2),
		Row(3),
		Row(24)]
		public void RepeatSequenceIsSpecifiedLength(int length)
		{
			var sequence = SequenceUtility.Repeat(() => new Item()).Take(length);
			Assert.AreEqual(length, sequence.Count());
		}

		[Test]
		public void ToSequenceSingleElement()
		{
			IEnumerable<int> sequence = 1.ToSequence();
			Assert.IsNotNull(sequence);
			Assert.AreEqual(1, sequence.Count());
			Assert.AreEqual(1, sequence.First());
		}

		[Test]
		public void ToSequenceNull()
		{
			IEnumerable<string> sequence = ((string) null).ToSequence();
			Assert.IsNotNull(sequence);
			Assert.AreEqual(1, sequence.Count());
			Assert.AreEqual(null, sequence.First());
		}

		[Test]
		public void ToSequenceSeveralElements()
		{
			IEnumerable<int> sequence = SequenceUtility.ToSequence(1, 2, 3, 4);
			Assert.IsNotNull(sequence);
			Assert.AreEqual(4, sequence.Count());
			Assert.AreEqual(new int[] { 1, 2, 3, 4 }, sequence.ToArray());
		}

		[Test]
		public void ToSequenceSeveralElementsWithNullValues()
		{
			IEnumerable<string> sequence = SequenceUtility.ToSequence("one", null, "two", null, "three");
			Assert.IsNotNull(sequence);
			Assert.AreEqual(5, sequence.Count());
			Assert.AreEqual(new string[] { "one", null, "two", null, "three" }, sequence.ToArray());
		}

		[Test]
		public void ForEachWithIndex()
		{
			var array = new string[] { "one", "two", "three" };
			int expectedIndex = 0;
			array.ForEachWithIndex((value, index) => Assert.AreEqual(expectedIndex++, index));
		}

		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void ForEachWithIndex_SourceNull()
		{
			string[] array = null;
			array.ForEachWithIndex((value, index) => Assert.Fail());
		}

		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void ForEachWithIndex_ActionNull()
		{
			new string[] { }.ForEachWithIndex(null);
		}

		[Test]
		public void WithIndex()
		{
			var array = new string[] { "one", "two", "three" };
			int expectedIndex = 0;

			foreach (var pair in array.WithIndex())
			{
				Assert.AreEqual(pair.Second, array[expectedIndex]);
				Assert.AreEqual(pair.First, expectedIndex++);
			}
		}

		class Item
		{
			public bool Visited { get; set; }
		}
	}
}
