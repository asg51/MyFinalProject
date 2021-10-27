using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        object Get(string key);
        void Add(string key, object value, int duration);// duration cahce bellekte duracagı zamanı ayarlıyoruz.
        bool IsAdd(string key); //Cahce bu veri varmı.
        void Remove(string key);
        void RemoveByPattern(string key); //fonkisyonun ismi içinde istenilen kelime olması durumu.
    }
}
