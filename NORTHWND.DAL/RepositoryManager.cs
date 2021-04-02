using NORTHWND.Core.Abstractions;
using NORTHWND.Core.Abstractions.Repositories;
using NORTHWND.DAL.Repositories;
using System.Data;
using System.Threading.Tasks;

namespace NORTHWND.DAL
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly NORTHWNDContext _dbContext;
        public RepositoryManager(NORTHWNDContext context)
        {
            _dbContext = context;
        }
        private IUserRepository _users;
        public IUserRepository Users => _users ?? (_users = new UserRepository(_dbContext));
        private IOrderRepository _orders;
        public IOrderRepository Orders => _orders ?? (_orders = new OrderRepository(_dbContext));
        private ICustomerRepository _customers;
        public ICustomerRepository Customers => _customers ?? (_customers = new CustomerRepository(_dbContext));
        private IEmployeeRepository _employees;
        public IEmployeeRepository Employees => _employees ?? (_employees = new EmployeeRepository(_dbContext));
        private IOrderDetailRepository _orderDetails;
        public IOrderDetailRepository OrderDetails => _orderDetails ?? (_orderDetails = new OrderDetailRepository(_dbContext));
        private IProductRepository _products;
        public IProductRepository Products => _products ?? (_products = new ProductRepository(_dbContext));
        private ISupplierRepository _suppliers;
        public ISupplierRepository Suppliers => _suppliers ?? (_suppliers = new SupplierRepository(_dbContext));
        private ICategoryRepository _categories;
        public ICategoryRepository Categories => _categories ?? (_categories = new CategoryRepository(_dbContext));
        public IDatabaseTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return new DatabaseTransaction(_dbContext, isolationLevel);
        }
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
