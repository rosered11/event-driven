using System;
using System.Collections.Generic;
using Rosered11.Common.Domain.ValueObject;

namespace Rosered11.Order.Application.Service.DTO.Track
{
    public record TrackOrderResponse(
        Guid OrderTrackingId,
        OrderStatus OrderStatus,
        List<string> FailureMessages
    );
}