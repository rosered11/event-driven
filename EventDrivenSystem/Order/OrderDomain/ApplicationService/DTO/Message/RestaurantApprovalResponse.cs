using System.Collections.Generic;
using Rosered11.Common.Domain.ValueObject;

namespace Rosered11.Order.Application.Service.DTO.Message
{
    public record RestaurantApprovalResponse(
        string ID,
        string SagaId,
        string OrderId,
        string RestaurantId,
        long CreatedAt,
        OrderApprovalStatus OrderApprovalStatus,
        IEnumerable<string> FailureMessages
    );
}