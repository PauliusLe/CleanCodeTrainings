namespace BurgerQueen.Model.MenuItems
{
    public class CheeseBurger : MenuItem
    {
        public override void GetPrerequisites()
        {
            Ingredients.Add("Bread");
            Ingredients.Add("Ham");
            Ingredients.Add("Salad");
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
