using NORTHWND.Core.Abstractions.Repositories;
using NORTHWND.Core.BusinessModels;
using NORTHWND.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NORTHWND.DAL.Repositories
{
    public class EmployeeRepository:RepositoryBase<Employee>,IEmployeeRepository
    {
        public EmployeeRepository(NORTHWNDContext context):base(context){}


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
                Title=model.Title,
                TitleOfCourtesy=model.TitleOfCourtesy
            });
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
