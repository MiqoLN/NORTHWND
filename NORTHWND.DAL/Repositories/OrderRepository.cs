using NORTHWND.Core.Abstractions.Repositories;
using NORTHWND.Core.BusinessModels;
using NORTHWND.Core.Entities;
using NORTHWND.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NORTHWND.DAL.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(NORTHWNDContext context) : base(context) { }

        public void AddOrder(OrderRegisterModel model)
        {
            Context.Orders.Add(new Order
            {
                CustomerId = model.CustomerId,
                EmployeeId = model.EmployeeId,
                Freight = model.Freight,
                ShipAddress = model.ShipAddress,
                ShipName = model.ShipName,
                ShipCountry = model.ShipCountry,
                ShipCity = model.ShipCity
            });
        }

        public IEnumerable<OrderViewModel> Get(OrderViewModel model)
        {
            var orders = Context.Orders.AsQueryable();
            var res = (from o in orders
                       where (string.IsNullOrEmpty(model.CustomerId) || o.CustomerId == model.CustomerId)
                       && (string.IsNullOrEmpty(model.ShipCity) || o.ShipCity == model.ShipCity)
                       && (string.IsNullOrEmpty(model.ShipCountry) || o.ShipCountry == model.ShipCountry)
                       && (string.IsNullOrEmpty(model.ShipRegion) || o.ShipRegion == model.ShipRegion)
                       && (!model.OrderId.HasValue || o.OrderId == model.OrderId)
                       && (!model.RequiredDate.HasValue || o.RequiredDate == model.RequiredDate)
                       && (!model.EmployeeId.HasValue || o.EmployeeId == model.EmployeeId)
                       && (!model.Freight.HasValue || o.Freight == model.Freight)
                       && (!model.OrderDate.HasValue || o.OrderDate == model.OrderDate)
                       && (!model.ShippedDate.HasValue || o.ShippedDate == model.ShippedDate)
                       select new OrderViewModel
                       {
                           ShippedDate = o.ShippedDate,
                           RequiredDate = o.RequiredDate,
                           OrderDate = o.OrderDate,
                           Freight = o.Freight,
                           CustomerId = o.CustomerId,
                           EmployeeId = o.EmployeeId,
                           OrderId = o.OrderId,
                           ShipCity = o.ShipCity,
                           ShipCountry = o.ShipCountry,
                           ShipRegion = o.ShipRegion
                       }).ToList();
            return res;
        }

        public IEnumerable<OrderDetailsModel> GetDoubledOrders()
        {
            var query = from order in Context.OrderDetails
                        where order.Quantity >= 60
                        group order by new { order.OrderId, order.Quantity } into g
                        where g.Count() == 2
                        select g.Key.OrderId;
            var res = from order in Context.OrderDetails
                      where query.Contains(order.OrderId)
                      select new OrderDetailsModel
                      {
                          OrderId = order.OrderId,
                          Discount = order.Discount,
                          ProductId = order.ProductId,
                          Quantity = order.Quantity,
                          UnitPrice = order.UnitPrice
                      };
            return res.ToList();
        }

        public IEnumerable<InventoryListModel> GetInventoryList()
        {
            var query = from order in Context.Orders
                        join employee in Context.Employees on order.EmployeeId equals employee.EmployeeId
                        join orderDetail in Context.OrderDetails on order.OrderId equals orderDetail.OrderId
                        join product in Context.Products on orderDetail.ProductId equals product.ProductId
                        select new InventoryListModel
                        {
                            EmployeeId = employee.EmployeeId,
                            Lastname = employee.LastName,
                            OrderId = order.OrderId,
                            ProductName = product.ProductName,
                            Quantity = orderDetail.Quantity
                        };
            return query.ToList();
        }
        public IEnumerable<OrderViewModel> GetLateOrders()
        {
            var query = from order in Context.Orders
                        where order.RequiredDate < order.ShippedDate
                        select new OrderViewModel
                        {
                            ShippedDate = order.ShippedDate,
                            RequiredDate = order.RequiredDate,
                            CustomerId = order.CustomerId,
                            EmployeeId = order.EmployeeId,
                            Freight = order.Freight,
                            OrderDate = order.OrderDate,
                            OrderId = order.OrderId,
                            ShipCity = order.ShipCity,
                            ShipCountry = order.ShipCountry,
                            ShipRegion = order.ShipRegion
                        };
            return query.ToList();
        }

        public IEnumerable<OrderViewModel> GetMonthEndOrders()
        {
            var query = from order in Context.Orders
                        where
                        order.OrderDate.HasValue
                        && order.OrderDate.Value.AddDays(1).Month > order.OrderDate.Value.Month
                        select new OrderViewModel
                        {
                            OrderDate = order.OrderDate,
                            CustomerId = order.CustomerId,
                            EmployeeId = order.EmployeeId,
                            Freight = order.Freight,
                            OrderId = order.OrderId,
                            RequiredDate = order.RequiredDate,
                            ShipCity = order.ShipCity,
                            ShipCountry = order.ShipCountry,
                            ShippedDate = order.ShippedDate,
                            ShipRegion = order.ShipRegion
                        };
            return query.ToList();
        }

        public OrderViewModel GetOrder(int id)
        {

            var order = Context.Orders.Find(id) ?? throw new LogicException("Wrong Order Id");
            return new OrderViewModel
            {
                CustomerId = order.CustomerId,
                EmployeeId = order.EmployeeId,
                Freight = order.Freight,
                OrderDate = order.OrderDate,
                OrderId = order.OrderId,
                RequiredDate = order.RequiredDate,
                ShipCity = order.ShipCity,
                ShipCountry = order.ShipCountry,
                ShippedDate = order.ShippedDate,
                ShipRegion = order.ShipRegion
            };
        }

        public IEnumerable<OrderViewModel> GetOrdersByCountry(string country)
        {
            var query = (from o in Context.Orders
                         where o.ShipCountry == country
                         select new OrderViewModel
                         {
                             ShipCountry = o.ShipCountry,
                             CustomerId = o.CustomerId,
                             EmployeeId = o.EmployeeId,
                             Freight = o.Freight,
                             OrderDate = o.OrderDate,
                             OrderId = o.OrderId,
                             RequiredDate = o.RequiredDate,
                             ShipCity = o.ShipCity,
                             ShippedDate = o.ShippedDate,
                             ShipRegion = o.ShipRegion
                         }).ToList();
            return query;
        }

        public IEnumerable<OrderViewModel> GetRandomOrders()
        {

            var query = (from o in Context.Orders
                         orderby Guid.NewGuid()
                         select new OrderViewModel
                         {
                             CustomerId = o.CustomerId,
                             EmployeeId = o.EmployeeId,
                             Freight = o.Freight,
                             OrderDate = o.OrderDate,
                             OrderId = o.OrderId,
                             RequiredDate = o.RequiredDate,
                             ShipCity = o.ShipCity,
                             ShipCountry = o.ShipCountry,
                             ShippedDate = o.ShippedDate,
                             ShipRegion = o.ShipRegion
                         }
                         ).Take(Context.Orders.Count() / 50);
            return query.ToList();
        }

        public IEnumerable<OrdersTotalModel> GetTotalOrders()
        {
            var query = from orders in Context.OrderDetails
                        group orders by orders.OrderId into g
                        orderby g.Count() descending
                        select new OrdersTotalModel
                        {
                            OrderId = g.Key,
                            TotalOrderDetails = g.Count()
                        };
            return query.ToList();
        }
    }
}

