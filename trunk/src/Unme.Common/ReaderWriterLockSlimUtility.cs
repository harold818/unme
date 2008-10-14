
using System;
using System.Threading;

namespace Unme.Common
{
	/// <summary>
	/// Convenience methods for the ReaderWriterLockSlim type.
	/// </summary>
	public static class ReaderWriterLockSlimUtility
	{
		/// <summary>
		/// Convenience method providing a Scoped write lock.
		/// </summary>
		public static Scope ScopedEnterWriteLock(this ReaderWriterLockSlim readerWriterLock)
		{
			if (readerWriterLock == null)
				throw new ArgumentNullException("readerWriterLock");

			readerWriterLock.EnterWriteLock();
			return Scope.Create(readerWriterLock.ExitWriteLock);
		}

		/// <summary>
		/// Convenience method providing a Scoped read lock.
		/// </summary>
		public static Scope ScopedEnterReadLock(this ReaderWriterLockSlim readerWriterLock)
		{
			if (readerWriterLock == null)
				throw new ArgumentNullException("readerWriterLock");

			readerWriterLock.EnterReadLock();
			return Scope.Create(readerWriterLock.ExitReadLock);
		}

		/// <summary>
		/// Convenience method providing a Scoped upgradeable read lock.
		/// </summary>
		public static Scope ScopedEnterUpgradeableReadLock(this ReaderWriterLockSlim readerWriterLock)
		{
			if (readerWriterLock == null)
				throw new ArgumentNullException("readerWriterLock");

			readerWriterLock.EnterUpgradeableReadLock();
			return Scope.Create(readerWriterLock.ExitUpgradeableReadLock);
		}
	}
}
