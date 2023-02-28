using System.Text.Json;
using Microsoft.Extensions.Logging;
using Rosered11.Common.Domain.ValueObject;
using Rosered11.Infrastructure.Outbox;
using Rosered11.Infrastructure.Saga;
using Rosered11.Infrastructure.Saga.Order;
using Rosered11.Order.Application.Service.Outbox.Model.Payment;
using Rosered11.Order.Application.Service.Ports.Output.Repository;
using Rosered11.Order.Domain.Core.Exception;

namespace Rosered11.Order.Application.Service.Outbox.Scheduler;

public class PaymentOutboxHelper
{
    private readonly ILogger<PaymentOutboxHelper> _logger;
    private readonly IPaymentOutboxRepository paymentOutboxRepository;
    // private readonly ObjectMapper objectMapper;

    public PaymentOutboxHelper(ILogger<PaymentOutboxHelper> logger, IPaymentOutboxRepository paymentOutboxRepository) {
        _logger = logger;
        this.paymentOutboxRepository = paymentOutboxRepository;
        // this.objectMapper = objectMapper;
    }

    // @Transactional(readOnly = true)
    public List<OrderPaymentOutboxMessage>? getPaymentOutboxMessageByOutboxStatusAndSagaStatus(
            OutboxStatus outboxStatus, IEnumerable<SagaStatus> sagaStatus) {
        return paymentOutboxRepository.findByTypeAndOutboxStatusAndSagaStatus(SagaConstants.ORDER_SAGA_NAME,
                outboxStatus,
                sagaStatus);
    }

    // @Transactional(readOnly = true)
    public OrderPaymentOutboxMessage? getPaymentOutboxMessageBySagaIdAndSagaStatus(Guid sagaId,
                                                                                            IEnumerable<SagaStatus> sagaStatus) {
        return paymentOutboxRepository.findByTypeAndSagaIdAndSagaStatus(SagaConstants.ORDER_SAGA_NAME, sagaId, sagaStatus);
    }

    // @Transactional
    public void save(OrderPaymentOutboxMessage orderPaymentOutboxMessage) {
       OrderPaymentOutboxMessage response = paymentOutboxRepository.save(orderPaymentOutboxMessage);
       if (response == null) {
           _logger.LogError("Could not save OrderPaymentOutboxMessage with outbox id: {}", orderPaymentOutboxMessage.ID);
           throw new OrderDomainException("Could not save OrderPaymentOutboxMessage with outbox id: " +
                   orderPaymentOutboxMessage.ID);
       }
       _logger.LogInformation("OrderPaymentOutboxMessage saved with outbox id: {}", orderPaymentOutboxMessage.ID);
    }

    // @Transactional
    public void savePaymentOutboxMessage(OrderPaymentEventPayload paymentEventPayload,
                                         OrderStatus orderStatus,
                                         SagaStatus sagaStatus,
                                         OutboxStatus outboxStatus,
                                         Guid sagaId) {
        save(new OrderPaymentOutboxMessage{
            ID = Guid.NewGuid(),
            SagaId = sagaId,
            CreatedAt = paymentEventPayload.CreatedAt,
            Type = SagaConstants.ORDER_SAGA_NAME,
            Payload = createPayload(paymentEventPayload),
            OrderStatus = orderStatus,
            SagaStatus = sagaStatus,
            OutboxStatus = outboxStatus
        });
    }

    // @Transactional
    public void deletePaymentOutboxMessageByOutboxStatusAndSagaStatus(OutboxStatus outboxStatus,
                                                                      IEnumerable<SagaStatus> sagaStatus) {
        paymentOutboxRepository.deleteByTypeAndOutboxStatusAndSagaStatus(SagaConstants.ORDER_SAGA_NAME, outboxStatus, sagaStatus);
    }

    private String createPayload(OrderPaymentEventPayload paymentEventPayload) {
        try {
            return JsonSerializer.Serialize(paymentEventPayload);
        } catch (Exception e) {
            _logger.LogError("Could not create OrderPaymentEventPayload object for order id: {}",
                    paymentEventPayload.OrderId, e);
            throw new OrderDomainException("Could not create OrderPaymentEventPayload object for order id: " +
                    paymentEventPayload.OrderId, e);
        }
    }
}