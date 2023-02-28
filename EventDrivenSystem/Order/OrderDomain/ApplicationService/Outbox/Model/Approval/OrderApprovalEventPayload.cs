using System.Collections.Generic;
using Rosered11.Common.Domain.Entity;

namespace Rosered11.Order.Application.Service.Outbox.Model.Approval;

public record OrderApprovalEventPayload(
    string OrderId,
    string RestaurantId,
    decimal Price,
    ZoneDateTime CreatedAt,
    string RestaurantOrderStatus,
    IEnumerable<OrderApprovalEventProduct> Products
);