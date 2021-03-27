using NORTHWND.Core.BusinessModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NORTHWND.Core.Abstractions.Operations
{
    public interface IEmployeeOperations
    {
        public IEnumerable<EmployeeViewModel> GetAll();
        public IEnumerable<LateEmployeeModel> GetLateEmployees();
    }
}
