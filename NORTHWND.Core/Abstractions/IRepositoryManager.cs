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
        void SaveChanges();
        Task SaveChangesAsync();

    }
}
