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

        public IEnumerable<ProductCategoryModel> GetCategories()
        {
            var products = Context.Products.AsQueryable();
            var categories = Context.Categories.AsQueryable();
            var query = from p in products
                        group p by p.CategoryId into pp
                        orderby pp.Count() descending
                        select new
                        {
                            Id = pp.Key,
                            Count = pp.Count()
                        };
            var res = (from r in query
                       join c in categories
                       on r.Id equals c.CategoryId
                       select new ProductCategoryModel
                       {
                           CategoryName = c.CategoryName,
                           Count = r.Count
                       }).ToList();
            return res;
        }

        public IEnumerable<ProductViewModel> GetReorderingProducts()
        {
            var products = Context.Products.AsQueryable();
            var res = (from p in products
                       where (p.UnitsInStock + p.UnitsOnOrder < p.ReorderLevel) && p.Discontinued == false
                       orderby p.ProductId
                       select new ProductViewModel
                       {
                           CategoryId = p.CategoryId,
                           Discontinued = p.Discontinued,
                           ProductId = p.ProductId,
                           ProductName = p.ProductName,
                           QuantityPerUnit = p.QuantityPerUnit,
                           SupplierId = p.SupplierId,
                           UnitPrice = p.UnitPrice,
                           UnitsInStock = p.UnitsInStock
                       }).ToList();
            return res;
        }
    }
}
