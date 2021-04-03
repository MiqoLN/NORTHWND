using NORTHWND.Core.Abstractions.Repositories;
using NORTHWND.Core.BusinessModels;
using NORTHWND.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NORTHWND.DAL.Repositories
{
    public class OrderDetailRepository : RepositoryBase<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(NORTHWNDContext context) : base(context) { }

        public IEnumerable<OrderDetailsModel> Get(OrderDetailsModel model)
        {
            var od = Context.OrderDetails.AsQueryable();
            var res = (from o in od
                       where (!model.OrderId.HasValue || o.OrderId == model.OrderId)
                       && (!model.ProductId.HasValue || o.ProductId == model.ProductId)
                       && (!model.Quantity.HasValue || o.Quantity == model.Quantity)
                       && (!model.UnitPrice.HasValue || o.UnitPrice == model.UnitPrice)
                       && (!model.Discount.HasValue || o.Discount == model.Discount)
                       select new OrderDetailsModel
                       {
                           Discount = o.Discount,
                           UnitPrice = o.UnitPrice,
                           OrderId = o.OrderId,
                           ProductId = o.ProductId,
                           Quantity = o.Quantity
                       }).ToList();
            return res;
        }

        public IEnumerable<OrderDetail> GetRange(int orderId)
        {
            var od = Context.OrderDetails.AsQueryable();
            var res = (from o in od
                       where o.OrderId == orderId
                       select o).ToList();
            return res;
        }
    }
}
