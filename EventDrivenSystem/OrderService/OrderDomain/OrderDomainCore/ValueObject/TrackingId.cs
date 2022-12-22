using Common.CommonDomain.ValueObject;

namespace Rosered11.OrderService.Domain.ValueObject
{
    public class TrackingId : BaseId<Guid>
    {
        public TrackingId(Guid value) : base(value)
        {
            
        }
    }
}