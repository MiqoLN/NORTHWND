using Microsoft.Extensions.Logging;
using NORTHWND.Core.Abstractions;
using NORTHWND.Core.Abstractions.Operations;
using NORTHWND.Core.BusinessModels;
using NORTHWND.Core.Entities;
using NORTHWND.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NORTHWND.BLL.Operations
{
    public class OrderDetailOperations : IOrderDetailOperations
    {
        private readonly IRepositoryManager _repositories;
        private readonly ILogger<OrderDetailOperations> _logger;
        public OrderDetailOperations(IRepositoryManager repositories, ILogger<OrderDetailOperations> logger)
        {
            _repositories = repositories;
            _logger = logger;
        }

        public void Add(OrderDetailRegisterModel model)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var product = _repositories.Products.GetSingle(u => u.ProductId == model.ProductId);
            if (product == null)
                throw new LogicException("Wrong productId");
            var order = _repositories.Orders.GetSingle(u => u.OrderId == model.OrderId);
            if (order == null)
                throw new LogicException("Wrong OrderId");
            _repositories.OrderDetails.Add(new OrderDetail
            {
                Discount = model.Discount,
                OrderId = model.OrderId,
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                UnitPrice = model.UnitPrice
            });
            _repositories.SaveChanges();
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
        }

        public void Delete(OrderDetailDeleteModel model)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");

            using (var transaction = _repositories.BeginTransaction())
            {
                try
                {
                    var od = _repositories.OrderDetails.GetSingle(u => u.OrderId == model.OrderId && u.ProductId == model.ProductId);
                    if (od == null)
                        throw new LogicException("There is no order with that parameters");
                    _repositories.OrderDetails.Remove(od);
                    _repositories.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.RollBack();
                    throw ex;
                }
            }
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
        }

        public IEnumerable<OrderDetailsModel> Get(OrderDetailsModel model)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var res = _repositories.OrderDetails.Get(model);
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
            return res;
        }

        public IEnumerable<OrderDetailsModel> GetAll()
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var ods = _repositories.OrderDetails.GetAll().AsQueryable();
            var res = (from od in ods
                       select new OrderDetailsModel
                       {
                           Discount = od.Discount,
                           OrderId = od.OrderId,
                           ProductId = od.ProductId,
                           Quantity = od.Quantity,
                           UnitPrice = od.UnitPrice

                       }).ToList();
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
            return res;
        }
    }
}
