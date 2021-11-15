namespace BurgerQueen.Model.MenuItems
{
    public class CheeseBurgerMeal : CheeseBurger
    {
        private readonly Drink _drink = new Drink();

        protected override void GetPrerequisites()
        {
            base.GetPrerequisites();

            Ingredients.Add("Fries");
        }

        public override MenuItem SendToService()
        {
            GetPrerequisites();
            Prepare();
            _drink.SendToService();
            
            Ingredients.AddRange(_drink.Ingredients);
            
            base.SendToService();
            return this;
        }
    }
}