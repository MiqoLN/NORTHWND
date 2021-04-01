using NORTHWND.Core.BusinessModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NORTHWND.Core.Abstractions.Operations
{
    public interface ICustomerOperations
    {
        public void Add(CustomerRegisterModel model);
        public void EditCustomer(CustomerChangeModel model);
        public CustomerViewModel Get(string id);
        public IEnumerable<CustomersWithNoOrdersModel> GetCustomersWithNoOrders(int id);
        public IEnumerable<VipCustomerModel> GetVipCustomers();
        public IEnumerable<CustomersByGroup> GetCustomersByGroup();
        public IEnumerable<CustomerGroup> GetCustomersGroup();
        public IEnumerable<CustomerWithOnlyId> GetCustomersWithNoOrders();
        public IEnumerable<CustomerViewModel> Get();
    }
}
