namespace Common.CommonDomain.ValueObject
{
    public class RestaurantId : BaseId<Guid>
    {
        public RestaurantId(Guid value) : base(value){}
    }
}