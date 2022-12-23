using Rosered11.OrderService.Domain.Entities;

namespace Rosered11.OrderService.Domain.Ports.Output.Repository
{
    public interface CustomerRepository
    {
        public Customer FindCustomer(Guid customerId);
    }
}