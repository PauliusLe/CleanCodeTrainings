using System;

namespace BurgerQueen.Model.MenuItems
{
    public class Drink : MenuItem
    {
        public override void GetPrerequisites()
        {
            throw new NotImplementedException();
        }

        public override void Prepare()
        {
            throw new NotImplementedException();
        }

        public override void SendToService()
        {
            Ingredients.Add("Coca-cola");
            IsSentToService = true;
        }
    }
}
