using Microsoft.Extensions.Logging;
using Rosered11.Order.Application.Service.DTO.Create;
using Rosered11.Order.Application.Service.Mapper;
using Rosered11.Order.Application.Service.Ports.Output.Repository;
using Rosered11.Order.Domain.Core;
using Rosered11.Order.Domain.Core.Entity;
using Rosered11.Order.Domain.Core.Event;
using Rosered11.Order.Domain.Core.Exception;

namespace Rosered11.Order.Application.Service;

public class OrderCreateHelper
{
    private readonly ILogger<OrderCreateHelper> _logger;
    private readonly OrderDomainService orderDomainService;

    private readonly IOrderRepository orderRepository;

    private readonly ICustomerRepository customerRepository;

    private readonly IRestaurantRepository restaurantRepository;

    private readonly OrderDataMapper orderDataMapper;

    public OrderCreateHelper(ILogger<OrderCreateHelper> logger,
            OrderDomainService orderDomainService,
                             IOrderRepository orderRepository,
                             ICustomerRepository customerRepository,
                             IRestaurantRepository restaurantRepository,
                             OrderDataMapper orderDataMapper) {
                                _logger = logger;
        this.orderDomainService = orderDomainService;
        this.orderRepository = orderRepository;
        this.customerRepository = customerRepository;
        this.restaurantRepository = restaurantRepository;
        this.orderDataMapper = orderDataMapper;
    }

    // @Transactional
    public OrderCreatedEvent persistOrder(CreateOrderCommand createOrderCommand) {
        checkCustomer(createOrderCommand.CustomerId);
        Restaurant restaurant = checkRestaurant(createOrderCommand);
        Domain.Core.Entity.Order order = orderDataMapper.createOrderCommandToOrder(createOrderCommand);
        OrderCreatedEvent orderCreatedEvent = orderDomainService.validateAndInitiateOrder(order, restaurant);
        saveOrder(order);
        _logger.LogInformation("Order is created with id: {}", orderCreatedEvent.Order.ID.GetValue());
        return orderCreatedEvent;
    }

    private Restaurant checkRestaurant(CreateOrderCommand createOrderCommand) {
        Restaurant restaurant = orderDataMapper.createOrderCommandToRestaurant(createOrderCommand);
        Restaurant? optionalRestaurant = restaurantRepository.findRestaurantInformation(restaurant);
        if (optionalRestaurant == null) {
            _logger.LogWarning("Could not find restaurant with restaurant id: {}", createOrderCommand.RestaurantId);
            throw new OrderDomainException("Could not find restaurant with restaurant id: " +
                    createOrderCommand.RestaurantId);
        }
        return optionalRestaurant;
    }

    private void checkCustomer(Guid customerId) {
        Customer? customer = customerRepository.findCustomer(customerId);
        if (customer == null) {
            _logger.LogWarning("Could not find customer with customer id: {}", customerId);
            throw new OrderDomainException("Could not find customer with customer id: " + customer);
        }
    }

    private Domain.Core.Entity.Order saveOrder(Domain.Core.Entity.Order order) {
        Domain.Core.Entity.Order orderResult = orderRepository.save(order);
        if (orderResult == null) {
            _logger.LogError("Could not save order!");
            throw new OrderDomainException("Could not save order!");
        }
        _logger.LogInformation("Order is saved with id: {}", orderResult.ID.GetValue());
        return orderResult;
    }
}