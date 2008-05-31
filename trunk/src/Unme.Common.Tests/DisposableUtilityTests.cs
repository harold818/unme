
using System.IO;
using MbUnit.Framework;

namespace Unme.Common.Tests
{
	[TestFixture]
	public class DisposableUtilityTests
	{
		[Test]
		public void Dispose()
		{
			Stream stream = new MemoryStream();

			DisposableUtility.Dispose(ref stream);
			DisposableUtility.Dispose(ref stream);
			Assert.IsNull(stream);
		}		
	}
}
