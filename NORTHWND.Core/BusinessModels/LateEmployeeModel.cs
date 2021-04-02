namespace NORTHWND.Core.BusinessModels
{
    public class LateEmployeeModel
    {
        public int EmployeeId { get; set; }
        public string LastName { get; set; }
        public int AllOrders { get; set; }
        public decimal LateOrders { get; set; }
    }
}
