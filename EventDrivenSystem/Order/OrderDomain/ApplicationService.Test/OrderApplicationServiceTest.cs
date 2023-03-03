using System.Text.Json;
using Microsoft.Extensions.Logging;
using Moq;
using Rosered11.Common.Domain.Entity;
using Rosered11.Common.Domain.ValueObject;
using Rosered11.Infrastructure.Outbox;
using Rosered11.Infrastructure.Saga;
using Rosered11.Infrastructure.Saga.Order;
using Rosered11.Order.Application.Service.DTO.Create;
using Rosered11.Order.Application.Service.Mapper;
using Rosered11.Order.Application.Service.Outbox.Model.Payment;
using Rosered11.Order.Application.Service.Outbox.Scheduler;
using Rosered11.Order.Application.Service.Ports;
using Rosered11.Order.Application.Service.Ports.Input.Service;
using Rosered11.Order.Application.Service.Ports.Output.Repository;
using Rosered11.Order.Domain.Core;
using Rosered11.Order.Domain.Core.Entity;
using Rosered11.Order.Domain.Core.Exception;

namespace Rosered11.Order.Application.Service.Test;

public class OrderApplicationServiceTest
{
    private OrderApplicationService _orderApplicationService;
    private OrderDataMapper orderDataMapper;
    private CreateOrderCommand createOrderCommand;
    private CreateOrderCommand createOrderCommandWrongPrice;
    private CreateOrderCommand createOrderCommandWrongProductPrice;
    private static Guid CUSTOMER_ID = new("d215b5f8-0249-4dc5-89a3-51fd148cfb41");
    private static Guid RESTAURANT_ID = new("d215b5f8-0249-4dc5-89a3-51fd148cfb45");
    private static Guid PRODUCT_ID = new("d215b5f8-0249-4dc5-89a3-51fd148cfb48");
    private static Guid ORDER_ID = new("15a497c1-0f4b-4eff-b9f4-c402c8c07afb");
    private static Guid SAGA_ID = new("15a497c1-0f4b-4eff-b9f4-c402c8c07afa");
    private const decimal PRICE = 200.00m;

    private IRestaurantRepository GetRestaurantRepository(IRestaurantRepository? repository = null){
        
        if (repository == null)
        {
            Restaurant restaurantResponse = new Restaurant( new(createOrderCommand.RestaurantId)){
                Products = new List<Product>{
                    new(new(PRODUCT_ID), "product-1", new(50.00m)),
                    new(new(PRODUCT_ID), "product-2", new(50.00m))
                },
                Active = true
            };
            Mock<IRestaurantRepository> mockRestaurantRepo = new();
                mockRestaurantRepo.Setup(x => x.findRestaurantInformation(orderDataMapper.createOrderCommandToRestaurant(createOrderCommand)))
                    .Returns(restaurantResponse);
            return mockRestaurantRepo.Object;
        }
        return repository;
    }
    private OrderApplicationService CreateOrderApplicationService(IRestaurantRepository? repository = null)
    {
        Customer customer = new (new (CUSTOMER_ID));
        orderDataMapper = new();
        var order = orderDataMapper.createOrderCommandToOrder(createOrderCommand);
        order.SetId(new OrderId(ORDER_ID));
        
        Mock<ICustomerRepository> mockCustomerRepo = new();
        mockCustomerRepo.Setup(x => x.findCustomer(CUSTOMER_ID))
            .Returns(customer);

        Mock<IOrderRepository> mockOrderRepo = new();
        mockOrderRepo.Setup(x => x.save(It.IsAny<Domain.Core.Entity.Order>())).Returns(order);

        Mock<IPaymentOutboxRepository> mockPaymentOutboxRepo = new();
        mockPaymentOutboxRepo.Setup(x => x.save(It.IsAny<OrderPaymentOutboxMessage>())).Returns(getOrderPaymentOutboxMessage());
        
        return new(
            new Ports.OrderCreateCommandHandler(
                Mock.Of<ILogger<OrderCreateCommandHandler>>()
                , new(Mock.Of<ILogger<OrderCreateHelper>>(), new(Mock.Of<ILogger<OrderDomainService>>()), mockOrderRepo.Object, mockCustomerRepo.Object, GetRestaurantRepository(repository), orderDataMapper)
                , orderDataMapper
                , new(Mock.Of<ILogger<PaymentOutboxHelper>>(), mockPaymentOutboxRepo.Object)
                , new(Mock.Of<ILogger<OrderSagaHelper>>(), mockOrderRepo.Object)),
            new (Mock.Of<ILogger<OrderTrackCommandHandler>>(), orderDataMapper, mockOrderRepo.Object));
    }

