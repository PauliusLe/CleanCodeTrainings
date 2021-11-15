using System;

namespace BurgerQueen.Model.MenuItems
{
    public class DrinkCola : ItemData
    {
        public override void GetPrerequisites()
        {
            throw new NotImplementedException();
        }

        public override void Prepare()
        {
            throw new NotImplementedException();
        }

        public override void Send()
        {
            List.Add("Coca-cola");
            IsSentToService = true;
        }
    }
}
