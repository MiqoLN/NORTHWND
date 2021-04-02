using NORTHWND.Core.BusinessModels;
using System.Collections.Generic;

namespace NORTHWND.Core.Abstractions.Operations
{
    public interface IOrderDetailOperations
    {
        public void Add(OrderDetailRegisterModel model);
        public void Delete(OrderDetailDeleteModel model);
        public IEnumerable<OrderDetailsModel> GetAll();
        public IEnumerable<OrderDetailsModel> Get(OrderDetailsModel model);
    }
}
