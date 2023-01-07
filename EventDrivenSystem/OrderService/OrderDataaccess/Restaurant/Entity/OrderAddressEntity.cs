using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rosered11.OrderService.DataAccess.Entity
{
    public class OrderAddressEntity
    {
        public Guid Id { get; set; }
        [Required]
        public required string Street { get; set; }
        public required string PostalCode { get; set; }
        public required string City { get; set; }

        [ForeignKey("Order")]
        public Guid? OrderId { get; set; }
        public OrderEntity? Order { get; set; }
    }
}