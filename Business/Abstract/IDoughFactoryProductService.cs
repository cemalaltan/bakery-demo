using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IDoughFactoryProductService
    {
        Task<List<DoughFactoryProduct>> GetAllAsync();
        Task AddAsync(DoughFactoryProduct doughFactoryProduct);
        Task DeleteAsync(DoughFactoryProduct doughFactoryProduct);
        Task UpdateAsync(DoughFactoryProduct doughFactoryProduct);
        Task<DoughFactoryProduct> GetByIdAsync(int id);
        Task<List<DoughFactoryProduct>> GetAllProductsAsync();

    }
}
