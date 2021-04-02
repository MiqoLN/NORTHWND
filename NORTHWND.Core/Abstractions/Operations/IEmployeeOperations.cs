using NORTHWND.Core.BusinessModels;
using System.Collections.Generic;

namespace NORTHWND.Core.Abstractions.Operations
{
    public interface IEmployeeOperations
    {
        public void AddEmployee(EmployeeRegisterModel model);
        public void EditEmployee(EmployeeChangeModel model);
        public EmployeeViewModel Get(int id);
        public IEnumerable<EmployeeViewModel> Get();
        public IEnumerable<LateEmployeeModel> GetLateEmployees();
        public IEnumerable<EmployeeViewModel> Get(EmployeeViewModel model);
    }
}
