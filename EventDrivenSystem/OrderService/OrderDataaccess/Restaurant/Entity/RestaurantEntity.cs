using Microsoft.EntityFrameworkCore;
using Rosered11.OrderService.Common.DataAccess;

namespace Rosered11.OrderService.DataAccess.Entity
{
    [PrimaryKey(nameof(Id), nameof(ProductId))]
    public class RestaurantEntity : BaseEntity<Guid>
    {
        public Guid ProductId { get; set; }
        public string? RestaurantName { get; set; }
        public bool RestaurantActive { get; set; }
        public string? ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public RestaurantEntity(Guid id) : base(id)
        {
        }
    }
}