using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestSwagger.Models
{
    public class MyMemoryCache
    {
        static private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

        public RegistrationRequest GetOrCreate(string key, RegistrationRequest item)
        {
            RegistrationRequest cacheEntry;
            if (!_cache.TryGetValue(key, out cacheEntry)) // Ищем ключ в кэше.
            {
                // Ключ отсутствует в кэше, поэтому получаем данные.
                cacheEntry = item;

                // Сохраняем данные в кэше. 
                _cache.Set(key, cacheEntry);
            }
            return cacheEntry;
        }

        public RegistrationRequest Get(string key, RegistrationRequest item)
        {
            RegistrationRequest cacheEntry;
            if (!_cache.TryGetValue(key, out cacheEntry)) // Ищем ключ в кэше.
            {
                //если не нашло получаем пустой объект
                cacheEntry = item;
            }
            return cacheEntry;
        }


    }
}

