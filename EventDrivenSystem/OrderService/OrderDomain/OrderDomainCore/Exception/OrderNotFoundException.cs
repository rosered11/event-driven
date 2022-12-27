using Rosered11.OrderService.Common.Exception;

namespace Rosered11.OrderService.Exception
{
    public class OrderNotFoundException : DomainException
    {
        public OrderNotFoundException(string message) : base(message)
        {
        }

        public OrderNotFoundException(string message, System.Exception exception) : base(message, exception)
        {
        }
    }
}