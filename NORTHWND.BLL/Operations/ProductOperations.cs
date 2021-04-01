using NORTHWND.Core.Abstractions;
using NORTHWND.Core.Abstractions.Operations;
using NORTHWND.Core.BusinessModels;
using NORTHWND.Core.Entities;
using NORTHWND.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NORTHWND.BLL.Operations
{
    public class ProductOperations : IProductOperations
    {
        private readonly IRepositoryManager _repositories;
        public ProductOperations(IRepositoryManager repositories)
        {
            _repositories = repositories;
        }

        public void Add(ProductRegisterModel model)
        {
            var supplier = _repositories.Suppliers.Get(model.SupplierId);
            if (supplier == null)
                throw new LogicException("There is no supplier with that Id");
            var category = _repositories.Categories.Get(model.CategoryId);
            if (category == null)
                throw new LogicException("There is no category with that Id");
            _repositories.Products.Add(new Product
            {
                CategoryId = model.CategoryId,
                Discontinued = model.Discontinued,
                ProductName = model.ProductName,
                QuantityPerUnit = model.QuantityPerUnit,
                SupplierId = model.SupplierId,
                UnitPrice = model.UnitPrice,
                UnitsInStock = model.UnitsInStock
            });
            _repositories.SaveChanges();
        }

        public void Edit(ProductChangeModel model)
        {
            var product = _repositories.Products.Get(model.ProductId);
            if (product == null)
                throw new LogicException("There is no product with that Id");
            if (model.SupplierId != null)
            {
                var supplier = _repositories.Suppliers.Get((int)model.SupplierId);
                if (supplier == null)
                    throw new LogicException("There is no supplier with that Id");
            }
            if (model.CategoryId != null)
            {
                var category = _repositories.Categories.Get((int)model.CategoryId);
                if (category == null)
                    throw new LogicException("There is no category with that Id");
            }
            product.SupplierId = model.SupplierId;
            product.CategoryId = model.CategoryId;
            product.ProductName = model.ProductName == null ? product.ProductName : model.ProductName;
            product.QuantityPerUnit = model.QuantityPerUnit == null ? product.QuantityPerUnit : model.QuantityPerUnit;
            product.UnitPrice = model.UnitPrice == null ? product.UnitPrice : model.UnitPrice;
            product.UnitsInStock = model.UnitsInStock == null ? product.UnitsInStock : model.UnitsInStock;
            _repositories.Products.Update(product);
            _repositories.SaveChanges();
        }

        public IEnumerable<ProductViewModel> Get()
        {
            var products = _repositories.Products.GetAll().AsQueryable();
            var res = (from p in products
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

        public ProductViewModel Get(int id)
        {
            var product = _repositories.Products.Get(id);
            if (product == null)
                throw new LogicException("There is no product with that Id");
            return new ProductViewModel
            {
                CategoryId = product.CategoryId,
                Discontinued = product.Discontinued,
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                QuantityPerUnit = product.QuantityPerUnit,
                SupplierId = product.SupplierId,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock
            };
        }
    }
}
