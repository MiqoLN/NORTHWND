using NORTHWND.Core.Abstractions.Repositories;
using NORTHWND.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NORTHWND.DAL.Repositories
{
    public class SupplierRepository : RepositoryBase<Supplier>, ISupplierRepository
    {
        public SupplierRepository(NORTHWNDContext context) : base(context){}
    }
}
