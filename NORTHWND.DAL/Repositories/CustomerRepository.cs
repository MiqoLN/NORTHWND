using NORTHWND.Core.Abstractions.Repositories;
using NORTHWND.Core.BusinessModels;
using NORTHWND.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NORTHWND.DAL.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(NORTHWNDContext context) : base(context) { }

        public void AddCustomer(CustomerRegistrationModel model)
        {
            Context.Customers.Add(new Customer
            {
                CustomerId=model.CustomerId,
                City = model.City,
                CompanyName = model.CompanyName,
                ContactName = model.ContactName,
                ContactTitle = model.ContactTitle,
                Country = model.Country,
                Region = model.Region
            });
        }
    }
}
