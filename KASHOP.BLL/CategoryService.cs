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

         public async Task<bool> CreateAsync(CategoryRequest category, CancellationToken cancellationToken = default)
        {
            var existingCategory = await _categoryRepository.GetOneAsync(filter:,cancellationToken: cancellationToken);
            if (existingCategory != null)
            {
                return false;
            }

            var categoryModel = category.Adapt<Category>();
            await _categoryRepository.CreateAsync(categoryModel, cancellationToken);
            return true;
        }

         public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryResponse?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

      

         public async Task<bool> UpdateAsync(int id, CategoryRequest category, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoryResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var categories = await _categoryRepository.GetAsync(x => x.Id == 4, cancellationToken: cancellationToken);
            return categories.Adapt<List<CategoryResponse>>(); 

        }
    }
}