    public OrderApplicationServiceTest()
    {
        createOrderCommand = new (CUSTOMER_ID, RESTAURANT_ID, PRICE
        , new List<DTO.Create.OrderItem>{
            new(PRODUCT_ID, 1, 50.00m, 50.00m),
            new(PRODUCT_ID, 3, 50.00m, 150.00m)
        }
        , new OrderAddress("street_1", "1000AB", "Paris"));
        
        createOrderCommandWrongPrice = new(CUSTOMER_ID, RESTAURANT_ID, 250.00m
        , new List<DTO.Create.OrderItem>{
            new(PRODUCT_ID, 1, 50.00m, 50.00m),
            new(PRODUCT_ID, 3, 50.00m, 150.00m)
        }
        , new OrderAddress("street_1", "1000AB", "Paris"));
        
        createOrderCommandWrongProductPrice = new(CUSTOMER_ID, RESTAURANT_ID, 210.00m
        , new List<DTO.Create.OrderItem>{
            new(PRODUCT_ID, 1, 60.00m, 60.00m),
            new(PRODUCT_ID, 3, 50.00m, 150.00m)
        }
        , new OrderAddress("street_1", "1000AB", "Paris"));

        _orderApplicationService = CreateOrderApplicationService();
    }

    private OrderPaymentOutboxMessage getOrderPaymentOutboxMessage() {
        OrderPaymentEventPayload orderPaymentEventPayload = 
            new(ORDER_ID.ToString(), CUSTOMER_ID.ToString(), PRICE, ZoneDateTime.UtcNow(), nameof(PaymentOrderStatus.PENDING));

        return new (){
            ID = Guid.NewGuid(),
            SagaId = SAGA_ID,
            CreatedAt = ZoneDateTime.UtcNow(),
            Type = SagaConstants.ORDER_SAGA_NAME,
            Payload = createPayload(orderPaymentEventPayload),
            OrderStatus = OrderStatus.PENDING,
            SagaStatus = SagaStatus.STARTED,
            OutboxStatus = OutboxStatus.STARTED,
            Version = 0
        };
    }

    private String createPayload(OrderPaymentEventPayload orderPaymentEventPayload) {
        try {
            return  JsonSerializer.Serialize(orderPaymentEventPayload);
        } catch (Exception) {
            throw new OrderDomainException("Cannot create OrderPaymentEventPayload object!");
        }
    }

    [Fact]
    public void When_Should()
    {
        CreateOrderResponse createOrderResponse = _orderApplicationService.createOrder(createOrderCommand);
        Assert.Equal(OrderStatus.PENDING, createOrderResponse.OrderStatus);
        Assert.Equal("Order created successfully", createOrderResponse.Message);
        Assert.NotNull(createOrderResponse.OrderTrackingId);
    }

    [Fact]
    public void testCreateOrderWithWrongTotalPrice() {
       OrderDomainException orderDomainException = Assert.Throws<OrderDomainException>(
                () => _orderApplicationService.createOrder(createOrderCommandWrongPrice));
       Assert.Equal("Total price: 250.00 is not equal to Order items total: 200.00!", orderDomainException.Message);
    }

    [Fact]
    public void testCreateOrderWithWrongProductPrice() {
       OrderDomainException orderDomainException = Assert.Throws<OrderDomainException>(
                () => _orderApplicationService.createOrder(createOrderCommandWrongProductPrice));
       Assert.Equal("Order item price: 60.00 is not valid for product " + PRODUCT_ID, orderDomainException.Message);
    }

    [Fact]
    public void testCreateOrderWithPassiveRestaurant() {
        Restaurant restaurantResponse = new Restaurant(new(createOrderCommand.RestaurantId)){
            Products = new List<Product>{
                new(new(PRODUCT_ID), "product-1", new(50.00m)),
                new(new(PRODUCT_ID), "product-2", new(50.00m))
            },
            Active = false
        };
        
        Mock<IRestaurantRepository> mockRepo = new();
        mockRepo.Setup(x => x.findRestaurantInformation(orderDataMapper.createOrderCommandToRestaurant(createOrderCommand)))
            .Returns(restaurantResponse);
       
       _orderApplicationService = CreateOrderApplicationService(mockRepo.Object);
       
       OrderDomainException orderDomainException = Assert.Throws<OrderDomainException>(
               () => _orderApplicationService.createOrder(createOrderCommand));
       Assert.Equal("Restaurant with id " + RESTAURANT_ID + " is currently not active!", orderDomainException.Message);
    }
}