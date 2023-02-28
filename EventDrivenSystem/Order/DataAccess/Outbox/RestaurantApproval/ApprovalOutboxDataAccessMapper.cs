using Rosered11.Common.Domain.ValueObject;
using Rosered11.Infrastructure.Outbox;
using Rosered11.Infrastructure.Saga;
using Rosered11.Order.Application.Service.Outbox.Model.Approval;
using Rosered11.Order.DataAccess.Outbox.RestaurantApproval.Entity;

namespace Rosered11.Order.DataAccess.Outbox.RestaurantApproval;

public class ApprovalOutboxDataAccessMapper
{
    public ApprovalOutboxEntity orderCreatedOutboxMessageToOutboxEntity(OrderApprovalOutboxMessage
                                                                                orderApprovalOutboxMessage) {
        return new ApprovalOutboxEntity{
            Id = orderApprovalOutboxMessage.Id,
            SagaId = orderApprovalOutboxMessage.SagaId,
            CreatedAt = orderApprovalOutboxMessage.CreatedAt.GetDateTime,
            Type = orderApprovalOutboxMessage.Type,
            Payload = orderApprovalOutboxMessage.Payload,
            OrderStatus = orderApprovalOutboxMessage.OrderStatus.ToString(),
            SagaStatus = orderApprovalOutboxMessage.SagaStatus.ToString(),
            OutboxStatus = orderApprovalOutboxMessage.OutboxStatus.ToString(),
            Version = orderApprovalOutboxMessage.Version
        };
    }

    public OrderApprovalOutboxMessage approvalOutboxEntityToOrderApprovalOutboxMessage(ApprovalOutboxEntity
                                                                                               approvalOutboxEntity) {
        return new OrderApprovalOutboxMessage{
            Id = approvalOutboxEntity.Id,
            SagaId = approvalOutboxEntity.SagaId,
            CreatedAt = new(approvalOutboxEntity.CreatedAt),
            Type = approvalOutboxEntity.Type,
            Payload = approvalOutboxEntity.Payload,
            OrderStatus = Enum.Parse<OrderStatus>(approvalOutboxEntity.OrderStatus),
            SagaStatus = Enum.Parse<SagaStatus>(approvalOutboxEntity.SagaStatus),
            OutboxStatus = Enum.Parse<OutboxStatus>(approvalOutboxEntity.OutboxStatus),
            Version = approvalOutboxEntity.Version
        };
    }
}