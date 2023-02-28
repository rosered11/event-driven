using System;

namespace Rosered11.Order.Application.Service.DTO.Create
{
    public record OrderItem(
        Guid ProductId,
        int Quantity,
        decimal Price,
        decimal SubTotal
    );
}