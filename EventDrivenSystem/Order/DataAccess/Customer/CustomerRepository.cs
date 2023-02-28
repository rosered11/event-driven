using Rosered11.Order.Application.Service.Ports.Output.Repository;

namespace Rosered11.Order.DataAccess.Customer;

public class CustomerRepository : ICustomerRepository
{
    public virtual Domain.Core.Entity.Customer findCustomer(Guid customerId) {
        // Mock
        return new Domain.Core.Entity.Customer(new(customerId));
    }

    public virtual Domain.Core.Entity.Customer save(Domain.Core.Entity.Customer customer) {
        // Mock
        return new Domain.Core.Entity.Customer(new(customer.ID.GetValue()));
    }
}