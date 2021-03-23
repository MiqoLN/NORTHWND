using NORTHWND.Core.BusinessModels;
using NORTHWND.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NORTHWND.Core.Abstractions.Operations
{
    public interface IOrderOperations
    {
        public IEnumerable<Order> GetOrders();
        public void AddOrder(OrderRegisterModel model);
    }
}
