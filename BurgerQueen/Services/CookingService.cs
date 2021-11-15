using System.Collections.Generic;
using BurgerQueen.Model;
using BurgerQueen.Model.MenuItems;

namespace BurgerQueen.Services
{
    public class CookingService
    {
        private readonly Dictionary<string, MenuItem> restaurantMenu = new Dictionary<string, MenuItem>
        {
            {Constants.Drink, new Drink()},
            {Constants.CheeseBurger, new CheeseBurger()},
            {Constants.CheeseBurgerMeal, new CheeseBurgerMeal()}
        };

        public MenuItem Prepare(string itemId)
        {
            var menuItem = restaurantMenu[itemId];
            if (menuItem is CheeseBurgerMeal || menuItem is CheeseBurger)
            {
                menuItem.GetPrerequisites();
                menuItem.Prepare();
                menuItem.SendToService();
            }
            else if (menuItem is Drink)
            {
                menuItem.SendToService();
            }

            return menuItem;
        }
    }
}