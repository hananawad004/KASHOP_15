using KASHOP.DAL.DTO.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.DTO.Responses
{
    public class CategoryResponse
    {
        public int Id { get; set; }

        //public List<CategoryTranslationRequest> Translations { get; set; } = new();
        public string Name { get; set; } = null!;
        public bool Status { get; set; } 
    }
}
