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
    public class ProductService : IProductService
    {
        private readonly IFileService _fileService;
        private readonly IGenericRepository<Product> _productRepository;
        public ProductService(IFileService fileService, IGenericRepository<Product> productRepository)
        {
            _fileService = fileService;
            _productRepository = productRepository;
        }
        public async Task<bool> CreateAsync(ProductRequest request, CancellationToken cancellationToken = default)
        {
            var imageName = await _fileService.UploadFileAsync(request.Image, cancellationToken);
            if(imageName == null) return false;

            var newProduct = request.Adapt<Product>();
            newProduct.Image = imageName;
            try
            {
                await _productRepository.CreateAsync(newProduct, cancellationToken);
            }catch 
            {
                _fileService.DeleteFile(imageName);
                throw;
            }

            return true;
        }

        public async Task<List<ProductResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var products = await _productRepository.GetAsync(
                includes: [p => p.Translations, p => p.Category.Translations], cancellationToken: cancellationToken);

            return products.Adapt<List<ProductResponse>>();
        }
        
    }
}
