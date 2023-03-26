using System;

namespace SalesAndInventory.Api.Models
{
    public class OrderDetail
    {
        private OrderDetail()
        { }

        public OrderDetail(Order order, Product product, decimal unitPrice, short qty, decimal discount)
        {
            Order = order;
            OrderId = order.OrderId;
            Product = product;
            ProductId = product.ProductId;
            UnitPrice = unitPrice;
            Qty = qty;
            Discount = discount;
        }

        public int OrderId { get; private set; }
        public Order Order { get; private set; }
        public int ProductId { get; private set; }
        public Product Product { get; private set; }
        public decimal UnitPrice { get; private set; }
        public short Qty { get; private set; }
        public decimal Discount { get; private set; }
    }
}