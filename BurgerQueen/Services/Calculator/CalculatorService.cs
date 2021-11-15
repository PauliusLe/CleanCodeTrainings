using System.Collections.Generic;
using System.Linq;
using BurgerQueen.Interfaces;
using BurgerQueen.Model;

namespace BurgerQueen.Services.Calculator
{
    public class CalculatorService : ICalculatorService
    {
        private readonly List<ICalculationRule> _calculationRules = new List<ICalculationRule>
        {
            new BurgerRule(),
            new DrinkRule(),
            new MealRule()
        };

        public double CalculateAmount(IEnumerable<OrderItem> items)
        {
            var total = 0d;
            foreach (var item in items)
            {
                var rule = _calculationRules.FirstOrDefault(c => c.IsMatch(item.ItemId));
                if (rule != null)
                {
                    total += rule.Apply(item);
                }
            }

            return total;
        }
    }
}