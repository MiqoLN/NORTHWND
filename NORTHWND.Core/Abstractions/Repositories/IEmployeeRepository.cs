using NORTHWND.Core.BusinessModels;
using NORTHWND.Core.Entities;
using System.Collections.Generic;

namespace NORTHWND.Core.Abstractions.Repositories
{
    public interface IEmployeeRepository:IRepositoryBase<Employee>
    {
        public void AddEmployee(EmployeeRegisterModel model);
        public IEnumerable<EmployeeViewModel> Get(EmployeeViewModel model);
        public IEnumerable<LateEmployeeModel> GetLateEmployees();
    }
}
