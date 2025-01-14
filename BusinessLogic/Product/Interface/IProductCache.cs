using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Product.Interface
{
    public interface IProductCache
    {
        string GetStatusName(int status);
    }
}
