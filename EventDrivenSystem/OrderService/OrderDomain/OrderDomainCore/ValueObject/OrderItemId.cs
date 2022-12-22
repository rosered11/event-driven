using Common.CommonDomain.ValueObject;

namespace Rosered11.OrderService.Domain.ValueObject
{
    public class OrderItemId : BaseId<long>
    {
        public OrderItemId(long value) : base(value) {}
    }
}