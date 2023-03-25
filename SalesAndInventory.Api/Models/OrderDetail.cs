namespace SalesAndInventory.Api.Models
{
    public class OrderDetail
    {
        public int OrderId { get; private set; }
        public Order Order { get; private set; }

        public int ProductId { get; private set; }
        public Product Product { get; private set; }

        public decimal UnitPrice { get; private set; }
        public short Qty { get; private set; }
        public decimal Discount { get; private set; }

        private OrderDetail()
        {
        }

        public OrderDetail(int orderId, int productId, decimal unitPrice, short qty, decimal discount)
        {
            OrderId = orderId;
            ProductId = productId;
            UnitPrice = unitPrice;
            Qty = qty;
            Discount = discount;
        }
    }
}