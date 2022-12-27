namespace Rosered11.OrderService.Domain.Test
{
    public class OrderTestConfigurationFixture
    {
        public Mock<OrderCreatedPaymentRequestMessagePublisher> OrderCreatedPaymentRequestMessagePublisher { get; private set; }
        public Mock<OrderCancelledPaymentRequestMessagePublisher> OrderCancelledPaymentRequestMessagePublisher { get; private set; }
        public Mock<OrderPaidRestaurantRequestMessagePublisher> OrderPaidRestaurantRequestMessagePublisher { get; private set; }
        public Mock<OrderRepository> OrderRepository { get; private set; }
        public Mock<CustomerRepository> CustomerRepository { get; private set; }
        public Mock<RestaurantRepository> RestaurantRepository { get; private set; }
        public IOrderDomainService OrderDomainService { get; private set; }
        public OrderCreateHelper OrderCreateHelper { get; private set; }
        public OrderDataMapper OrderDataMapper { get; } = new();
        public OrderCreateCommandHandler OrderCreateCommandHandler { get; private set; }
        public OrderTrackCommandHandler OrderTrackCommandHandler { get; private set; }
        public ServiceProvider ServiceProvider { get; private set; }
        public OrderTestConfigurationFixture()
        {
            InitOrderCreatedPaymentRequestMessagePublisher();
            InitOrderCancelledPaymentRequestMessagePublisher();
            InitOrderPaidRestaurantRequestMessagePublisher();
            InitOrderRepository();
            InitCustomerRepository();
            InitRestaurantRepository();

            BuildDI();
        }
        public void BuildDI()
        {
            InitOrderDomainService();
            InitOrderCreateHelper();

            InitOrderCreateCommandHandler();
            InitOrderTrackCommandHandler();

            ServiceCollection serviceCollection = new ();
            serviceCollection.AddScoped<OrderCreatedPaymentRequestMessagePublisher>(x => OrderCreatedPaymentRequestMessagePublisher.Object);
            serviceCollection.AddScoped<OrderCancelledPaymentRequestMessagePublisher>(x => OrderCancelledPaymentRequestMessagePublisher.Object);
            serviceCollection.AddScoped<OrderPaidRestaurantRequestMessagePublisher>(x => OrderPaidRestaurantRequestMessagePublisher.Object);
            serviceCollection.AddScoped<OrderRepository>(x => OrderRepository.Object);
            serviceCollection.AddScoped<CustomerRepository>(x => CustomerRepository.Object);
            serviceCollection.AddScoped<RestaurantRepository>(x => RestaurantRepository.Object);
            serviceCollection.AddScoped<IOrderDomainService, OrderDomainService>();
            serviceCollection.AddScoped<OrderCreateHelper>(x => OrderCreateHelper);
            serviceCollection.AddScoped<OrderDataMapper>(x => OrderDataMapper);
            serviceCollection.AddScoped<OrderCreateCommandHandler>(x => OrderCreateCommandHandler);
            serviceCollection.AddScoped<OrderTrackCommandHandler>(x => OrderTrackCommandHandler);
            serviceCollection.AddScoped<IOrderApplicationService, OrderApplicationService>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        private void InitOrderCreatedPaymentRequestMessagePublisher()
        {
            Mock<OrderCreatedPaymentRequestMessagePublisher> mock = new();
            OrderCreatedPaymentRequestMessagePublisher = mock;
        }
        private void InitOrderCancelledPaymentRequestMessagePublisher()
        {
            Mock<OrderCancelledPaymentRequestMessagePublisher> mock = new();
            OrderCancelledPaymentRequestMessagePublisher = mock;
        }
        private void InitOrderPaidRestaurantRequestMessagePublisher()
        {
            Mock<OrderPaidRestaurantRequestMessagePublisher> mock = new();
            OrderPaidRestaurantRequestMessagePublisher = mock;
        }
        private void InitOrderRepository()
        {
            Mock<OrderRepository> mock = new();
            OrderRepository = mock;
        }
        private void InitCustomerRepository()
        {
            Mock<CustomerRepository> mock = new();
            CustomerRepository = mock;
        }
        private void InitRestaurantRepository()
        {
            Mock<RestaurantRepository> mock = new();
            RestaurantRepository = mock;
        }
        private void InitOrderDomainService()
        {
            Mock<ILogger<OrderDomainService>> logger = new();
            OrderDomainService = new OrderDomainService(logger.Object);
        }
        private void InitOrderCreateHelper()
        {
            Mock<ILogger<OrderCreateHelper>> logger = new();
            OrderCreateHelper = new OrderCreateHelper(logger.Object, OrderDomainService, OrderRepository.Object, CustomerRepository.Object, RestaurantRepository.Object, OrderDataMapper);
        }

        private void InitOrderCreateCommandHandler()
        {
            Mock<ILogger<OrderCreateCommandHandler>> logger = new();
            OrderCreateCommandHandler = new(logger.Object, OrderCreateHelper, OrderDataMapper, OrderCreatedPaymentRequestMessagePublisher.Object);
        }
        private void InitOrderTrackCommandHandler()
        {
            Mock<ILogger<OrderTrackCommandHandler>> logger = new();
            OrderTrackCommandHandler = new(logger.Object, OrderDataMapper, OrderRepository.Object);
        }
    }
}
