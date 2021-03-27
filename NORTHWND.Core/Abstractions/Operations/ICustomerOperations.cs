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
        public IEnumerable<CustomersWithNoOrdersModel> GetCustomersWithNoOrders(int id);
        public IEnumerable<VipCustomerModel> GetVipCustomers();
        public IEnumerable<CustomersByGroup> GetCustomersByGroup();
        public IEnumerable<CustomerGroup> GetCustomersGroup();
    }
}
