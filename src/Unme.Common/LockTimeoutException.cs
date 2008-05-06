
using System;
using System.Runtime.Serialization;

namespace Unme.Common
{
	/// <summary>
	/// Custom exception for lock timeout.
	/// </summary>
	[Serializable]
	public class LockTimeoutException : Exception
	{
		private const string DefaultExceptionMessage = "Timeout occurred waiting for lock.";

		/// <summary>
		/// Initializes a new instance of the <see cref="LockTimeoutException"/> class.
		/// </summary>
		public LockTimeoutException()
			: base(DefaultExceptionMessage)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LockTimeoutException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		public LockTimeoutException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LockTimeoutException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="innerException">The inner exception.</param>
		public LockTimeoutException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
