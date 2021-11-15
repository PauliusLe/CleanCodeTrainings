namespace BurgerQueen.Model
{
    public class OrderItem
    {
        public string ItemId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; } 
        public ItemData Item { get; set; } 
    }
}