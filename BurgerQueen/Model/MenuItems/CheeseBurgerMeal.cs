namespace BurgerQueen.Model.MenuItems
{
    public class CheeseBurgerMeal : MenuItem
    {
        public override void GetPrerequisites()
        {
            // Burger
            Ingredients.Add("Bread");
            Ingredients.Add("Ham");
            Ingredients.Add("Salad");
            
            // Fries
            Ingredients.Add("Fries");
            
            // Drink
            Ingredients.Add("Coca-cola");
        }

        public override void Prepare()
        {
            IsPrepared = true;
        }

        public override void SendToService()
        {
            IsSentToService = true;
        }
    }
}
