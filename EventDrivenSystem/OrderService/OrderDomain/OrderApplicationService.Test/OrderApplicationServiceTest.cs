namespace Rosered11.OrderService.Domain.Test
{
    public class OrderApplicationServiceTest : IClassFixture<OrderTestConfigurationFixture>
    {
        private readonly OrderTestConfigurationFixture _fixture;
        private IOrderApplicationService _orderApplicationService;
        private CreateOrderCommand _createOrderCommand;
        private CreateOrderCommand _createOrderCommandWrongPrice;
        private CreateOrderCommand _createOrderCommandWrongProductPrice;
        private readonly Guid CUSTOMER_ID = new Guid("527a254b-2d80-4d5a-9cd2-56e0d0dea467");
        private readonly Guid RESTAURANT_ID = new Guid("6eecd52e-dbaf-4c34-8f19-50d751651e18");
        private readonly Guid PRODUCT_ID = new Guid("6ee66908-3fc2-4527-9c06-3ca36cdee7fd");
        private readonly Guid ORDER_ID = new Guid("3c32302d-092f-4688-8c90-dec45e7c8723");
        private readonly decimal PRICE = Decimal.Parse("200.00");

        public OrderApplicationServiceTest(OrderTestConfigurationFixture fixture)
        {
            _fixture = fixture;
            Init();
        }
        public void Init()
        {
            _createOrderCommand = CreateOrderCommand.NewBuilder()
                .SetCustomerId(CUSTOMER_ID)
                .SetRestaurantId(RESTAURANT_ID)
                .SetAddress(OrderAddress.NewBuilder()
                    .SetStreet("street_1")
                    .SetPostalCode("1000AB")
                    .SetCity("Paris")
                    .Build())
                .SetPrice(PRICE)
                .SetItems(new List<DTO.Create.OrderItem>(){ 
                    DTO.Create.OrderItem.NewBuilder()
                        .SetProductId(PRODUCT_ID)
                        .SetQuantity(1)
                        .SetPrice(50.00m)
                        .SetSubTotal(50.00m)
                        .Build(),
                    DTO.Create.OrderItem.NewBuilder()
                        .SetProductId(PRODUCT_ID)
                        .SetQuantity(3)
                        .SetPrice(50.00m)
                        .SetSubTotal(150.00m)
                        .Build()
                    })
                .Build();

            _createOrderCommandWrongPrice = CreateOrderCommand.NewBuilder()
                .SetCustomerId(CUSTOMER_ID)
                .SetRestaurantId(RESTAURANT_ID)
                .SetAddress(OrderAddress.NewBuilder()
                    .SetStreet("street_1")
                    .SetPostalCode("1000AB")
                    .SetCity("Paris")
                    .Build())
                .SetPrice(250.00m)
                .SetItems(new List<DTO.Create.OrderItem>(){ 
                    DTO.Create.OrderItem.NewBuilder()
                        .SetProductId(PRODUCT_ID)
                        .SetQuantity(1)
                        .SetPrice(50.00m)
                        .SetSubTotal(50.00m)
                        .Build(),
                    DTO.Create.OrderItem.NewBuilder()
                        .SetProductId(PRODUCT_ID)
                        .SetQuantity(3)
                        .SetPrice(50.00m)
                        .SetSubTotal(150.00m)
                        .Build()
                    })
                .Build();

            _createOrderCommandWrongProductPrice = CreateOrderCommand.NewBuilder()
                .SetCustomerId(CUSTOMER_ID)
                .SetRestaurantId(RESTAURANT_ID)
                .SetAddress(OrderAddress.NewBuilder()
                    .SetStreet("street_1")
                    .SetPostalCode("1000AB")
                    .SetCity("Paris")
                    .Build())
                .SetPrice(210m)
                .SetItems(new List<DTO.Create.OrderItem>(){ 
                    DTO.Create.OrderItem.NewBuilder()
                        .SetProductId(PRODUCT_ID)
                        .SetQuantity(1)
                        .SetPrice(60.00m)
                        .SetSubTotal(60.00m)
                        .Build(),
                    DTO.Create.OrderItem.NewBuilder()
                        .SetProductId(PRODUCT_ID)
                        .SetQuantity(3)
                        .SetPrice(50.00m)
                        .SetSubTotal(150.00m)
                        .Build()
                    })
                .Build();

            Customer customer = new (new CustomerId(CUSTOMER_ID));

            Restaurant restaurantResponse = Restaurant.NewBuilder()
                    .SetRestaurantId(new RestaurantId(_createOrderCommand.RestaurantId))
                    .SetProducts(new List<Product> { 
                        new Product( new ProductId(PRODUCT_ID), "product-1", new Money(50.00m)),
                        new Product(new ProductId(PRODUCT_ID), "product-2", new Money(50.00m))
                    })
                    .SetActive(true)
                    .Build();

            Order order = _fixture.OrderDataMapper.CreateOrderCommandToOrder(_createOrderCommand);
            order.Id = new OrderId(ORDER_ID);
            _fixture.CustomerRepository.Setup(x => x.FindCustomer(CUSTOMER_ID)).Returns(customer);
            
            _fixture.RestaurantRepository.Setup(x => x.FindRestaurantInformation(_fixture.OrderDataMapper.CreateOrderCommandToRestaurant(_createOrderCommand)))
                .Returns(restaurantResponse);

            _fixture.OrderRepository.Setup(x => x.Save(It.IsAny<Order>())).Returns(order);
            _orderApplicationService = (IOrderApplicationService)_fixture.ServiceProvider.GetService(typeof(IOrderApplicationService));
        }
        [Fact]
        public void WhenCreateOrder_ShoudIsPass()
        {
            CreateOrderResponse createOrderResponse = _orderApplicationService.CreateOrder(_createOrderCommand);
            Assert.Equal(OrderStatus.PENDING, createOrderResponse.OrderStatus);
            Assert.Equal("Order created successfully", createOrderResponse.Message);
            Assert.NotNull(createOrderResponse.OrderTrackingId);
        }

        [Fact]
        public void WhenCreateOrderWithWrongTotalPrice_ShouldThrowException()
        {
            OrderDomainException orderDomainException = Assert.Throws<OrderDomainException>(() => _orderApplicationService.CreateOrder(_createOrderCommandWrongPrice));
            Assert.Equal("Total price: 250.00 isn't equal Order items total: 200.00!", orderDomainException.Message);
        }
        [Fact]
        public void WhenCreateOrderWithWrongProductPrice_ShouldThrowException()
        {
            OrderDomainException orderDomainException = Assert.Throws<OrderDomainException>(() => _orderApplicationService.CreateOrder(_createOrderCommandWrongProductPrice));
            Assert.Equal($"Order item price: 60.00 is not valid for product {PRODUCT_ID}", orderDomainException.Message);
        }
        [Fact]
        public void WhenCreateOrderWithPassiveRestaurant_ShouldThrowException()
        {
            Restaurant restaurantResponse = Restaurant.NewBuilder()
                    .SetRestaurantId(new RestaurantId(_createOrderCommand.RestaurantId))
                    .SetProducts(new List<Product> { 
                        new Product( new ProductId(PRODUCT_ID), "product-1", new Money(50.00m)),
                        new Product(new ProductId(PRODUCT_ID), "product-2", new Money(50.00m))
                    })
                    .SetActive(false)
                    .Build();
            
            _fixture.RestaurantRepository.Setup(x => x.FindRestaurantInformation(_fixture.OrderDataMapper.CreateOrderCommandToRestaurant(_createOrderCommand)))
                .Returns(restaurantResponse);
            
            OrderDomainException orderDomainException = Assert.Throws<OrderDomainException>(() => _orderApplicationService.CreateOrder(_createOrderCommandWrongProductPrice));
            Assert.Equal($"Restaurant with id {RESTAURANT_ID} is currently not active!", orderDomainException.Message);
        }
    }
}