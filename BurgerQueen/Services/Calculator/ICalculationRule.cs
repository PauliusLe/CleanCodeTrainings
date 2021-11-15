using BurgerQueen.Model;

namespace BurgerQueen.Services.Calculator
{
    public interface ICalculationRule
    {
        bool IsMatch(string itemId);
        double Apply(OrderItem item);
    }
}