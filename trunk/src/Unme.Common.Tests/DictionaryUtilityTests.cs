
using System;
using System.Collections.Generic;
using NUnit.Framework;
using Unme.NUnit.Framework.Extensions;

namespace Unme.Common.Tests
{
	[TestFixture]
	public class DictionaryUtilityTests
	{
		[Test]
		public void GetValueOrDefaultValidatesArguments()
		{
			AssertUtility.Throws<ArgumentNullException>(() => DictionaryUtility.GetValueOrDefault(default(IDictionary<int, string>), 1));
			AssertUtility.Throws<ArgumentNullException>(() => DictionaryUtility.GetValueOrDefault(default(IDictionary<string, int>), "one", -1));
		}

		[Test]
		public void GetValueOrDefaultReturnsValueFromDictionary()
		{
			var dictionary = new Dictionary<string, int> { { "one", 1 }, { "two", 2 }, { "three", 3 } };
			foreach (var pair in dictionary)
			{
				Assert.AreEqual(pair.Value, dictionary.GetValueOrDefault(pair.Key));
				Assert.AreEqual(pair.Value, dictionary.GetValueOrDefault(pair.Key, -1));
			}
		}

		[Test]
		public void GetValueOrDefaultReturnsExpectedDefault()
		{
			var colors = new Dictionary<ConsoleColor, string> { { ConsoleColor.Red, "Red" }, { ConsoleColor.Green, "Green" }, { ConsoleColor.Blue, "Blue" } };
			Assert.AreEqual(null, colors.GetValueOrDefault(ConsoleColor.DarkMagenta));
			Assert.AreEqual("not found!", colors.GetValueOrDefault(ConsoleColor.Magenta) ?? "not found!");
			
			var numbers = new Dictionary<string, int> { { "one", 1 }, { "two", 2 }, { "three", 3 } };
			Assert.AreEqual(0, numbers.GetValueOrDefault("four"));
			Assert.AreEqual(-1, numbers.GetValueOrDefault("five", -1));
		}
	}
}
