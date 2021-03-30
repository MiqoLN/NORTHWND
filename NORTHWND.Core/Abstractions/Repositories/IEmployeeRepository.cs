using NORTHWND.Core.BusinessModels;
using NORTHWND.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NORTHWND.Core.Abstractions.Repositories
{
    public interface IEmployeeRepository:IRepositoryBase<Employee>
    {
        public void AddEmployee(EmployeeRegisterModel model);
        public IEnumerable<LateEmployeeModel> GetLateEmployees();
    }
}
