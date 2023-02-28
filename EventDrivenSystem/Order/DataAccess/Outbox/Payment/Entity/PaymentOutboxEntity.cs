using Rosered11.Infrastructure.Saga;

namespace Rosered11.Order.DataAccess.Outbox.Payment.Entity;

public class PaymentOutboxEntity
{
    public Guid Id { get; set; }
    public Guid SagaId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ProcessedAt { get; set; }
    public string Type { get; set; }
    public string Payload { get; set; }
    public string SagaStatus { get; set; }
    public string OrderStatus { get; set; }
    public string OutboxStatus { get; set; }
    public int Version { get; set; }
}