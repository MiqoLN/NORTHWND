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
            var customer = _repositories.Customers.GetSingle(u=>u.CustomerId==model.CustomerId);
            if (customer == null)
                throw new LogicException("There is no customer with that Id");
            var employee = _repositories.Employees.Get(model.EmployeeId);
            if (employee == null)
                throw new LogicException("There is no employee with that Id");
            _repositories.Orders.AddOrder(model);
            _repositories.SaveChanges();
        }
        public IEnumerable<Order> GetOrders()
        {
            var query = _repositories.Orders.GetAll();
            return query.ToList();
        }
    }
}
