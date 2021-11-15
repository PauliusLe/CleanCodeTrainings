using System.Collections.Generic;

namespace BurgerQueen.Model
{
    public class OrderInfo
    {
        //Amount
        public double Total { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }
        //Email
        public string Customer { get; set; }
    }
}