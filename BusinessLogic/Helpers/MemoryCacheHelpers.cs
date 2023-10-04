using System.Runtime.Caching;

namespace BusinessLogic.Helpers
{
	public class MemoryCacheHelpers
	{
		private static MemoryCache _memoryCache = new MemoryCache("MemoryCache");

		public static void AddToMemoryCache(string key, object value, DateTimeOffset expiredWhen)
		{

			if (_memoryCache != null)
			{
				// set new value to cache
				if (_memoryCache.Get(key) != null)
				{
					//delete existing
					_memoryCache.Remove(key);
				}

				_memoryCache.Add(
					new CacheItem(key, value),
					new CacheItemPolicy
					{
						AbsoluteExpiration = expiredWhen,
					});
			}
		}

		public static object GetFromMemoryCache(string key)
		{
			if (_memoryCache != null)
			{
				if (_memoryCache.Get(key) != null)
				{
					var rates = _memoryCache.Get(key);
					return rates;
				}
			}

			return null;
		}

		public static void ResetMemoryCache()
		{
			if (_memoryCache != null)
			{
				_memoryCache.Dispose();
			}

		}
	}
}
