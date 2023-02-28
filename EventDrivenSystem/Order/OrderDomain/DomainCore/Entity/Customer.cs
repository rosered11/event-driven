using Rosered11.Common.Domain.Entity;
using Rosered11.Common.Domain.ValueObject;

namespace Rosered11.Order.Domain.Core.Entity;

public class Customer : AggregateRoot<CustomerId>
{
    public string UserName { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Customer(CustomerId id) : base(id)
    {
    }
    public Customer(CustomerId id, string username, string firstname, string lastname) : base(id)
    {
        UserName = username;
        FirstName = firstname;
        LastName = lastname;
    }
}