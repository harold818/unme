
using System;
using MbUnit.Framework;

namespace Unme.MbUnit.Framework.Extensions
{
	public static class AssertUtility
	{
		public static void Throws<T>(Action action)
			where T : Exception
		{
			try
			{
				action();
				Assert.Fail("Expected exception, but got none.");
			}
			catch (T)
			{
				// do nothing; desired result
			}
			catch (Exception ex)
			{
				Assert.AreEqual(typeof(T), ex.GetType());
			}
		}
	}
}
