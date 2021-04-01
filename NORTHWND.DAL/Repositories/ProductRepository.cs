using NORTHWND.Core.Abstractions.Repositories;
using NORTHWND.Core.BusinessModels;
using NORTHWND.Core.Entities;
using System.Collections.Generic;
using System.Linq;
namespace NORTHWND.DAL.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(NORTHWNDContext context) : base(context)
        {

        }

        public IEnumerable<ProductViewModel> Get(ProductViewModel model)
        {
            var products = Context.Products.AsQueryable();
            var res = (from p in products
                       where (string.IsNullOrEmpty(model.ProductName) || p.ProductName == model.ProductName)
                       && (string.IsNullOrEmpty(model.QuantityPerUnit) || p.QuantityPerUnit == model.QuantityPerUnit)
                       && (p.Discontinued == model.Discontinued)
                       && (!model.CategoryId.HasValue || p.CategoryId == model.CategoryId)
                       && (!model.ProductId.HasValue || p.ProductId == model.ProductId)
                       && (!model.SupplierId.HasValue || p.SupplierId == model.SupplierId)
                       && (!model.UnitPrice.HasValue || p.UnitPrice == model.UnitPrice)
                       && (!model.UnitsInStock.HasValue || p.UnitsInStock == model.UnitsInStock)
                       select new ProductViewModel
                       {
                           CategoryId = p.CategoryId,
                           UnitsInStock = p.UnitsInStock,
                           UnitPrice = p.UnitPrice,
                           Discontinued = p.Discontinued,
                           ProductId = p.ProductId,
                           ProductName = p.ProductName,
                           QuantityPerUnit = p.QuantityPerUnit,
                           SupplierId = p.SupplierId
                       }).ToList();
            return res;
        }
    }
}
