using LaQuimera.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaQuimera.Services
{
    public interface IProductService
    {
        Task<List<ProductModel>> GetByProductAsync(string searchProduct);
    }
}