using NORTHWND.Core.BusinessModels;
using NORTHWND.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NORTHWND.Core.Abstractions.Repositories
{
    public interface IProductRepository:IRepositoryBase<Product>
    {
        public IEnumerable<ProductViewModel> Get(ProductViewModel model);
    }
}
