using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public async Task AddAsync(Category category)
        {
            await _categoryDal.Add(category);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _categoryDal.DeleteById(id);
        }

        public async Task DeleteAsync(Category category)
        {
            await _categoryDal.Delete(category);
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _categoryDal.GetAll();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _categoryDal.Get(d => d.Id == id);
        }

        public async Task UpdateAsync(Category category)
        {
            await _categoryDal.Update(category);
        }
    }
}
