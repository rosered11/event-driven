namespace Rosered11.Order.Domain.Core.Exception
{
    public class OrderDomainException : System.Exception
    {
        public OrderDomainException(string message) : base(message)
        {
        }
        public OrderDomainException(string message, System.Exception? ex) : base(message, ex)
        {
        }
    }
}