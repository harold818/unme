
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

		class Item
		{
			public bool Visited { get; set; }
		}
	}
}
