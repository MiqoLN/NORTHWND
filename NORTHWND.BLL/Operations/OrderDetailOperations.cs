using NORTHWND.Core.Abstractions;
using NORTHWND.Core.Abstractions.Operations;
using NORTHWND.Core.BusinessModels;
using NORTHWND.Core.Entities;
using NORTHWND.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NORTHWND.BLL.Operations
{
    public class OrderDetailOperations : IOrderDetailOperations
    {
        private readonly IRepositoryManager _repositories;
        public OrderDetailOperations(IRepositoryManager repository)
        {
            _repositories = repository;
        }

        public void Add(OrderDetailRegisterModel model)
        {
            var product = _repositories.Products.GetSingle(u => u.ProductId == model.ProductId);
            if (product == null)
                throw new LogicException("Wrong productId");
            var order = _repositories.Orders.GetSingle(u => u.OrderId == model.OrderId);
            if (order == null)
                throw new LogicException("Wrong OrderId");
            _repositories.OrderDetails.Add(new OrderDetail
            {
                Discount = model.Discount,
                OrderId = model.OrderId,
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                UnitPrice = model.UnitPrice
            });

        }
    }
}
