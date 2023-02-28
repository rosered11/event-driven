using System;
using Rosered11.Common.Domain.ValueObject;

namespace Rosered11.Order.Domain.Core.ValueObject
{
    public class TrackingId : BaseId<Guid>
    {
        public TrackingId(Guid value) : base(value)
        {
        }
    }
}