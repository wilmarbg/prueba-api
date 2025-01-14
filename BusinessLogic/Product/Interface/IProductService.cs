using DataClasses.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Product.Interface
{
    public interface IProductService
    {
        Task<ProductResponseDto> GetByIdAsync(int id);
        Task<ProductResponseDto> CreateAsync(ProductRequestDto productDto);
        Task<bool> UpdateAsync(int id, ProductRequestDto productDto);
    }
}
