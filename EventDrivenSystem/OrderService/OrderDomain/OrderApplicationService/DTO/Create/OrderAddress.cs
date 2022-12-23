using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Rosered11.OrderService.Domain.DTO.Create
{
    public class OrderAddress
    {
        [NotNull]
        private readonly string _street;
        private readonly string _postalCode;
        private readonly string _city;

        public string Street => _street;

        public string PostalCode => _postalCode;

        public string City => _city;
    }
}