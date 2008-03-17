
using System;
using System.Collections.Generic;
using Xunit;

namespace Unme.Common.Tests
{
	public class DictionaryUtilityTests
	{
		[Fact]
		public void GetValueOrDefaultValidatesArguments()
		{
			Assert.Throws<ArgumentNullException>(() => DictionaryUtility.GetValueOrDefault(default(IDictionary<int, string>), 1));
			Assert.Throws<ArgumentNullException>(() => DictionaryUtility.GetValueOrDefault(default(IDictionary<string, int>), "one", -1));
		}

		[Fact]
		public void GetValueOrDefaultReturnsValueFromDictionary()
		{
			var dictionary = new Dictionary<string, int> { { "one", 1 }, { "two", 2 }, { "three", 3 } };
			foreach (var pair in dictionary)
			{
				Assert.Equal(pair.Value, dictionary.GetValueOrDefault(pair.Key));
				Assert.Equal(pair.Value, dictionary.GetValueOrDefault(pair.Key, -1));
			}
		}

		[Fact]
		public void GetValueOrDefaultReturnsExpectedDefault()
		{
			var colors = new Dictionary<ConsoleColor, string> { { ConsoleColor.Red, "Red" }, { ConsoleColor.Green, "Green" }, { ConsoleColor.Blue, "Blue" } };
			Assert.Equal(null, colors.GetValueOrDefault(ConsoleColor.DarkMagenta));
			Assert.Equal("not found!", colors.GetValueOrDefault(ConsoleColor.Magenta) ?? "not found!");
			
			var numbers = new Dictionary<string, int> { { "one", 1 }, { "two", 2 }, { "three", 3 } };
			Assert.Equal(0, numbers.GetValueOrDefault("four"));
			Assert.Equal(-1, numbers.GetValueOrDefault("five", -1));
		}
	}
}
