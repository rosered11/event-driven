using Rosered11.Infrastructure.Outbox;
using Rosered11.Infrastructure.Saga;
using Rosered11.Order.Application.Service.Outbox.Model.Approval;

namespace Rosered11.Order.Application.Service.Ports.Output.Repository;

public interface IApprovalOutboxRepository {

    OrderApprovalOutboxMessage save(OrderApprovalOutboxMessage orderApprovalOutboxMessage);

    List<OrderApprovalOutboxMessage>? findByTypeAndOutboxStatusAndSagaStatus(string type,
                                                                                     OutboxStatus outboxStatus,
                                                                                     IEnumerable<SagaStatus> sagaStatus);
    OrderApprovalOutboxMessage? findByTypeAndSagaIdAndSagaStatus(string type,
                                                                         Guid sagaId,
                                                                         IEnumerable<SagaStatus> sagaStatus);
    void deleteByTypeAndOutboxStatusAndSagaStatus(string type,
                                                  OutboxStatus outboxStatus,
                                                  IEnumerable<SagaStatus> sagaStatus);
}