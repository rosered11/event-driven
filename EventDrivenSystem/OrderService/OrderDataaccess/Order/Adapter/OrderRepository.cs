using Rosered11.OrderService.DataAccess.Entity;
using Rosered11.OrderService.DataAccess.Mapper;
using Rosered11.OrderService.Domain.Entities;
using Rosered11.OrderService.Domain.Ports.Output.Repository;
using Rosered11.OrderService.Domain.ValueObject;

namespace Rosered11.OrderService.DataAccess.Adapter
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Repository.OrderRepository _orderRepository;
        private readonly OrderDataAccessMapper _orderDataAccessMapper;

        public OrderRepository(DataAccess.Repository.OrderRepository orderRepository, OrderDataAccessMapper orderDataAccessMapper)
        {
            _orderRepository = orderRepository;
            _orderDataAccessMapper = orderDataAccessMapper;
        }
        public Order FindByTrackingId(TrackingId trackingId)
        {
            var order = _orderRepository.Find(trackingId.GetValue());
            if (order == null)
                throw new System.Exception("");
            return _orderDataAccessMapper.OrderEntityToOrder(order);
        }

        public Order Save(Order order)
        {
            return _orderDataAccessMapper.OrderEntityToOrder(_orderRepository.Save(_orderDataAccessMapper.OrderToOrderEntity(order)));
        }
    }
}