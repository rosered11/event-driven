using Rosered11.OrderService.Common.Exception;

namespace Rosered11.OrderService.Order.Exception
{
    public class OrderDomainException : DomainException
    {
        public OrderDomainException(string message) : base(message)
        {
        }

        public OrderDomainException(string message, System.Exception exception) : base(message, exception)
        {
        }
    }
}