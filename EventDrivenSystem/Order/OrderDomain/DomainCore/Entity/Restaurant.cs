using System.Collections.Generic;
using Rosered11.Common.Domain.Entity;
using Rosered11.Common.Domain.ValueObject;

namespace Rosered11.Order.Domain.Core.Entity
{
    public class Restaurant : AggregateRoot<RestaurantId>
    {
        public List<Product> Products { get; set; } = new();
        public bool Active;
        public Restaurant(RestaurantId id) : base(id)
        {
        }
    }
}