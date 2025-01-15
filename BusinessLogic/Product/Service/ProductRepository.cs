using BusinessLogic.Product.Interface;
using DataClasses.Core;
using PruebaWebApi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Product.Service
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<ProductCore> _products;

        public ProductRepository()
        {
            _products = FileHelper.LoadProducts();
        }

        public Task<List<ProductCore>> GetAllAsync()
        {
            return Task.FromResult(_products.ToList());
        }

        public Task<ProductCore> GetByIdAsync(int id)
        {
            return Task.FromResult(_products.FirstOrDefault(p => p.ProductId == id));
        }

        public Task<ProductCore> AddAsync(ProductCore product)
        {
            product.ProductId = _products.Count > 0 ? _products.Max(p => p.ProductId) + 1 : 1;
            _products.Add(product);
            FileHelper.SaveProducts(_products);
            return Task.FromResult(product);
        }

        public Task<bool> UpdateAsync(ProductCore product)
        {
            var existing = _products.FirstOrDefault(p => p.ProductId == product.ProductId);
            if (existing == null) return Task.FromResult(false);

            existing.Name = product.Name;
            existing.Status = product.Status;
            existing.Stock = product.Stock;
            existing.Description = product.Description;
            existing.Price = product.Price;

            FileHelper.SaveProducts(_products);
            return Task.FromResult(true);
        }
    }
}
