using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using System.Linq;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCahceManager : ICacheManager
    {
        IMemoryCache _memoryCahce;

        public MemoryCahceManager()
        {
            _memoryCahce = ServiceTool.ServiceProvider.GetService<IMemoryCache>(); //injection yapıyoruz.
        }
        public void Add(string key, object value, int duration)
        {
            _memoryCahce.Set(key, value, TimeSpan.FromMinutes(duration)); //verilen bilgiler key anahtarı ile birlikte verilen zaman kadar cahce de durur.
        }

        public T Get<T>(string key)
        {
            return _memoryCahce.Get<T>(key); //istenilen verileri anahtara göre getirir.
        }

        public object Get(string key)
        {
            return _memoryCahce.Get(key); //istenilen verileri anahtara göre getirir.
        }

        public bool IsAdd(string key)
        {
            return _memoryCahce.TryGetValue(key, out _); //out parametresi ile geriye değer döndermesini istemediğmiz zaman kullanılır.
        }

        public void Remove(string key)
        {
            _memoryCahce.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCahce) as dynamic;
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

            foreach (var key in keysToRemove)
            {
                _memoryCahce.Remove(key);
            }
        }
    }
}
