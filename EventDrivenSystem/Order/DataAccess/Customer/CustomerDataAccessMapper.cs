using Rosered11.Common.Domain.ValueObject;
using Rosered11.Order.DataAccess.Customer.Entity;

namespace Rosered11.Order.DataAccess.Customer;

public class CustomerDataAccessMapper
{
    public Domain.Core.Entity.Customer customerEntityToCustomer(CustomerEntity customerEntity) {
        return new Domain.Core.Entity.Customer(new CustomerId(customerEntity.Id));
    }

    public CustomerEntity customerToCustomerEntity(Domain.Core.Entity.Customer customer) {
        return new CustomerEntity{
            Id = customer.ID.GetValue(),
            Username = customer.UserName,
            FirstName = customer.FirstName,
            LastName = customer.LastName
        };
    }
}