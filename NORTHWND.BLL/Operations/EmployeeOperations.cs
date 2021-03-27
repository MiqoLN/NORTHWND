using NORTHWND.Core.Abstractions;
using NORTHWND.Core.Abstractions.Operations;
using NORTHWND.Core.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NORTHWND.BLL.Operations
{
    public class EmployeeOperations : IEmployeeOperations
    {
        private readonly IRepositoryManager _repositories;
        public EmployeeOperations(IRepositoryManager manager)
        {
            _repositories = manager;
        }

        public IEnumerable<EmployeeViewModel> GetAll()
        {
            var query = _repositories.Employees.GetAll();
            var res = (from e in query
                      select new EmployeeViewModel
                      {
                          BirthDate = e.BirthDate,
                          City = e.City,
                          Country = e.Country,
                          EmployeeId = e.EmployeeId,
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
            return _repositories.Employees.GetLateEmployees();
        }
    }
}
