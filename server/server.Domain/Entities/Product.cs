namespace server.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }

        private Product() { }

        public Product(string name, decimal price, int stock)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            Stock = stock;
        }

        public void ReduceStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Số lượng phải lớn hơn 0");

            if (Stock < quantity)
                throw new InvalidOperationException("Không đủ số lượng trong kho");

            Stock -= quantity;
        }
    }
}
