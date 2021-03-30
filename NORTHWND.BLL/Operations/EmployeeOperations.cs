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
    public class EmployeeOperations : IEmployeeOperations
    {
        private readonly IRepositoryManager _repositories;
        public EmployeeOperations(IRepositoryManager manager)
        {
            _repositories = manager;
        }

        public void AddEmployee(EmployeeRegisterModel model)
        {
            _repositories.Employees.AddEmployee(model);
            _repositories.SaveChanges();
        }

        public void EditEmployee(EmployeeChangeModel model)
        {
            var employee = _repositories.Employees.Get(model.EmployeeId);
            if (employee == null)
                throw new LogicException("There is no Employee with that Id");
            employee.BirthDate = model.BirthDate == null ? employee.BirthDate : model.BirthDate;
            employee.City = string.IsNullOrEmpty(model.City) ? employee.City : model.City;
            employee.Country = string.IsNullOrEmpty(model.Country) ? employee.Country : model.Country;
            employee.Region = string.IsNullOrEmpty(model.Region) ? employee.Region : model.Region;
            employee.Title = string.IsNullOrEmpty(model.Title) ? employee.Title : model.Title;
            employee.TitleOfCourtesy = string.IsNullOrEmpty(model.TitleOfCourtesy) ? employee.TitleOfCourtesy : model.TitleOfCourtesy;
            employee.FirstName = string.IsNullOrEmpty(model.FirstName) ? employee.FirstName : model.FirstName;
            employee.LastName = string.IsNullOrEmpty(model.LastName) ? employee.LastName : model.LastName;
            _repositories.Employees.Update(employee);
            _repositories.SaveChanges();
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
