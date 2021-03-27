using NORTHWND.Core.Abstractions;
using NORTHWND.Core.Abstractions.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace NORTHWND.BLL.Operations
{
    public class ProductOperations : IProductOperations
    {
        private readonly IRepositoryManager _repositories;
        public ProductOperations(IRepositoryManager repositories)
        {
            _repositories = repositories;
        }
    }
}
