using server.Domain.Enum;

namespace server.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal TotalAmount { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private readonly List<OrderItem> _items = new();
        public IReadOnlyCollection<OrderItem> Items => _items;

        private Order() { }

        public Order(Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Status = OrderStatus.Pending;
            CreatedAt = DateTime.UtcNow;
        }

        public void AddItem(Guid productId, int quantity, decimal price)
        {
            if (quantity <= 0)
                throw new ArgumentException("Số lượng phải lớn hơn 0");

            var item = new OrderItem(Id, productId, quantity, price);
            _items.Add(item);

            RecalculateTotal();
        }

        public void MarkProcessing()
        {
            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException("Đơn hàng phải ở trạng thái chờ (Pending) mới có thể xử lý");

            Status = OrderStatus.Processing;
        }

        public void MarkCompleted()
        {
            if (Status != OrderStatus.Processing)
                throw new InvalidOperationException("Đơn hàng phải đang xử lý mới có thể hoàn thành");

            Status = OrderStatus.Completed;
        }

        public void MarkFailed()
        {
            Status = OrderStatus.Failed;
        }

        private void RecalculateTotal()
        {
            TotalAmount = _items.Sum(x => x.Quantity * x.Price);
        }
    }
}
