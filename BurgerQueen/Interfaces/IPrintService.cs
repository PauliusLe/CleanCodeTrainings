using BurgerQueen.Model;

namespace BurgerQueen.Interfaces
{
    public interface IPrintService
    {
        void PrintReceipt(Order order);
    }
}