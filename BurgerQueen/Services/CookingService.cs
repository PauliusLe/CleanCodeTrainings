using System.Collections.Generic;
using BurgerQueen.Model;
using BurgerQueen.Model.MenuItems;

namespace BurgerQueen.Services
{
    public class CookingService
    {
        private readonly Dictionary<string, ItemData> _restaurantMenu = new Dictionary<string, ItemData>
        {
            {Const.Drink, new DrinkCola()},
            {Const.CheeseBurger, new CheeseBurger()},
            {Const.CheeseBurgerMeal, new CheeseBurgerMeal()}
        };

        public ItemData Prepare(string id)
        {
            var m = _restaurantMenu[id];
            if (m is CheeseBurgerMeal || m is CheeseBurger)
            {
                m.GetPrerequisites();
                m.Prepare();
                m.Send();
            }
            else if (m is DrinkCola)
            {
                m.Send();
            }

            return m;
        }
    }
}