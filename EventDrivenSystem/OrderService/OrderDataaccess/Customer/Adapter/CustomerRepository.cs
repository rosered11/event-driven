using Rosered11.OrderService.DataAccess.Entity;
using Rosered11.OrderService.DataAccess.Mapper;
using Rosered11.OrderService.Domain.Entities;
using Rosered11.OrderService.Domain.Ports.Output.Repository;

namespace Rosered11.OrderService.DataAccess.Adapter
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly Repository.CustomerRepository _customerRepository;
        private readonly CustomerDataAccessMapper _customerDataAccessMapper;

        public CustomerRepository(Repository.CustomerRepository customerRepository, CustomerDataAccessMapper customerDataAccessMapper)
        {
            _customerRepository = customerRepository;
            _customerDataAccessMapper = customerDataAccessMapper;
        }
        public Customer FindCustomer(Guid customerId)
        {
            CustomerEntity? customer = _customerRepository.Find(customerId);
            if (customer == null)
                throw new System.Exception("");
            return _customerDataAccessMapper.CustomerEntityToCustomer(customer);
        }
    }
}