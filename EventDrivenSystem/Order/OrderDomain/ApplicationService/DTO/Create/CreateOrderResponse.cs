using System;
using Rosered11.Common.Domain.ValueObject;

namespace Rosered11.Order.Application.Service.DTO.Create
{
    public record CreateOrderResponse(
        Guid OrderTrackingId,
        OrderStatus OrderStatus,
        string Message
    );
}