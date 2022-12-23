using Microsoft.Extensions.Logging;
using Rosered11.OrderService.Domain.DTO.Create;
using Rosered11.OrderService.Domain.Entities;
using Rosered11.OrderService.Domain.Event;
using Rosered11.OrderService.Domain.Mapper;
using Rosered11.OrderService.Domain.Ports.Output.Repository;
using Rosered11.OrderService.Exception;

namespace Rosered11.OrderService.Domain
{
    public class OrderCreateCommandHandler
    {
        private readonly ILogger<OrderCreateCommandHandler> _logger;
        private readonly OrderDomainService _orderDomainService;
        private readonly OrderRepository _orderRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly RestaurantRepository _restaurantRepository;
        private readonly OrderDataMapper _orderDataMapper;

        public OrderCreateCommandHandler(ILogger<OrderCreateCommandHandler> logger, OrderDomainService orderDomainService, OrderRepository orderRepository, CustomerRepository customerRepository, RestaurantRepository restaurantRepository, OrderDataMapper orderDataMapper)
        {
            _logger = logger;
            _orderDomainService = orderDomainService;
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _restaurantRepository = restaurantRepository;
            _orderDataMapper = orderDataMapper;
        }

        public CreateOrderResponse CreateOrder(CreateOrderCommand createOrderCommand)
        {
            CheckCustomer(createOrderCommand.CustomerId);
            Restaurant restaurant = CheckRestaurant(createOrderCommand);
            Order order = _orderDataMapper.CreateOrderCommandToOrder(createOrderCommand);
            OrderCreatedEvent orderCreateEvent = _orderDomainService.ValidateAndInitiateOrder(order, restaurant);
            Order orderResult = SaveOrder(order);
            _logger.LogInformation("Order is created with id: {id}", orderResult.Id.GetValue());
            return _orderDataMapper.OrderToCreateOrderResponse(orderResult);
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