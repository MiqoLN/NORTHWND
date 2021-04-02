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
    public class EmployeeOperations : IEmployeeOperations
    {
        private readonly IRepositoryManager _repositories;
        private readonly ILogger<EmployeeOperations> _logger;
        public EmployeeOperations(IRepositoryManager repositories,ILogger<EmployeeOperations> logger)
        {
            _logger = logger;
            _repositories = repositories;
        }

        public void AddEmployee(EmployeeRegisterModel model)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            _repositories.Employees.AddEmployee(model);
            _repositories.SaveChanges();
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
        }

        public void EditEmployee(EmployeeChangeModel model)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
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
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
        }

        public IEnumerable<EmployeeViewModel> Get()
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
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
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
            return res;
        }

        public EmployeeViewModel Get(int id)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var employee = _repositories.Employees.Get(id);
            if (employee == null)
                throw new LogicException("There is no employee with that Id");
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
            return new EmployeeViewModel
            {
                BirthDate = employee.BirthDate,
                City = employee.City,
                Country = employee.Country,
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Region = employee.Region,
                Title = employee.Title,
                TitleOfCourtesy = employee.TitleOfCourtesy

            };
        }

        public IEnumerable<EmployeeViewModel> Get(EmployeeViewModel model)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var res= _repositories.Employees.Get(model);
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
            return res;
        }

        public IEnumerable<LateEmployeeModel> GetLateEmployees()
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} started");
            var res =_repositories.Employees.GetLateEmployees();
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} finished");
            return res;
        }
    }
}
