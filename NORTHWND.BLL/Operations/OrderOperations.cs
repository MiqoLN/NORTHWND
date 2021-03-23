using NORTHWND.Core.Abstractions;
using NORTHWND.Core.Abstractions.Operations;
using NORTHWND.Core.BusinessModels;
using NORTHWND.Core.Entities;
using NORTHWND.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NORTHWND.BLL.Operations
{
    public class OrderOperations : IOrderOperations
    {
        private IRepositoryManager _repositories;
        public OrderOperations(IRepositoryManager repositoryManager)
        {
            _repositories = repositoryManager;
        }
        public void AddOrder(OrderRegisterModel model)
        {
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
