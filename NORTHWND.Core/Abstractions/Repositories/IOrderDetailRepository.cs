using NORTHWND.Core.BusinessModels;
using NORTHWND.Core.Entities;
using System.Collections.Generic;

namespace NORTHWND.Core.Abstractions.Repositories
{
    public interface IOrderDetailRepository:IRepositoryBase<OrderDetail>
    {
        public IEnumerable<OrderDetailsModel> Get(OrderDetailsModel model);
        public IEnumerable<OrderDetail> GetRange(int orderId);
    }
}
