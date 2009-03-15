
using System;
using System.Threading;

namespace Unme.Common
{
	public sealed class LazyObject<T> : IDisposable
	{
		private object _lockGenereration;
		private volatile bool _initialized;
		private T _value;
		private Func<T> _generator;

		private LazyObject(Func<T> generator)
		{
			_lockGenereration = new object();
			_generator = generator;
		}		

		public T Value
		{
			get
			{
				if (!_initialized)
				{
					lock (_lockGenereration)
					{
						Func<T> generator = Interlocked.Exchange(ref _generator, null);
						if (generator != null)
						{
							_value = generator();
							_initialized = true;
						}
					}
					
					_lockGenereration = null;
				}

				return _value;
			}
		}

		public static LazyObject<T> Create(Func<T> generator)
		{
			if (generator == null)
				throw new ArgumentNullException("generator");

			return new LazyObject<T>(generator);
		}

		public void Dispose()
		{
			_initialized = true;
			_lockGenereration = null;
			_generator = null;

			IDisposable disposable = _value as IDisposable;
			if (disposable != null)
				disposable.Dispose();

			_value = default(T);
		}		
	}
}
