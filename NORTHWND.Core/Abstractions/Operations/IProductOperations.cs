using NORTHWND.Core.BusinessModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NORTHWND.Core.Abstractions.Operations
{
    public interface IProductOperations
    {
        public void Edit(ProductChangeModel model);
        public void Add(ProductRegisterModel model);
        public ProductViewModel Get(int id);
        public IEnumerable<ProductViewModel> Get();
        public IEnumerable<ProductViewModel> Get(ProductViewModel model);

    }
}
