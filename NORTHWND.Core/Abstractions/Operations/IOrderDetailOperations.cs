using NORTHWND.Core.BusinessModels;
using System;
using System.Collections.Generic;
using System.Text;

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
