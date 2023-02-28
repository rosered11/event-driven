using System.Collections.Generic;
using Rosered11.Common.Domain.ValueObject;

namespace Rosered11.Order.Application.Service.DTO.Message
{
    public record PaymentResponse(
        string ID,
        string SagaId,
        string OrderId,
        string PaymentId,
        string CustomerId,
        decimal Price,
        long CreatedAt,
        PaymentStatus PaymentStatus,
        IEnumerable<string> FailureMessages
    );
}