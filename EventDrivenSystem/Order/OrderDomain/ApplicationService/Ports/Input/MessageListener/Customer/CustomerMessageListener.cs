namespace Rosered11.Order.Application.Service.Ports.Input.Message.Listener.Customer;

public class CustomerMessageListenerImpl
{

    // private final CustomerRepository customerRepository;
    // private final OrderDataMapper orderDataMapper;

    // public CustomerMessageListenerImpl(CustomerRepository customerRepository, OrderDataMapper orderDataMapper) {
    //     this.customerRepository = customerRepository;
    //     this.orderDataMapper = orderDataMapper;
    // }

    // @Override
    // public void customerCreated(CustomerModel customerModel) {
    //     Customer customer = customerRepository.save(orderDataMapper.customerModelToCustomer(customerModel));
    //     if (customer == null) {
    //         log.error("Customer could not be created in order database with id: {}", customerModel.getId());
    //         throw new OrderDomainException("Customer could not be created in order database with id " +
    //                 customerModel.getId());
    //     }
    //     log.info("Customer is created in order database with id: {}", customer.getId());
    // }
}