using System.Text.Json;
using Microsoft.Extensions.Logging;
using Rosered11.Common.Domain.ValueObject;
using Rosered11.Infrastructure.Outbox;
using Rosered11.Infrastructure.Saga;
using Rosered11.Infrastructure.Saga.Order;
using Rosered11.Order.Application.Service.Outbox.Model.Approval;
using Rosered11.Order.Application.Service.Ports.Output.Repository;
using Rosered11.Order.Domain.Core.Exception;

namespace Rosered11.Order.Application.Service.Outbox.Scheduler.Approval;

public class ApprovalOutboxHelper
{
    private readonly ILogger<ApprovalOutboxHelper> _logger;
    private readonly IApprovalOutboxRepository approvalOutboxRepository;
    // private readonly ObjectMapper objectMapper;

    public ApprovalOutboxHelper(IApprovalOutboxRepository approvalOutboxRepository) {
        this.approvalOutboxRepository = approvalOutboxRepository;
        // this.objectMapper = objectMapper;
    }

    // @Transactional(readOnly = true)
    public List<OrderApprovalOutboxMessage>
    getApprovalOutboxMessageByOutboxStatusAndSagaStatus(
            OutboxStatus outboxStatus, IEnumerable<SagaStatus> sagaStatus) {
        return approvalOutboxRepository.findByTypeAndOutboxStatusAndSagaStatus(SagaConstants.ORDER_SAGA_NAME,
                outboxStatus,
                sagaStatus);
    }

    // @Transactional(readOnly = true)
    public OrderApprovalOutboxMessage?
    getApprovalOutboxMessageBySagaIdAndSagaStatus(Guid sagaId, IEnumerable<SagaStatus> sagaStatus) {
        return approvalOutboxRepository.findByTypeAndSagaIdAndSagaStatus(SagaConstants.ORDER_SAGA_NAME, sagaId, sagaStatus);
    }

    // @Transactional
    public void save(OrderApprovalOutboxMessage orderApprovalOutboxMessage) {
        OrderApprovalOutboxMessage response = approvalOutboxRepository.save(orderApprovalOutboxMessage);
        if (response == null) {
            _logger.LogError("Could not save OrderApprovalOutboxMessage with outbox id: {}",
                    orderApprovalOutboxMessage.Id);
            throw new OrderDomainException("Could not save OrderApprovalOutboxMessage with outbox id: " +
                    orderApprovalOutboxMessage.Id);
        }
        _logger.LogInformation("OrderApprovalOutboxMessage saved with outbox id: {}", orderApprovalOutboxMessage.Id);
    }

    // @Transactional
    public void saveApprovalOutboxMessage(OrderApprovalEventPayload orderApprovalEventPayload,
                                          OrderStatus orderStatus,
                                          SagaStatus sagaStatus,
                                          OutboxStatus outboxStatus,
                                          Guid sagaId) {
        save(new OrderApprovalOutboxMessage{
            Id = Guid.NewGuid(),
            SagaId = sagaId,
            CreatedAt = orderApprovalEventPayload.CreatedAt,
            Type = SagaConstants.ORDER_SAGA_NAME,
            Payload = createPayload(orderApprovalEventPayload),
            OrderStatus = orderStatus,
            SagaStatus = sagaStatus,
            OutboxStatus = outboxStatus
        });
    }

    // @Transactional
    public void deleteApprovalOutboxMessageByOutboxStatusAndSagaStatus(OutboxStatus outboxStatus,
                                                                       IEnumerable<SagaStatus> sagaStatus) {
        approvalOutboxRepository.deleteByTypeAndOutboxStatusAndSagaStatus(SagaConstants.ORDER_SAGA_NAME, outboxStatus, sagaStatus);
    }

    private string createPayload(OrderApprovalEventPayload orderApprovalEventPayload) {
        try {
            return JsonSerializer.Serialize(orderApprovalEventPayload); //objectMapper.writeValueAsString(orderApprovalEventPayload);
        } catch (Exception e) {
            _logger.LogError("Could not create OrderApprovalEventPayload for order id: {}",
                    orderApprovalEventPayload.OrderId, e);
            throw new OrderDomainException("Could not create OrderApprovalEventPayload for order id: " +
                    orderApprovalEventPayload.OrderId, e);
        }
    }
}