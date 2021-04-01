using NORTHWND.Core.Abstractions.Repositories;
using NORTHWND.Core.BusinessModels;
using NORTHWND.Core.Entities;
using System.Collections.Generic;
using System.Linq;


namespace NORTHWND.DAL.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(NORTHWNDContext context) : base(context) { }

        public void AddCustomer(CustomerRegisterModel model)
        {
            Context.Customers.Add(new Customer
            {
                CustomerId = model.CustomerId,
                City = model.City,
                CompanyName = model.CompanyName,
                ContactName = model.ContactName,
                ContactTitle = model.ContactTitle,
                Country = model.Country,
                Region = model.Region
            });
        }

        public IEnumerable<CustomersWithNoOrdersModel> GetCustomersWithNoOrders(int id)
        {

            var customerIds = (from order in Context.Orders
                               where order.EmployeeId == id
                               select order.CustomerId).ToList();


            var query = from customer in Context.Customers
                        where !customerIds.Contains(customer.CustomerId)
                        select new CustomersWithNoOrdersModel
                        {
                            CustomerId = customer.CustomerId,
                        };
            return query.ToList();
        }

        public IEnumerable<VipCustomerModel> GetVipCustomers()
        {
            var query = from c in Context.Customers
                        join o in Context.Orders on c.CustomerId equals o.CustomerId
                        join od in Context.OrderDetails on o.OrderId equals od.OrderId
                        group od by new { c.CustomerId, c.CompanyName } into gc
                        where gc.Sum(x=>x.Quantity*x.UnitPrice-(decimal)(1-x.Discount))>10000
                        orderby gc.Sum(x => x.Quantity * x.UnitPrice - (decimal)(1 - x.Discount)) descending
                        select new VipCustomerModel
                        {
                            CustomerId = gc.Key.CustomerId,
                            CompanyName = gc.Key.CompanyName,
                            TotalOrderAmount = gc.Sum(x => x.Quantity * x.UnitPrice - (decimal)(1 - x.Discount))
                        };

            return query.ToList();

        }
        public IEnumerable<CustomersByGroup> GetCustomersByGroup()
        {
            var query = from c in Context.Customers
                        join o in Context.Orders on c.CustomerId equals o.CustomerId
                        join od in Context.OrderDetails on o.OrderId equals od.OrderId
                        group od by new { c.CustomerId, c.CompanyName } into gc
                        select new CustomersByGroup
                        {
                            CustomerId = gc.Key.CustomerId,
                            CompanyName = gc.Key.CompanyName,
                            TotalOrderAmount = gc.Sum(x => x.Quantity * x.UnitPrice - (decimal)(1 - x.Discount)),
                            CustomerGroup = (
                            gc.Sum(x => x.Quantity * x.UnitPrice - (decimal)(1 - x.Discount)) < 1000 ? "Low" :
                            gc.Sum(x => x.Quantity * x.UnitPrice - (decimal)(1 - x.Discount)) < 5000 ? "Medium" :
                            gc.Sum(x => x.Quantity * x.UnitPrice - (decimal)(1 - x.Discount)) < 10000 ? "High" : "Very High"
                            )
                        };
            return query;
        }
        public IEnumerable<CustomerGroup> GetCustomersGroup()
        {
            var query = GetCustomersByGroup();
            var count = query.Count();
            var res = from q in query
                      group q by q.CustomerGroup into g
                      select new CustomerGroup
                      {
                          Group = g.Key,
                          TotalInGroup = g.Count(),
                          PercentaceInGroup = (decimal)g.Count() / count
                      };
            return res;
        }
        public IEnumerable<CustomerWithOnlyId> GetCustomersWithNoOrders()
        {
            var orders = Context.Orders.AsQueryable();
            var customers = Context.Customers.AsQueryable();
            var res = (from c in customers
                       join o in orders on c.CustomerId equals o.CustomerId into gj
                       from sub in gj.DefaultIfEmpty()
                       where sub.CustomerId == null
                       select new CustomerWithOnlyId
                       {
                           CustomerId = c.CustomerId,
                           //              OrderId = sub == null ? -1 : sub.OrderId
                       }).ToList();

            return res;
        }

        public IEnumerable<CustomerViewModel> Get(CustomerViewModel model)
        {
            var customers = Context.Customers.AsQueryable();
            var res = (from c in customers
                       where (string.IsNullOrEmpty(model.City) || c.City == model.City)
                       && (string.IsNullOrEmpty(model.CompanyName) || c.CompanyName == model.CompanyName)
                       && (string.IsNullOrEmpty(model.ContactName) || c.ContactName == model.ContactName)
                       && (string.IsNullOrEmpty(model.ContactTitle) || c.ContactTitle == model.ContactTitle)
                       && (string.IsNullOrEmpty(model.Country) || c.Country == model.Country)
                       && (string.IsNullOrEmpty(model.CustomerId) || c.CustomerId == model.CustomerId)
                       && (string.IsNullOrEmpty(model.Region) || c.Region == model.Region)
                       select new CustomerViewModel
                       {
                           Region = c.Region,
                           CustomerId = c.CustomerId,
                           City = c.City,
                           CompanyName = c.CompanyName,
                           ContactName = c.ContactName,
                           ContactTitle = c.ContactTitle,
                           Country = c.Country
                       }).ToList();
            return res;
        }
    }
}
