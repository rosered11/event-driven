using Rosered11.Common.Domain.Entity;

namespace Rosered11.Order.Application.Service.Outbox.Model.Payment
{
    public record OrderPaymentEventPayload(
        string OrderId,
        string CustomerId,
        decimal Price,
        ZoneDateTime CreatedAt,
        string PaymentOrderStatus
    );
}