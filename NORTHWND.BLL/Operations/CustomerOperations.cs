using NORTHWND.Core.Abstractions;
using NORTHWND.Core.Abstractions.Operations;
using NORTHWND.Core.BusinessModels;
using NORTHWND.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NORTHWND.BLL.Operations
{
    public class CustomerOperations : ICustomerOperations
    {
        private readonly IRepositoryManager _repositories;
        public CustomerOperations(IRepositoryManager manager)
        {
            _repositories = manager;
        }

        public void Add(CustomerRegistrationModel model)
        {
            var modelCheck = _repositories.Customers.GetSingle(u => u.CustomerId == model.CustomerId);
            if (modelCheck == null)
            {
                _repositories.Customers.AddCustomer(model);
                _repositories.SaveChanges();
            }
            else
            {
                throw new LogicException("There is already customer with that Id");
            }
        }

        public IEnumerable<CustomerViewModel> GetAll()
        {
            var query = _repositories.Customers.GetAll().AsQueryable();
            var res = from c in query
                      select new CustomerViewModel
                      {
                          City = c.City,
                          CompanyName = c.CompanyName,
                          ContactName = c.ContactName,
                          ContactTitle = c.ContactTitle,
                          Country = c.Country,
                          CustomerId = c.CustomerId,
                          Region = c.Region
                      };
            return res.ToList();
        }

        public IEnumerable<VipCustomerModel> GetVipCustomers()
        {
            return _repositories.Customers.GetVipCustomers();
        }

        public IEnumerable<CustomersWithNoOrdersModel> GetCustomersWithNoOrders(int id)
        {
            var customer = _repositories.Employees.Get(id) ?? throw new LogicException("Wrong customerId");
            return _repositories.Customers.GetCustomersWithNoOrders(id);
        }

        public IEnumerable<CustomersByGroup> GetCustomersByGroup()
        {
            return _repositories.Customers.GetCustomersByGroup();
        }


        public IEnumerable<CustomerGroup> GetCustomersGroup()
        {
            return _repositories.Customers.GetCustomersGroup();
        }

        public IEnumerable<CustomerWithOnlyId> GetCustomersWithNoOrders()
        {
            return _repositories.Customers.GetCustomersWithNoOrders();
        }
    }
}
