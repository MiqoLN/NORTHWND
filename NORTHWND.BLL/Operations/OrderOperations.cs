using NORTHWND.Core.Abstractions;
using NORTHWND.Core.Abstractions.Operations;
using NORTHWND.Core.BusinessModels;
using NORTHWND.Core.Entities;
using NORTHWND.Core.Exceptions;
using NORTHWND.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NORTHWND.BLL.Operations
{
    public class OrderOperations : IOrderOperations
    {
        private readonly IRepositoryManager _repositories;
        public OrderOperations(IRepositoryManager repositoryManager)
        {
            _repositories = repositoryManager;
        }
        public void AddOrder(OrderRegisterModel model)
        {
            var customer = _repositories.Customers.GetSingle(u => u.CustomerId == model.CustomerId);
            if (customer == null)
                throw new LogicException("There is no customer with that Id");
            var employee = _repositories.Employees.Get(model.EmployeeId);
            if (employee == null)
                throw new LogicException("There is no employee with that Id");
            _repositories.Orders.AddOrder(model);
            _repositories.SaveChanges();
        }

        public IEnumerable<InventoryListModel> GetInventoryList()
        {
            return _repositories.Orders.GetInventoryList();
        }

        public IEnumerable<OrderViewModel> GetMonthEndOrders()
        {
            return _repositories.Orders.GetMonthEndOrders();
        }

        public IEnumerable<OrderViewModel> GetOrders()
        {
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
            return res;
        }

        public IEnumerable<OrdersTotalModel> GetTotalOrders()
        {
            return _repositories.Orders.GetTotalOrders();
        }

        public IEnumerable<OrderViewModel> GetRandomOrders()
        {
            return _repositories.Orders.GetRandomOrders();
        }
        public IEnumerable<OrderDetailsModel> GetDoubledOrders()
        {
            return _repositories.Orders.GetDoubledOrders();
        }
        /*
        DO TRANSACTION!!
        public void Test()
        {
            using (var transaction = _repositories.BeginTransaction())
            {
                try
                {
                    //add
                    //remove
                    _repositories.SaveChanges();
                    transaction.Commit();
                }
                catch (System.Exception)
                {
                    transaction.RollBack();
                    throw;
                }
            }
        }
        */
        public IEnumerable<OrderViewModel> GetLateOrders()
        {
            return _repositories.Orders.GetLateOrders();
        }
        public OrderViewModel GetOrder(int id)
        {
            var order = _repositories.Orders.GetSingle(u => u.OrderId == id) ?? throw new LogicException("Wrong Order Id");
            return _repositories.Orders.GetOrder(id);
        }

        public IEnumerable<OrderViewModel> GetOrdersByCountry(string country)
        {
            var order = _repositories.Orders.GetSingle(u => u.ShipCountry == country) ?? throw new LogicException("There is no country like that to ship");
            return _repositories.Orders.GetOrdersByCountry(country);
        }

        public void EditOrder(OrderChangeModel model)
        {
            var order = _repositories.Orders.Get(model.OrderId);
            if (order == null)
                throw new LogicException("There is no order with that Id");
            if (!string.IsNullOrEmpty(model.CustomerId))
            {
                var customer = _repositories.Customers.GetSingle(u => u.CustomerId == model.CustomerId);
                if (customer == null)
                    throw new LogicException("There is no customer with that Id");
            }
            if(order.EmployeeId!=null)
            {
                var employee = _repositories.Employees.Get((int)model.EmployeeId);
                if(employee==null)
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
        }

        public IEnumerable<OrderViewModel> Get(OrderViewModel model)
        {
            return _repositories.Orders.Get(model);
        }
    }
}
