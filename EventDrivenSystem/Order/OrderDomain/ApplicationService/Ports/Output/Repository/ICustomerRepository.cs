using Rosered11.Order.Domain.Core.Entity;

namespace Rosered11.Order.Application.Service.Ports.Output.Repository;

public interface ICustomerRepository {

    Customer? findCustomer(Guid customerId);

    Customer save(Customer customer);
}