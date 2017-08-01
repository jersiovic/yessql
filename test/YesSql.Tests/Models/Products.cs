using System.Collections.Generic;
using YesSql.Attributes;

namespace YesSql.Tests.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public IList<OrderLine> OrderLines { get; set; }

        public Order()
        {
            OrderLines = new List<OrderLine>();
        }
    }

    public class OrderLine
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [Reference(typeof(Product))]
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
