using BurgerQueen.Model;

namespace BurgerQueen.Services.Calculator
{
    public class DrinkRule : ICalculationRule
    {
        public bool IsMatch(string itemId) => itemId == Constants.Drink;

        public double Apply(OrderItem item)
        {
            var setsOfThree = item.Quantity / 3;
            return (item.Quantity - setsOfThree) * item.Price;
        }
    }
}