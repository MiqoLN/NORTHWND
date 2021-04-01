using NORTHWND.Core.Abstractions.Repositories;
using NORTHWND.Core.BusinessModels;
using NORTHWND.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NORTHWND.DAL.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(NORTHWNDContext context) : base(context) { }


        public void AddEmployee(EmployeeRegisterModel model)
        {
            Context.Employees.Add(new Employee
            {
                BirthDate = model.BirthDate,
                City = model.City,
                Country = model.Country,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Region = model.Region,
                Title = model.Title,
                TitleOfCourtesy = model.TitleOfCourtesy
            });
        }

        public IEnumerable<EmployeeViewModel> Get(EmployeeViewModel model)
        {
            var employees = Context.Employees.AsQueryable();
            var res = (from e in employees
                       where (string.IsNullOrEmpty(model.City) || e.City == model.City)
                       && (string.IsNullOrEmpty(model.Country) || e.Country == model.Country)
                       && (string.IsNullOrEmpty(model.FirstName) || e.FirstName == model.FirstName)
                       && (string.IsNullOrEmpty(model.LastName) || e.LastName == model.LastName)
                       && (string.IsNullOrEmpty(model.Region) || e.Region == model.Region)
                       && (string.IsNullOrEmpty(model.Title) || e.Title == model.Title)
                       && (string.IsNullOrEmpty(model.TitleOfCourtesy) || e.TitleOfCourtesy == model.TitleOfCourtesy)
                       && (!model.BirthDate.HasValue || e.BirthDate == model.BirthDate)
                       && (!model.EmployeeId.HasValue || e.EmployeeId == model.EmployeeId)
                       select new EmployeeViewModel
                       {
                           EmployeeId = e.EmployeeId,
                           BirthDate = e.BirthDate,
                           City = e.City,
                           Country = e.Country,
                           FirstName = e.FirstName,
                           LastName = e.LastName,
                           Region = e.Region,
                           Title = e.Title,
                           TitleOfCourtesy = e.TitleOfCourtesy
                       }).ToList();
            return res;
        }

        public IEnumerable<LateEmployeeModel> GetLateEmployees()
        {
            var orders = (from employee in Context.Employees
                          join order in Context.Orders
                          on employee.EmployeeId equals order.EmployeeId
                          group employee by new { employee.EmployeeId, employee.LastName } into g
                          select new
                          {
                              EmployeeId = g.Key.EmployeeId,
                              LastName = g.Key.LastName,
                              Count = g.Count()
                          }).ToList();
            var query = (from employee in Context.Employees
                         join order in Context.Orders
                         on employee.EmployeeId equals order.EmployeeId
                         where order.RequiredDate < order.ShippedDate
                         group employee by new { employee.EmployeeId, employee.LastName } into g
                         select new
                         {
                             EmployeeId = g.Key.EmployeeId,
                             LastName = g.Key.LastName,
                             LateOrders = g.Count()
                         }).ToList();
            var res = from all in orders
                      join q in query
                      on all.EmployeeId equals q.EmployeeId into g
                      from gc in g.DefaultIfEmpty()
                      orderby gc.LateOrders descending
                      select new LateEmployeeModel
                      {
                          EmployeeId = gc.EmployeeId,
                          LastName = gc.LastName,
                          AllOrders = all.Count,
                          LateOrders = Math.Round((decimal)gc.LateOrders / (decimal)all.Count, 2)
                      };
            return res.ToList();
        }

    }
}
