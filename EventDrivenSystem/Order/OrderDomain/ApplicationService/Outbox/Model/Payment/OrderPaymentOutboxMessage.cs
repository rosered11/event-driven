using System;
using Rosered11.Common.Domain.Entity;
using Rosered11.Common.Domain.ValueObject;
using Rosered11.Infrastructure.Outbox;
using Rosered11.Infrastructure.Saga;

namespace Rosered11.Order.Application.Service.Outbox.Model.Payment
{
    public class OrderPaymentOutboxMessage
    {
        public Guid ID { get; set; }
        public Guid SagaId { get; set; }
        public ZoneDateTime CreatedAt { get; set; }
        public ZoneDateTime ProcessedAt { get; set; }
        public string Type { get; set; }
        public string Payload { get; set; }
        public SagaStatus SagaStatus { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public OutboxStatus OutboxStatus { get; set; }
        public int Version { get; set; }
    }
}