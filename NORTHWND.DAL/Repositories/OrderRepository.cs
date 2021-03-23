using NORTHWND.Core.Abstractions.Repositories;
using NORTHWND.Core.BusinessModels;
using NORTHWND.Core.Entities;
using NORTHWND.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NORTHWND.DAL.Repositories
{
    public class OrderRepository:RepositoryBase<Order>,IOrderRepository
    {
        public OrderRepository(NORTHWNDContext context):base(context){}

        public void AddOrder(OrderRegisterModel model)
        {
            var customer = Context.Customers.Find(model.CustomerId);
            if (customer == null)
                throw new LogicException("There is no customer with that Id");
            var employee = Context.Employees.Find(model.EmployeeId);
            if (employee == null)
                throw new LogicException("There is no employee with that Id");
            Context.Orders.Add(new Order
            {
                CustomerId = model.CustomerId,
                EmployeeId = model.EmployeeId,
                Freight=model.Freight,
                ShipAddress=model.ShipAddress,
                ShipName=model.ShipName,
                ShipCountry=model.ShipCountry,
                ShipCity=model.ShipCity
            });
        }
    }
}
