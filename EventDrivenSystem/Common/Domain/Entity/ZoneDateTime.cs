namespace Rosered11.Common.Domain.Entity
{
    public class ZoneDateTime
    {
        private readonly DateTimeOffset _dateTimeOffset;
        public ZoneDateTime()
        {
            _dateTimeOffset = DateTimeOffset.UtcNow;
        }
        public ZoneDateTime(DateTimeOffset dateTimeOffset)
        {
            _dateTimeOffset = dateTimeOffset;
        }
        public static ZoneDateTime UtcNow() => new ZoneDateTime();
        public override string ToString()
        {
            return base.ToString();
        }
        public DateTimeOffset GetDateTime => _dateTimeOffset;
    }
}