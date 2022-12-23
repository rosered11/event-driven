namespace Rosered11.OrderService.Domain.DTO.Create
{
    public class OrderItem
    {
        private readonly Guid _productId;
        private readonly int _quantity;
        private readonly decimal _price;
        private readonly decimal _subTotal;

        public Guid ProductId => _productId;

        public int Quantity => _quantity;

        public decimal Price => _price;

        public decimal SubTotal => _subTotal;
    }
}