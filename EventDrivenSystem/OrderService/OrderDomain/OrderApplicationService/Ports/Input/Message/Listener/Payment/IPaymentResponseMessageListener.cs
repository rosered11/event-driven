using Rosered11.OrderService.Domain.DTO.Message;

namespace Rosered11.OrderService.Domain.Ports.Input.Message.Listener.Payment
{
    public interface IPaymentResponseMessageListener
    {
        void PaymentCompleted(PaymentResponse paymentResponse);
        void PaymentCancelled(PaymentResponse paymentResponse);
    }
}