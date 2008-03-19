
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using XunitExt;

namespace Unme.Common.Tests
{
	public class SequenceUtilityTests
	{
		[Fact]
		public void ForEachValidatesArguments()
		{
			Assert.Throws<ArgumentNullException>(() => ((IEnumerable<int>) null).ForEach(n => Console.WriteLine(n)));
			Assert.Throws<ArgumentNullException>(() => (new[] { 1, 2, 3 }).ForEach(null));
		}

		[Fact]
		public void ForEachEnumeratesEntireCollection()
		{
			IEnumerable<Item> sequence = SequenceUtility.Repeat(() => new Item(), 3).ToList();
			Assert.True(sequence.All(item => !item.Visited));
			
			sequence.ForEach(item => item.Visited = true);
			Assert.True(sequence.All(item => item.Visited));
		}

		[Fact]
		public void RepeatValidatesArguments()
		{
			Assert.Throws<ArgumentNullException>(() => SequenceUtility.Repeat<int>(null, 1));
			Assert.Throws<ArgumentOutOfRangeException>(() => SequenceUtility.Repeat(() => new Item(), 0));
		}

		[Theory,
		InlineData(1),
		InlineData(2),
		InlineData(3),
		InlineData(24)]
		public void RepeatSequenceIsSpecifiedLength(int length)
		{
			var sequence = SequenceUtility.Repeat(() => new Item(), length);
			Assert.Equal(length, sequence.Count());
		}

		[Fact]
		public void ToSequenceSingleElement()
		{
			IEnumerable<int> sequence = 1.ToSequence();
			Assert.NotNull(sequence);
			Assert.Equal(1, sequence.Count());
			Assert.Equal(1, sequence.First());
		}

		[Fact]
		public void ToSequenceNull()
		{			
			IEnumerable<string> sequence = default(string).ToSequence();
			Assert.NotNull(sequence);
			Assert.Equal(1, sequence.Count());
			Assert.Equal(null, sequence.First());
		}

		[Fact]
		public void ToSequenceSeveralElements()
		{
			IEnumerable<int> sequence = 1.ToSequence(2, 3, 4);
			Assert.NotNull(sequence);
			Assert.Equal(4, sequence.Count());
			Assert.Equal(new int[] { 1, 2, 3, 4 }, sequence.ToArray());
		}

		[Fact]
		public void ToSequenceSeveralElementsWithNullValues()
		{
			IEnumerable<string> sequence = "one".ToSequence(null, "two", null, "three");
			Assert.NotNull(sequence);
			Assert.Equal(5, sequence.Count());
			Assert.Equal(new string[] { "one", null, "two", null, "three" }, sequence.ToArray());
		}

		class Item
		{
			public bool Visited { get; set; }
		}
	}
}
