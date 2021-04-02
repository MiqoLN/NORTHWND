using Microsoft.Extensions.Logging;
using NORTHWND.Core.Abstractions;
using NORTHWND.Core.Abstractions.Operations;
using NORTHWND.Core.BusinessModels;
using NORTHWND.Core.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NORTHWND.BLL.Operations
{
    public class CustomerOperations : ICustomerOperations
    {
        private readonly IRepositoryManager _repositories;
        private readonly ILogger<CustomerOperations> _logger;
        public CustomerOperations(IRepositoryManager manager, ILogger<CustomerOperations> logger)
        {
            _repositories = manager;
            _logger = logger;
        }

        public void Add(CustomerRegisterModel model)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
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
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
        }

        public IEnumerable<CustomerViewModel> Get()
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var query = _repositories.Customers.GetAll().AsQueryable();
            var res = (from c in query
                      select new CustomerViewModel
                      {
                          City = c.City,
                          CompanyName = c.CompanyName,
                          ContactName = c.ContactName,
                          ContactTitle = c.ContactTitle,
                          Country = c.Country,
                          CustomerId = c.CustomerId,
                          Region = c.Region
                      }).ToList();
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
            return res;
        }

        public IEnumerable<VipCustomerModel> GetVipCustomers()
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var res = _repositories.Customers.GetVipCustomers();
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
            return res;
        }

        public IEnumerable<CustomersWithNoOrdersModel> GetCustomersWithNoOrders(int id)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var customer = _repositories.Employees.Get(id) ?? throw new LogicException("Wrong customerId");
            var res = _repositories.Customers.GetCustomersWithNoOrders(id);
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
            return res;
        }

        public IEnumerable<CustomersByGroup> GetCustomersByGroup()
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var res = _repositories.Customers.GetCustomersByGroup();
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
            return res;
        }


        public IEnumerable<CustomerGroup> GetCustomersGroup()
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var res = _repositories.Customers.GetCustomersGroup();
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
            return res;
        }

        public IEnumerable<CustomerWithOnlyId> GetCustomersWithNoOrders()
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var res = _repositories.Customers.GetCustomersWithNoOrders();
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
            return res;
        }
        public void EditCustomer(CustomerChangeModel model)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var customer = _repositories.Customers.GetSingle(u => u.CustomerId == model.CustomerId);
            if (customer == null)
                throw new LogicException("There is no customer with that Id");
            customer.City = string.IsNullOrEmpty(model.City) ? customer.City : model.City;
            customer.CompanyName = string.IsNullOrEmpty(model.CompanyName) ? customer.CompanyName : model.CompanyName;
            customer.ContactName = string.IsNullOrEmpty(model.ContactName) ? customer.ContactName : model.ContactName;
            customer.ContactTitle = string.IsNullOrEmpty(model.ContactTitle) ? customer.ContactTitle : model.ContactTitle;
            customer.Country = string.IsNullOrEmpty(model.Country) ? customer.Country : model.Country;
            customer.Region = string.IsNullOrEmpty(model.Region) ? customer.Region : model.Region;
            _repositories.Customers.Update(customer);
            _repositories.SaveChanges();
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
        }

        public CustomerViewModel Get(string id)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var customer = _repositories.Customers.GetSingle(u => u.CustomerId == id);
            if (customer == null)
                throw new LogicException("There is no customer with that id");
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
            return new CustomerViewModel
            {
                City = customer.City,
                CompanyName = customer.CompanyName,
                ContactName = customer.ContactName,
                ContactTitle = customer.ContactTitle,
                Country = customer.Country,
                CustomerId = customer.CustomerId,
                Region = customer.Region
            };
        }

        public IEnumerable<CustomerViewModel> Get(CustomerViewModel model)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var res = _repositories.Customers.Get(model);
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
            return res;
        }
    }
}
