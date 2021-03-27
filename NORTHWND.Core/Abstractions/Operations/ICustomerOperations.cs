using NORTHWND.Core.BusinessModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NORTHWND.Core.Abstractions.Operations
{
    public interface ICustomerOperations
    {
        public IEnumerable<CustomerViewModel> GetAll();
        public void Add(CustomerRegistrationModel model);
    }
}
