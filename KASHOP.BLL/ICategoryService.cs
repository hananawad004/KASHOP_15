using KASHOP.DAL.DTO.Requests;
using KASHOP.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.BLL
{
     public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllAsync();
        Task<CategoryResponse?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CategoryRequest category);
        Task<bool> UpdateAsync(int id, CategoryRequest category);
        Task<bool> DeleteAsync(int id);



    }
}
