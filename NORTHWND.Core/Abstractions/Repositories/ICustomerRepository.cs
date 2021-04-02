using NORTHWND.Core.BusinessModels;
using NORTHWND.Core.Entities;
using System.Collections.Generic;

namespace NORTHWND.Core.Abstractions.Repositories
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        public void AddCustomer(CustomerRegisterModel model);
        public IEnumerable<CustomerViewModel> Get(CustomerViewModel model);
        public IEnumerable<CustomersWithNoOrdersModel> GetCustomersWithNoOrders(int id);
        public IEnumerable<VipCustomerModel> GetVipCustomers();
        public IEnumerable<CustomersByGroup> GetCustomersByGroup();
        public IEnumerable<CustomerGroup> GetCustomersGroup();
        public IEnumerable<CustomerWithOnlyId> GetCustomersWithNoOrders();

    }
}
