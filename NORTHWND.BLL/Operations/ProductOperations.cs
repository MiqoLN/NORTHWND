using Microsoft.Extensions.Logging;
using NORTHWND.Core.Abstractions;
using NORTHWND.Core.Abstractions.Operations;
using NORTHWND.Core.BusinessModels;
using NORTHWND.Core.Entities;
using NORTHWND.Core.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NORTHWND.BLL.Operations
{
    public class ProductOperations : IProductOperations
    {
        private readonly IRepositoryManager _repositories;
        private readonly ILogger<ProductOperations> _logger;
        public ProductOperations(IRepositoryManager repositories, ILogger<ProductOperations> logger)
        {
            _repositories = repositories;
            _logger = logger;
        }

        public void Add(ProductRegisterModel model)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var supplier = _repositories.Suppliers.Get((int)model.SupplierId);
            if (supplier == null)
                throw new LogicException("There is no supplier with that Id");
            var category = _repositories.Categories.Get((int)model.CategoryId);
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
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
        }

        public void Edit(ProductChangeModel model)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
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
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
        }

        public IEnumerable<ProductViewModel> Get()
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
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
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
            return res;
        }

        public ProductViewModel Get(int id)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var product = _repositories.Products.Get(id);
            if (product == null)
                throw new LogicException("There is no product with that Id");
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
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

        public IEnumerable<ProductViewModel> Get(ProductViewModel model)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var res = _repositories.Products.Get(model);
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
            return res;
        }

        public IEnumerable<ProductCategoryModel> GetCategories()
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var res = _repositories.Products.GetCategories();
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
            return res;
        }

        public IEnumerable<ProductViewModel> GetReorderingProducts()
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var res = _repositories.Products.GetReorderingProducts();
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
            return res;

        }
    }
}
