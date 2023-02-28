namespace Rosered11.Common.Domain.ValueObject
{
    public record Money(decimal Amount)
    {
        public static Money Zero => new(decimal.Zero);
        public override int GetHashCode()
        {
            return Amount.GetHashCode();
        }
        public bool isGreaterThanZero() {
            return Amount.CompareTo(decimal.Zero) > 0;
        }

        public bool isGreaterThan(Money money) {
            return Amount.CompareTo(money.Amount) > 0;
        }

        public Money add(Money money) {
            return new Money(decimal.Add(Amount, money.Amount));
        }

        public Money subtract(Money money) {
            return new Money(decimal.Subtract(Amount, money.Amount));
        }

        public Money multiply(int multiplier) {
            return new Money(decimal.Multiply(Amount, multiplier));
        }
    }
}