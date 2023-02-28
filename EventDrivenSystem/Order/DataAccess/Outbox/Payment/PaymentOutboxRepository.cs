using Rosered11.Infrastructure.Outbox;
using Rosered11.Infrastructure.Saga;
using Rosered11.Order.Application.Service.Outbox.Model.Payment;
using Rosered11.Order.Application.Service.Ports.Output.Repository;

namespace Rosered11.Order.DataAccess.Outbox.Payment;

public class PaymentOutboxRepository : IPaymentOutboxRepository
{
    private readonly PaymentOutboxDataAccessMapper _paymentOutboxMapper;

    public PaymentOutboxRepository(PaymentOutboxDataAccessMapper paymentOutboxMapper)
    {
        _paymentOutboxMapper = paymentOutboxMapper;
    }

    public virtual OrderPaymentOutboxMessage save(OrderPaymentOutboxMessage orderPaymentOutboxMessage) {
        // Mock
        return orderPaymentOutboxMessage;
    }

    public virtual List<OrderPaymentOutboxMessage> findByTypeAndOutboxStatusAndSagaStatus(string sagaType,
                                                                                            OutboxStatus outboxStatus,
                                                                                            IEnumerable<SagaStatus>  sagaStatus) {
        return new();
    }

    public virtual OrderPaymentOutboxMessage? findByTypeAndSagaIdAndSagaStatus(string type,
                                                                                Guid sagaId,
                                                                                IEnumerable<SagaStatus> sagaStatus) {
        return new OrderPaymentOutboxMessage();
    }

    public virtual void deleteByTypeAndOutboxStatusAndSagaStatus(string type, OutboxStatus outboxStatus, IEnumerable<SagaStatus> sagaStatus) {
        
    }
}