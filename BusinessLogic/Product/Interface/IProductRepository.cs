using DataClasses.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Product.Interface
{
    public interface IProductRepository
    {
        Task<ProductCore> GetByIdAsync(int id);
        Task<ProductCore> AddAsync(ProductCore product);
        Task<bool> UpdateAsync(ProductCore product);
    }
}
