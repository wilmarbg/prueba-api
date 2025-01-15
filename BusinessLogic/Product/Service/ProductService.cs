using BusinessLogic.Product.Interface;
using DataClasses.Core;
using DataClasses.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessLogic.Product.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IProductCache _cache;
        private readonly HttpClient _httpClient;


        public ProductService(HttpClient httpClient, IProductRepository repository, IProductCache cache)
        {
            _repository = repository;
            _cache = cache;
            _httpClient = httpClient;
        }

        public async Task<List<ProductResponseDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();
            if (products == null || !products.Any()) return new List<ProductResponseDto>();

            var productResponseList = new List<ProductResponseDto>();

            foreach (var product in products)
            {
                var statusName = _cache.GetStatusName(product.Status);
                var discount = await GetDiscountFromExternalService(product.ProductId);
                var finalPrice = product.Price * (100 - discount) / 100;

                productResponseList.Add(new ProductResponseDto
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    StatusName = statusName,
                    Stock = product.Stock,
                    Description = product.Description,
                    Price = product.Price,
                    Discount = discount,
                    FinalPrice = finalPrice
                });
            }

            return productResponseList;
        }

        public async Task<ProductResponseDto> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return null;

            var statusName = _cache.GetStatusName(product.Status);
            var discount = await GetDiscountFromExternalService(product.ProductId);
            var finalPrice = product.Price * (100 - discount) / 100;

            return new ProductResponseDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                StatusName = statusName,
                Stock = product.Stock,
                Description = product.Description,
                Price = product.Price,
                Discount = discount,
                FinalPrice = finalPrice
            };
        }

        public async Task<ProductResponseDto> CreateAsync(ProductRequestDto productDto)
        {
            var product = new ProductCore
            {
                Name = productDto.Name,
                Status = productDto.Status,
                Stock = productDto.Stock,
                Description = productDto.Description,
                Price = productDto.Price
            };

            var createdProduct = await _repository.AddAsync(product);
            return await GetByIdAsync(createdProduct.ProductId);
        }

        public async Task<bool> UpdateAsync(int id, ProductRequestDto productDto)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return false;

            product.Name = productDto.Name;
            product.Status = productDto.Status;
            product.Stock = productDto.Stock;
            product.Description = productDto.Description;
            product.Price = productDto.Price;

            return await _repository.UpdateAsync(product);
        }

        private async Task<decimal> GetDiscountFromExternalService(int productId)
        {
            try
            {
                var apiUrl = $"https://6785f601f80b78923aa4e05a.mockapi.io/api/v1/products/discount/{productId}";
                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var discountResponse = JsonSerializer.Deserialize<DiscountDto>(jsonString);

                    return discountResponse?.discount ?? 0;
                }
            }
            catch (Exception ex)
            {
                // Loguear el error, si es necesario.
                Console.WriteLine($"Error al obtener el descuento: {ex.Message}");
            }

            // Descuento predeterminado en caso de fallo.
            return 0;
        }
    }
}
