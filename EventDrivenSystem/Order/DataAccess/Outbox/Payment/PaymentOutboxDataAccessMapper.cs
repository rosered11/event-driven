using Rosered11.Common.Domain.ValueObject;
using Rosered11.Infrastructure.Outbox;
using Rosered11.Infrastructure.Saga;
using Rosered11.Order.Application.Service.Outbox.Model.Payment;
using Rosered11.Order.DataAccess.Outbox.Payment.Entity;

namespace Rosered11.Order.DataAccess.Outbox.Payment;

public class PaymentOutboxDataAccessMapper
{
    public PaymentOutboxEntity orderPaymentOutboxMessageToOutboxEntity(OrderPaymentOutboxMessage
                                                                               orderPaymentOutboxMessage) {
        return new PaymentOutboxEntity{
            Id = orderPaymentOutboxMessage.ID,
            SagaId = orderPaymentOutboxMessage.SagaId,
            CreatedAt = orderPaymentOutboxMessage.CreatedAt.GetDateTime,
            Type = orderPaymentOutboxMessage.Type,
            Payload = orderPaymentOutboxMessage.Payload,
            OrderStatus = orderPaymentOutboxMessage.OrderStatus.ToString(),
            SagaStatus = orderPaymentOutboxMessage.SagaStatus.ToString(),
            OutboxStatus = orderPaymentOutboxMessage.OutboxStatus.ToString(),
            Version = orderPaymentOutboxMessage.Version
        };
    }

    public OrderPaymentOutboxMessage paymentOutboxEntityToOrderPaymentOutboxMessage(PaymentOutboxEntity
                                                                               paymentOutboxEntity) {
        return new OrderPaymentOutboxMessage{
            ID = paymentOutboxEntity.Id,
            SagaId = paymentOutboxEntity.SagaId,
            CreatedAt = new(paymentOutboxEntity.CreatedAt),
            Type = paymentOutboxEntity.Type,
            Payload = paymentOutboxEntity.Payload,
            OrderStatus = Enum.Parse<OrderStatus>(paymentOutboxEntity.OrderStatus),
            SagaStatus = Enum.Parse<SagaStatus>(paymentOutboxEntity.SagaStatus),
            OutboxStatus = Enum.Parse<OutboxStatus>(paymentOutboxEntity.OutboxStatus),
            Version = paymentOutboxEntity.Version
        };
    }
}