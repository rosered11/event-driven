using Microsoft.Extensions.Logging;
using Rosered11.OrderService.Domain.DTO.Message;
using Rosered11.OrderService.Domain.Ports.Input.Message.Listener.Payment;

namespace Rosered11.OrderService.Domain
{
    public class PaymentResposeMessageListener : IPaymentResponseMessageListener
    {
        private readonly ILogger<PaymentResposeMessageListener> _logger;
        public void PaymentCancelled(PaymentResponse paymentResponse)
        {
            throw new NotImplementedException();
        }

        public void PaymentCompleted(PaymentResponse paymentResponse)
        {
            throw new NotImplementedException();
        }
    }
}