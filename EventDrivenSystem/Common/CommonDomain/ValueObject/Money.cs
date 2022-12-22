namespace Common.CommonDomain.ValueObject
{
    public class Money
    {
        private readonly decimal _amount;
        public static readonly Money Zero = new Money(decimal.Zero);
        public Money(decimal amount)
        {
            _amount = amount;
        }

        public decimal GetAmount() => _amount;

        public bool IsGreaterThanZero() => _amount.CompareTo(decimal.Zero) > 0;
        public bool IsGreaterThan(Money money) => _amount.CompareTo(money._amount) > 0;
        public static Money operator +(Money source, Money destination) => new (SetScale(source._amount + destination._amount));
        public static Money operator *(Money source, int number) => new(SetScale(source._amount * number));
        public Money Add(Money money) => new(SetScale(_amount + money._amount));
        public Money Subtract(Money money) => new(SetScale(_amount - money._amount));
        public Money Multiply(Money money) => new (SetScale(_amount * money._amount));

        private static decimal SetScale(decimal input) => System.Math.Round(input, 2, System.MidpointRounding.ToEven);

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
            
            // TODO: write your implementation of Equals() here
            Money that = (Money)obj;
            return _amount.Equals(that._amount);
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            
            return _amount.GetHashCode();
        }
    }
}