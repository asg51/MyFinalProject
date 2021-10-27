using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        [SecuredOperation("admin,category.add")]
        [ValidationAspect(typeof(CategoryValidator))]
        [CacheRemoveAspect("ICategoryService.GetAll")]
        public IResult Add(Category category)
        {
            _categoryDal.Add(category);

            return new SuccessResult(Messages.CategoryAdded);
        }

        [SecuredOperation("admin,category.delete")]
        [ValidationAspect(typeof(CategoryValidator))]
        [CacheRemoveAspect("ICategoryService.GetAll")]
        public IResult Delete(Category category)
        {
            _categoryDal.Delete(category);

            return new SuccessResult(Messages.CategoryDeleted);
        }

        [CacheAspect()]
        public IDataResult<List<Category>> GetAll()
        {
            var result = _categoryDal.GetAll();

            return new SuccessDataResult<List<Category>>(result);
        }

        [CacheAspect()]
        public IDataResult<Category> GetById(int id)
        {
            var result = _categoryDal.Get(x=>x.CategoryId==id);

            return new SuccessDataResult<Category>(result);
        }

        [SecuredOperation("admin,category.updated")]
        [ValidationAspect(typeof(CategoryValidator))]
        [CacheRemoveAspect("ICategoryService.GetAll")]
        public IResult Update(Category category)
        {
            _categoryDal.Update(category);

            return new SuccessResult(Messages.CategoryUpdated);
        }
    }
}
