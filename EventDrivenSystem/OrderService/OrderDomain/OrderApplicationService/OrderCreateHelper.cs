using Microsoft.Extensions.Logging;
using Rosered11.OrderService.Domain.DTO.Create;
using Rosered11.OrderService.Domain.Entities;
using Rosered11.OrderService.Domain.Event;
using Rosered11.OrderService.Domain.Mapper;
using Rosered11.OrderService.Domain.Ports.Output.Repository;
using Rosered11.OrderService.Exception;

namespace Rosered11.OrderService.Domain
{
    public class OrderCreateHelper
    {
        private readonly ILogger<OrderCreateHelper> _logger;
        private readonly IOrderDomainService _orderDomainService;
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly OrderDataMapper _orderDataMapper;

        public OrderCreateHelper(ILogger<OrderCreateHelper> logger, IOrderDomainService orderDomainService, IOrderRepository orderRepository, ICustomerRepository customerRepository, IRestaurantRepository restaurantRepository, OrderDataMapper orderDataMapper)
        {
            _logger = logger;
            _orderDomainService = orderDomainService;
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _restaurantRepository = restaurantRepository;
            _orderDataMapper = orderDataMapper;
        }

        public OrderCreatedEvent PersistOrder(CreateOrderCommand createOrderCommand)
        {
            CheckCustomer(createOrderCommand.CustomerId);
            Restaurant restaurant = CheckRestaurant(createOrderCommand);
            Order order = _orderDataMapper.CreateOrderCommandToOrder(createOrderCommand);
            OrderCreatedEvent orderCreateEvent = _orderDomainService.ValidateAndInitiateOrder(order, restaurant);
            SaveOrder(order);
            _logger.LogInformation("Order is created with id: {Id}", orderCreateEvent.Order.Id.GetValue());
            return orderCreateEvent;
        }

        private Restaurant CheckRestaurant(CreateOrderCommand createOrderCommand)
        {
            Restaurant restaurant = _orderDataMapper.CreateOrderCommandToRestaurant(createOrderCommand);
            Restaurant optionalRestaurant = _restaurantRepository.FindRestaurantInformation(restaurant);
            if (optionalRestaurant == null)
            {
                _logger.LogWarning("Could not find restaurant with restaurant id: {restaurantId}", createOrderCommand.RestaurantId);
                throw new OrderDomainException($"Could not find restaurant with restaurant id: {createOrderCommand.RestaurantId}");
            }
            return optionalRestaurant;
        }

        private void CheckCustomer(Guid customerId)
        {
            Customer customer = _customerRepository.FindCustomer(customerId);
            if (customer == null)
            {
                _logger.LogWarning("Could not find customer with customer id: {customerId}", customerId);
                throw new OrderDomainException($"Could not find customer with customer id: {customerId}");
            }
        }

        private Order SaveOrder(Order order)
        {
            Order orderResult = _orderRepository.Save(order);
            if (order == null)
            {
                throw new OrderDomainException("Could not save order!");
            }
            _logger.LogInformation("Order is saved with id: {Id}", orderResult.Id.GetValue());
            return orderResult;
        }
    }
}