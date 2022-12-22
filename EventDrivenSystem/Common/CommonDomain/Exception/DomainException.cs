
namespace Rosered11.OrderService.Common.Exception
{
    public class DomainException : System.Exception
    {
        public DomainException(string message) : base(message)
        {
        }

        public DomainException(string message, System.Exception exception) : base(message, exception){}
    }
}