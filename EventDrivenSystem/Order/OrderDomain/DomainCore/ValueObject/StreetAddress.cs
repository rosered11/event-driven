using System;

namespace Rosered11.Order.Domain.Core.ValueObject
{
    public class StreetAddress
    {
        public Guid ID { get; private set; }
        public string Street { get; private set; }
        public string PostalCode { get; private set; }
        public string City { get; private set; }
        public StreetAddress(Guid id, string street, string postalCode, string city)
        {
            ID = id;
            Street = street;
            PostalCode = postalCode;
            City = city;
        }

        public override bool Equals(object? obj)
        {
            if (ID == null || obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            var that = (StreetAddress)obj;
            return Street.Equals(that.Street) 
                && PostalCode.Equals(that.PostalCode) 
                && City.Equals(that.City);
        }
        
        public override int GetHashCode()
        {
            return (Street, PostalCode, City).GetHashCode();
        }
    }
}