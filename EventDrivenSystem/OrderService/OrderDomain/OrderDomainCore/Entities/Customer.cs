using Common.CommonDomain.Entities;
using Common.CommonDomain.ValueObject;

namespace Rosered11.OrderService.Domain.Entities
{
    public class Customer : AggregateRoot<CustomerId>
    {
        public Customer(CustomerId id) : base(id)
        {
        }
    }
}