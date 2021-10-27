﻿using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değildir!");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType); //doğrulama validation oluştur
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; //doğrulanacak olan sınıfın tipini bul
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); //attiribute kullanan sınıfdaki tüm içeriğini tara ve kullanıcak sınıf nesnelerini bul
            foreach (var entity in entities) // bulunan nesneleri kontrol et
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
