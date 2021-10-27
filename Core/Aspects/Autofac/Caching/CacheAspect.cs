using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.CrossCuttingConcerns.Caching;
using System.Linq;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}"); //isim uzayı ve method ismini al// MyFinalProject.Bussines.abstract.IproductServie.GetAll
            var arguments = invocation.Arguments.ToList();// methodun içerinde aldığı paratere değerlerini bul
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})"; // isim uzayı ile gelen değleri xyz.xyz(x,y,z) formatında key oluştur.
            if (_cacheManager.IsAdd(key))// Cahce veri bulunuyorsa 
            {
                invocation.ReturnValue = _cacheManager.Get(key); // fonksyonda işlemi yapma veriyi geri dönder.
                return;
            }
            invocation.Proceed();// fonkisyonu çalıştır database veriyi al
            _cacheManager.Add(key, invocation.ReturnValue, _duration); //verileri cahce al.
        }
    }
}
