namespace Rosered11.OrderService.Domain.Config
{
    public class OrderServiceConfigData
    {
        public string PaymentRequestTopicName { get; set; }
        public string PaymentResponseTopicName { get; set; }
        public string RestaurantApprovalRequestTopicName { get; set; }
        public string RestaurantApprovalResponseTopicName { get; set; }
    }
}