using NORTHWND.Core.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace NORTHWND.Core.Abstractions
{
    public interface IRepositoryManager
    {
        public IUserRepository Users { get; }
        public IOrderRepository Orders { get; }
        public ICustomerRepository Customers { get; }
        public IEmployeeRepository Employees { get; }
        public IOrderDetailRepository OrderDetails { get; }
        public IProductRepository Products { get; }
        public ISupplierRepository Suppliers { get; }
        public ICategoryRepository Categories { get; }
        public IDatabaseTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        void SaveChanges();
        Task SaveChangesAsync();

    }
}
