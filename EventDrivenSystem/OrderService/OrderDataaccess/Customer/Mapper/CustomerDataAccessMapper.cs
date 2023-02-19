using Common.CommonDomain.ValueObject;
using Rosered11.OrderService.DataAccess.Entity;
using Rosered11.OrderService.Domain.Entities;

namespace Rosered11.OrderService.DataAccess.Mapper
{
    public class CustomerDataAccessMapper
    {
        public Customer CustomerEntityToCustomer(CustomerEntity customerEntity)
        {
            return new Customer(new CustomerId(customerEntity.Id));
        }
    }
}