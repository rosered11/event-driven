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

        private OrderAddress(Builder builder)
        {
            _street = builder.Street;
            _postalCode = builder.PostalCode;
            _city = builder.City;
        }
        public static Builder NewBuilder()
        {
            return new Builder();
        }

        public sealed class Builder
        {
            public string Street { get; private set; }
            public string PostalCode { get; private set; }
            public string City { get; private set; }

            public Builder SetStreet(string street)
            {
                Street = street;
                return this;
            }
            public Builder SetPostalCode(string postalCode)
            {
                PostalCode = postalCode;
                return this;
            }
            public Builder SetCity(string city)
            {
                City = city;
                return this;
            }
            public OrderAddress Build() => new(this);
        }
    }
}