using KASHOP.DAL.DTO.Requests;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.BLL
{
    public class CategoryService : ICategoryService
    {
       private readonly IGenericRepository<Category> _categoryRepository;

        public CategoryService(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

         public async Task<bool> CreateAsync(CategoryRequest category)
        {
            var existingCategory = (await _categoryRepository.GetAsync()).Any(c => c.Name.ToLower() == category.Name.ToLower());
            if (existingCategory)
            {
                return false;
            }

            var categoryModel = category.Adapt<Category>();
            await _categoryRepository.CreateAsync(categoryModel);
            return true;
        }

         public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryResponse?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

      

         public async Task<bool> UpdateAsync(int id, CategoryRequest category)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoryResponse>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAsync();
            return categories.Adapt<List<CategoryResponse>>();

        }
    }
}
