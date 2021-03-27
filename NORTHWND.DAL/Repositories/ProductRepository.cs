using NORTHWND.Core.Abstractions.Repositories;
using NORTHWND.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NORTHWND.DAL.Repositories
{
    public class ProductRepository:RepositoryBase<Product>,IProductRepository
    {
        public ProductRepository(NORTHWNDContext context):base(context)
        {

        }
    }
}
