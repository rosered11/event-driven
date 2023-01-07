using Microsoft.AspNetCore.Mvc;
using Rosered11.OrderService.Domain.DTO.Create;
using Rosered11.OrderService.Domain.DTO.Track;
using Rosered11.OrderService.Domain.Ports.Input.Service;

namespace Rosered11.OrderService.Application
{
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderApplicationService _orderApplicationService;

        public OrderController(ILogger<OrderController> logger, IOrderApplicationService orderApplicationService)
        {
            _logger = logger;
            _orderApplicationService = orderApplicationService;
        }

        [HttpPost]
        public ActionResult<CreateOrderResponse> CreateOrder([FromBody] CreateOrderCommand createOrderCommand)
        {
            _logger.LogInformation("Creating order for customer: {customer} at restaurant: {restaurant}", createOrderCommand.CustomerId, createOrderCommand.RestaurantId);
            CreateOrderResponse createOrderResponse = _orderApplicationService.CreateOrder(createOrderCommand);
            _logger.LogInformation("Order created with tracking id: {trackingId}", createOrderResponse.OrderTrackingId);
            return Ok(createOrderResponse);
        }

        [HttpGet("{trackingId}")]
        public ActionResult<TrackOrderResponse> GetOrderByTrackingId([FromQuery] Guid trackingId)
        {
            TrackOrderResponse trackOrderResponse = _orderApplicationService.TrackOrder(TrackOrderQuery.NewBuilder().SetOrderTrackingId(trackingId).Build());
            _logger.LogInformation("Return order status with tracking id: {trackingId}", trackOrderResponse.OrderTrackingId);
            return Ok(trackOrderResponse);
        }
    }
}