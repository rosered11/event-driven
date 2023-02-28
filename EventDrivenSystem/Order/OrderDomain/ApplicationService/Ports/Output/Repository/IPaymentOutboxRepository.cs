using Rosered11.Infrastructure.Outbox;
using Rosered11.Infrastructure.Saga;
using Rosered11.Order.Application.Service.Outbox.Model.Payment;

namespace Rosered11.Order.Application.Service.Ports.Output.Repository;

public interface IPaymentOutboxRepository {

    OrderPaymentOutboxMessage save(OrderPaymentOutboxMessage orderPaymentOutboxMessage);

    List<OrderPaymentOutboxMessage>? findByTypeAndOutboxStatusAndSagaStatus(string type,
                                                                                     OutboxStatus outboxStatus,
                                                                                     IEnumerable<SagaStatus> sagaStatus);
    OrderPaymentOutboxMessage? findByTypeAndSagaIdAndSagaStatus(string type,
                                                                         Guid sagaId,
                                                                         IEnumerable<SagaStatus> sagaStatus);
    void deleteByTypeAndOutboxStatusAndSagaStatus(string type,
                                                  OutboxStatus outboxStatus,
                                                  IEnumerable<SagaStatus> sagaStatus);
}