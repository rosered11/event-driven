namespace Rosered11.OrderService.Domain.ValueObject
{
    public class StreetAddress
    {
        private readonly Guid id;
        private readonly string street;
        private readonly string postalCode;
        private readonly string city;

        public StreetAddress(Guid id, string street, string postalCode, string city)
        {
            this.id = id;
            this.street = street;
            this.postalCode = postalCode;
            this.city = city;
        }
        // override object.Equals
        public override bool Equals(object? obj)
        {
            //
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //
            
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            StreetAddress that = (StreetAddress)obj;
            
            return street == that.street && postalCode == that.postalCode && city == that.city;
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            // throw new System.NotImplementedException();
            return Tuple.Create(street, postalCode, city).GetHashCode();//base.GetHashCode();
        }
    }
}