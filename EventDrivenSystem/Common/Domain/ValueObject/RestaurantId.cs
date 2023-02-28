namespace Rosered11.Common.Domain.ValueObject
{
    public class RestaurantId : BaseId<Guid>
    {
        public RestaurantId(Guid id) : base(id){}
    }
}