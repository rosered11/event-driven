using System;
using System.Collections.Generic;
using Rosered11.Order.Domain.Core;

namespace Rosered11.Order.Application.Service.DTO.Create
{
    public record CreateOrderCommand(
        Guid CustomerId,
        Guid RestaurantId,
        decimal Price,
        IEnumerable<OrderItem> Items,
        OrderAddress Address);
}