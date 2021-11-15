namespace BurgerQueen.Model.MenuItems
{
    public class CheeseBurger : ItemData
    {
        public override void GetPrerequisites()
        {
            List.Add("Bread");
            List.Add("Ham");
            List.Add("Salad");
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
