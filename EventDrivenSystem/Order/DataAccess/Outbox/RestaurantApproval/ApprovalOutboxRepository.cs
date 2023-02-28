using Rosered11.Infrastructure.Outbox;
using Rosered11.Infrastructure.Saga;
using Rosered11.Order.Application.Service.Outbox.Model.Approval;
using Rosered11.Order.Application.Service.Ports.Output.Repository;

namespace Rosered11.Order.DataAccess.Outbox.RestaurantApproval;

public class ApprovalOutboxRepository : IApprovalOutboxRepository
{
    private readonly ApprovalOutboxDataAccessMapper _approvalOutboxMapper;

    public ApprovalOutboxRepository(ApprovalOutboxDataAccessMapper approvalOutboxMapper)
    {
        _approvalOutboxMapper = approvalOutboxMapper;
    }

    public virtual OrderApprovalOutboxMessage save(OrderApprovalOutboxMessage orderApprovalOutboxMessage) {
        return orderApprovalOutboxMessage;
    }

    public virtual List<OrderApprovalOutboxMessage> findByTypeAndOutboxStatusAndSagaStatus(string sagaType,
                                                                                       OutboxStatus outboxStatus,
                                                                       IEnumerable<SagaStatus> sagaStatus) {
        return new();
    }

    public virtual OrderApprovalOutboxMessage? findByTypeAndSagaIdAndSagaStatus(string type,
                                                                                 Guid sagaId,
                                                                                 IEnumerable<SagaStatus> sagaStatus) {
        return new();

    }

    public void deleteByTypeAndOutboxStatusAndSagaStatus(string type, OutboxStatus outboxStatus, IEnumerable<SagaStatus> sagaStatus)
    {
        
    }
}