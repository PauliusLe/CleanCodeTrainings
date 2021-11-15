namespace BurgerQueen.Model.MenuItems
{
    
    public class CheeseBurgerMeal : ItemData
    {
        public override void GetPrerequisites()
        {
            // Burger
            List.Add("Bread");
            List.Add("Ham");
            List.Add("Salad");
            
            // Fries
            List.Add("Fries");
            
            // Drink
            List.Add("Coca-cola");
        }

        public override void Prepare()
        {
            Prepared = true;
        }

        public override void Send()
        {
            IsSentToService = true;
        }
    }
}
