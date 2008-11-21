
using System;
using System.Collections.Generic;
using System.Linq;
using MbUnit.Framework;
using Unme.MbUnit.Framework.Extensions;
using System.Collections.ObjectModel;

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
		public void Repeat_ValidatesArguments()
		{
			AssertUtility.Throws<ArgumentNullException>(() => SequenceUtility.Repeat<int>(null));
		}

		[RowTest,
		Row(1),
		Row(2),
		Row(3),
		Row(24)]
		public void Repeat_SequenceIsSpecifiedLength(int length)
		{
			var sequence = SequenceUtility.Repeat(() => new Item()).Take(length);
			Assert.AreEqual(length, sequence.Count());
		}

		[Test]
		public void ToSequence_SingleElement()
		{
			IEnumerable<int> sequence = 1.ToSequence();
			Assert.IsNotNull(sequence);
			Assert.AreEqual(1, sequence.Count());
			Assert.AreEqual(1, sequence.First());
		}

		[Test]
		public void ToSequence_Null()
		{
			IEnumerable<string> sequence = ((string) null).ToSequence();
			Assert.IsNotNull(sequence);
			Assert.AreEqual(1, sequence.Count());
			Assert.AreEqual(null, sequence.First());
		}

		[Test]
		public void ToSequence_SeveralElements()
		{
			IEnumerable<int> sequence = SequenceUtility.ToSequence(1, 2, 3, 4);
			Assert.IsNotNull(sequence);
			Assert.AreEqual(4, sequence.Count());
			Assert.AreEqual(new int[] { 1, 2, 3, 4 }, sequence.ToArray());
		}

		[Test]
		public void ToSequence_SeveralElementsWithNullValues()
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

		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void WithIndex_SourceNull()
		{
			string[] array = null;
			array.WithIndex().ToArray();
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

		[Test]
		public void Join_Array()
		{
			ushort[] registers = new ushort[] { 1, 2, 3 };
			Assert.AreEqual("1, 2, 3", registers.Join(", "));
		}

		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void Join_ArrayNull()
		{
			bool[] array = null;
			array.Join(", ");
			Assert.Fail();
		}

		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void Join_ArrayConverterNull()
		{
			new bool[] { true, false }.Join(", ", null);
			Assert.Fail();
		}

		[Test]
		public void Join_Collection()
		{
			var registers = new Collection<ushort>(new ushort[] { 1, 2, 3 });
			Assert.AreEqual("1, 2, 3", registers.Join(", "));
		}

		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void Join_CollectionNull()
		{
			ICollection<bool> col = null;
			col.Join(", ");
			Assert.Fail();
		}

		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void Join_CollectionConverterNull()
		{
			new Collection<ushort>(new ushort[] { 1 }).Join(", ", null);
		}

		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void Join_SeparatorNull()
		{
			new int[] { }.Join(null);
		}

		[Test]
		public void Join_SeparatorEmpty()
		{
			Assert.AreEqual("12", new int[] { 1, 2 }.Join(""));
		}

		[Test]
		public void Join_ArrayCustomConversion()
		{
			ushort[] registers = new ushort[] { 1, 2, 3 };
			Assert.AreEqual("number: 1, number: 2, number: 3", registers.Join(", ", delegate(ushort number) { return String.Format("number: {0}", number); }));
		}

		[Test]
		public void Join_CollectionCustomConversion()
		{
			Collection<ushort> registers = new Collection<ushort>(new ushort[] { 1, 2, 3 });
			Assert.AreEqual("number: 1, number: 2, number: 3", registers.Join(", ", delegate(ushort number) { return String.Format("number: {0}", number); }));
		}

		[Test]
		public void Concat()
		{
			Assert.AreEqual(new byte[] { 1, 2, 3, 4 }, new byte[] { 1, 2 }.Concat(new byte[] { 3, 4 }).ToArray());
		}

		[Test]
		public void Concat_ThreeArrays()
		{
			Assert.AreEqual(new byte[] { 1, 2, 3, 4, 5, 6 }, new byte[] { 1, 2 }.Concat(new byte[] { 3, 4 }, new byte[] { 5, 6 }).ToArray());
		}

		[Test]
		public void Concat_FourArrays()
		{
			Assert.AreEqual(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }, new byte[] { 1, 2 }.Concat(new byte[] { 3, 4 }, new byte[] { 5, 6 }, new byte[] { 7, 8 }).ToArray());
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Concat_NullParams()
		{
			new byte[] { 1, 2 }.Concat(new byte[] { 3, 4 }, null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Concat_NullArgument1()
		{
			new byte[] { }.Concat(null, new byte[] { 1, 2 });
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Concat_NullArgument2()
		{
			new byte[] { }.Concat(new byte[] { 1, 2 }, null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Concat_NullArgument3()
		{
			new byte[] { }.Concat(new byte[] { 1, 2 }, new byte[] { 3, 4 }, null);
		}

		[Test]
		public void Slice()
		{
			Assert.AreEqual(new int[] { 3, 4, 5 }, new int[] { 1, 2, 3, 4, 5 }.Slice(2, 3).ToArray());
		}

		[Test]
		public void Slice_StartIndexMax()
		{
			Assert.AreEqual(new int[] { 5 }, new int[] { 1, 2, 3, 4, 5 }.Slice(4, 1).ToArray());
		}

		[Test]
		public void Slice_EntireSource()
		{
			Assert.AreEqual(new int[] { 1, 2, 3, 4, 5 }, new int[] { 1, 2, 3, 4, 5 }.Slice(0, 5).ToArray());
		}

		[Test]
		public void Slice_Empty()
		{
			Assert.AreEqual(new int[] { }, new int[] { }.Slice(0, 0).ToArray());
		}

		[Test]
		public void Slice_One()
		{
			Assert.AreEqual(new int[] { 3 }, new int[] { 3 }.Slice(0, 1).ToArray());
		}

		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Slice_SizeTooLarge()
		{
			int[] result = new int[] { 1, 2, 3, 4, 5 }.Slice(2, 4).ToArray();
		}

		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Slice_SizeNegative()
		{
			new int[] { 1, 2, 3, 4, 5 }.Slice(2, -1);
		}

		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Slice_StartIndexNegative()
		{
			new int[] { 1, 2, 3, 4, 5 }.Slice(-1, 1);
		}

		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Slice_StartIndexTooLarge()
		{
			new int[] { 1, 2, 3, 4, 5 }.Slice(5, 1);
		}

        [Test, ExpectedArgumentException]
        public void ForEach_DifferingLengths()
        {
            IEnumerable<int> first = new int[] { 0 };
            IEnumerable<int> second = new int[] { 0, 1 };
            first.ForEach(second, (left, right) => { });
        }

        [Test]
        public void ForEach_VerifyAllElementIterated()
        {
            int sum = 0;
            IEnumerable<int> first = new int[] { 1, 1 };
            IEnumerable<int> second = new int[] { 2, 2 };
            first.ForEach(second, (left, right) => { sum += left + right; });

            Assert.AreEqual(sum, 6);
        }

		class Item
		{
			public bool Visited { get; set; }
		}
	}
}
