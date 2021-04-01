using NORTHWND.Core.Abstractions.Repositories;
using System;
using System.Collections.Generic;
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
        void SaveChanges();
        Task SaveChangesAsync();

    }
}
