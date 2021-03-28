using NORTHWND.Core.BusinessModels;
using NORTHWND.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NORTHWND.Core.Abstractions.Repositories
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        public void AddOrder(OrderRegisterModel model);
        public IEnumerable<InventoryListModel> GetInventoryList();
        public IEnumerable<OrderViewModel> GetMonthEndOrders();
        public IEnumerable<OrdersTotalModel> GetTotalOrders();
        public IEnumerable<OrderViewModel> GetRandomOrders();
        public IEnumerable<OrderDetailsModel> GetDoubledOrders();
        public IEnumerable<OrderViewModel> GetLateOrders();
        public IEnumerable<OrderViewModel> GetOrdersByCountry(string country);
        public OrderViewModel GetOrder(int id);

    }
}
