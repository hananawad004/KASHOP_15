using KASHOP.DAL.DTO.Requests;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace KASHOP.BLL.Settings
{
    public static class MapsterConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<Category, CategoryResponse>.NewConfig()
                .Map(dest => dest.Name, src => src.Translations.
                Where(t => t.Language == CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
                .Select(t => t.Name).FirstOrDefault());

            TypeAdapterConfig<Product, ProductResponse>.NewConfig()
               .Map(dest => dest.Name, src => src.Translations.
               Where(t => t.Language == CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
               .Select(t => t.Name).FirstOrDefault())

               .Map(dest => dest.Description, src => src.Translations.
               Where(t => t.Language == CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
               .Select(t => t.Description).FirstOrDefault()
               )
               .Map(dest=>dest.ImageUrl, src => $"/images/{src.Image}")
               .Map(dest => dest.CategoryName, src => src.Category.Translations.
               Where(t => t.Language == CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
               .Select(t => t.Name).FirstOrDefault()
               )
               ;

            TypeAdapterConfig<ProductRequest, Product>.NewConfig()
                .Ignore(dest => dest.Image);

        }

    }
}
