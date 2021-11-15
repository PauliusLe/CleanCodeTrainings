using System.Collections.Generic;
using BurgerQueen.Model;

namespace BurgerQueen.Interfaces
{
    public interface ICalculatorService
    {
        double CalculateAmount(IEnumerable<OrderItem> items);
    }
}