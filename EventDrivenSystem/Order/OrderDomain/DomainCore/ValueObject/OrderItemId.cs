using Rosered11.Common.Domain.ValueObject;

namespace Rosered11.Order.Domain.Core.ValueObject
{
    public class OrderItemId : BaseId<long>
    {
        public OrderItemId(long value) : base(value)
        {
        }
    }
}