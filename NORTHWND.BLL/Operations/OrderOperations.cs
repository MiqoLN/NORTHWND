using Microsoft.Extensions.Logging;
using NORTHWND.Core.Abstractions;
using NORTHWND.Core.Abstractions.Operations;
using NORTHWND.Core.BusinessModels;
using NORTHWND.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NORTHWND.BLL.Operations
{
    public class OrderOperations : IOrderOperations
    {
        private readonly IRepositoryManager _repositories;
        private readonly ILogger<OrderOperations> _logger;
        public OrderOperations(IRepositoryManager repositories, ILogger<OrderOperations> logger)
        {
            _repositories = repositories;
            _logger = logger;
        }
        public void AddOrder(OrderRegisterModel model)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var customer = _repositories.Customers.GetSingle(u => u.CustomerId == model.CustomerId);
            if (customer == null)
                throw new LogicException("There is no customer with that Id");
            var employee = _repositories.Employees.Get(model.EmployeeId);
            if (employee == null)
                throw new LogicException("There is no employee with that Id");
            _repositories.Orders.AddOrder(model);
            _repositories.SaveChanges();
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
        }

        public IEnumerable<InventoryListModel> GetInventoryList()
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var res= _repositories.Orders.GetInventoryList();
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
            return res;
        }

        public IEnumerable<OrderViewModel> GetMonthEndOrders()
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var res = _repositories.Orders.GetMonthEndOrders();
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
            return res;
        }

        public IEnumerable<OrderViewModel> GetOrders()
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var data = _repositories.Orders.GetAll();
            var res = from d in data
                      select new OrderViewModel
                      {
                          CustomerId = d.CustomerId,
                          EmployeeId = d.EmployeeId,
                          Freight = d.Freight,
                          OrderDate = d.OrderDate,
                          OrderId = d.OrderId,
                          RequiredDate = d.RequiredDate,
                          ShipCity = d.ShipCity,
                          ShipCountry = d.ShipCountry,
                          ShippedDate = d.ShippedDate,
                          ShipRegion = d.ShipRegion
                      };
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
            return res;
        }

        public IEnumerable<OrdersTotalModel> GetTotalOrders()
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var res = _repositories.Orders.GetTotalOrders();
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
            return res;
        }

        public IEnumerable<OrderViewModel> GetRandomOrders()
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var res = _repositories.Orders.GetRandomOrders();
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
            return res;
        }
        public IEnumerable<OrderDetailsModel> GetDoubledOrders()
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var res = _repositories.Orders.GetDoubledOrders();
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
            return res;
        }
        public IEnumerable<OrderViewModel> GetLateOrders()
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var res = _repositories.Orders.GetLateOrders();
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
            return res;
        }
        public OrderViewModel GetOrder(int id)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var order = _repositories.Orders.GetSingle(u => u.OrderId == id) ?? throw new LogicException("Wrong Order Id");
            var res = _repositories.Orders.GetOrder(id);
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
            return res;
        }

        public IEnumerable<OrderViewModel> GetOrdersByCountry(string country)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var order = _repositories.Orders.GetSingle(u => u.ShipCountry == country) ?? throw new LogicException("There is no country like that to ship");
            var res = _repositories.Orders.GetOrdersByCountry(country);
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
            return res;
        }

        public void EditOrder(OrderChangeModel model)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var order = _repositories.Orders.Get(model.OrderId);
            if (order == null)
                throw new LogicException("There is no order with that Id");
            if (!string.IsNullOrEmpty(model.CustomerId))
            {
                var customer = _repositories.Customers.GetSingle(u => u.CustomerId == model.CustomerId);
                if (customer == null)
                    throw new LogicException("There is no customer with that Id");
            }
            if (order.EmployeeId != null)
            {
                var employee = _repositories.Employees.Get((int)model.EmployeeId);
                if (employee == null)
                {
                    throw new LogicException("There is no employee with that Id");
                }
            }
            order.CustomerId = string.IsNullOrEmpty(model.CustomerId) ? order.CustomerId : model.CustomerId;
            order.EmployeeId = model.EmployeeId == null ? order.EmployeeId : model.EmployeeId;
            order.Freight = model.Freight == null ? order.Freight : model.Freight;
            order.ShipAddress = string.IsNullOrEmpty(model.ShipAddress) ? order.ShipAddress : model.ShipAddress;
            order.ShipCity = string.IsNullOrEmpty(model.ShipCity) ? order.ShipCity : model.ShipCity;
            order.ShipCountry = string.IsNullOrEmpty(model.ShipCountry) ? order.ShipCountry : model.ShipCountry;
            order.ShipName = string.IsNullOrEmpty(model.ShipName) ? order.ShipName : model.ShipName; ;
            _repositories.Orders.Update(order);
            _repositories.SaveChanges();
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
        }

        public IEnumerable<OrderViewModel> Get(OrderViewModel model)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var res = _repositories.Orders.Get(model);
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
            return res;
        }

        public void Delete(int id)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");

            using (var transaction = _repositories.BeginTransaction())
            {
                try
                {
                    var order = _repositories.Orders.Get(id);
                    if (order == null)
                        throw new LogicException("There is no order with that parameters");
                    var od = _repositories.OrderDetails.GetRange(id);
                    if (od == null)
                        throw new LogicException("There is no order with that parameters");
                    _repositories.OrderDetails.RemoveRange(od);
                    _repositories.Orders.Remove(order);
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
    }
}
