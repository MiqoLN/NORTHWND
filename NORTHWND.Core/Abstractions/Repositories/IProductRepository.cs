using NORTHWND.Core.BusinessModels;
using NORTHWND.Core.Entities;
using System.Collections.Generic;

namespace NORTHWND.Core.Abstractions.Repositories
{
    public interface IProductRepository:IRepositoryBase<Product>
    {
        public IEnumerable<ProductViewModel> Get(ProductViewModel model);
        public IEnumerable<ProductCategoryModel> GetCategories();
        public IEnumerable<ProductViewModel> GetReorderingProducts();
    }
}
