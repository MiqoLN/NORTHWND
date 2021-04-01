using NORTHWND.Core.BusinessModels;
using NORTHWND.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NORTHWND.Core.Abstractions.Operations
{
    public interface IOrderOperations
    {
        public void AddOrder(OrderRegisterModel model);
        public void EditOrder(OrderChangeModel model);
        public OrderViewModel GetOrder(int id);
        public IEnumerable<OrderViewModel> GetOrders();
        public IEnumerable<InventoryListModel> GetInventoryList();
        public IEnumerable<OrderViewModel> GetMonthEndOrders();
        public IEnumerable<OrdersTotalModel> GetTotalOrders();
        public IEnumerable<OrderViewModel> GetRandomOrders();
        public IEnumerable<OrderDetailsModel> GetDoubledOrders();
        public IEnumerable<OrderViewModel> GetLateOrders();
        public IEnumerable<OrderViewModel> GetOrdersByCountry(string country);
        public IEnumerable<OrderViewModel> Get(OrderViewModel model);
    }
}
