namespace BurgerQueen.Model.MenuItems
{
    public class CheeseBurger : MenuItem
    {
        protected virtual void GetPrerequisites()
        {
            Ingredients.Add("Bread");
            Ingredients.Add("Ham");
            Ingredients.Add("Salad");
        }

        public override MenuItem SendToService()
        {
            GetPrerequisites();
            Prepare();
            return base.SendToService();
        }
    }
}
