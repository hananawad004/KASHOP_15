using KASHOP.BLL.Settings;
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
            if (!AreTranslationsValid(category.Translations)) return false;
            var names = category.Translations.Select(t => t.Name).ToList();

            var existing = await _categoryRepository.GetOneAsync(
               filter: c=>c.Translations.Any(t => names.Contains(t.Name.ToLower())));

            if(existing != null) return false;
            
            var newCategory=category.Adapt<Category>();
            await _categoryRepository.CreateAsync(newCategory, cancellationToken);
            return true;
        }

         public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var category = await _categoryRepository.GetOneAsync(filter: c => c.Id == id, cancellationToken: cancellationToken);
            if (category == null) return false;
            await _categoryRepository.RemoveAsync(category, cancellationToken);
            return true;
        }

        public async Task<CategoryResponse?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var category = await _categoryRepository.GetOneAsync(filter: c => c.Id == id, includes: [c=>c.Translations], cancellationToken: cancellationToken);
            return category?.Adapt<CategoryResponse>();
        }



        public async Task<bool> UpdateAsync(
     int id,
     CategoryRequest category,
     CancellationToken cancellationToken = default)
        {
            var existingCategory = await _categoryRepository.GetOneAsync(
                filter: c => c.Id == id,
                includes: [c => c.Translations]
            );

            if (existingCategory == null)
                return false;

            // Update category properties
            existingCategory.Status = category.Status;

            // Update translations
            foreach (var translation in category.Translations)
            {
                var existingTranslation = existingCategory.Translations
                    .FirstOrDefault(t => t.Language == translation.Language);

                if (existingTranslation != null)
                {
                    // Update existing translation
                    existingTranslation.Name = translation.Name;
                }
                else
                {
                    // Add new translation
                    existingCategory.Translations.Add(new CategoryTranslation
                    {
                        Name = translation.Name,
                        Language = translation.Language
                    });
                }
            }

            await _categoryRepository.UpdateAsync(
                existingCategory, cancellationToken);

            return true;
        }
        public async Task<List<CategoryResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var categories = await _categoryRepository.GetAsync(includes: [cancellationToken=>cancellationToken.Translations], cancellationToken: cancellationToken);
            return categories.Adapt<List<CategoryResponse>>(); 

        }

        public static bool AreTranslationsValid(List<CategoryTranslationRequest> translations)
        {
            var languages = translations.Select(t => t.Language).ToList();
            if (languages.Distinct().Count() != languages.Count) return false; // Check for duplicate languages

            return languages.Count == SupportedLanguges.Languages.Length 
                && languages.All(lang => SupportedLanguges.Languages.Contains(lang));
        }
    }
}
