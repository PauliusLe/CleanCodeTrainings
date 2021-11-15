using BurgerQueen.Model;

namespace BurgerQueen.Services.Calculator
{
    public class MealRule : ICalculationRule
    {
        public bool IsMatch(string itemId) => itemId == Constants.CheeseBurgerMeal;

        public double Apply(OrderItem item)
        {
            return item.Price * item.Quantity * 0.9;
        }
    }
}