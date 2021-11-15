namespace BurgerQueen.Model.MenuItems
{
    public class Drink : MenuItem
    {
        public override MenuItem SendToService()
        {
            Ingredients.Add("Coca-cola");
            return base.SendToService();
        }
    }
}
