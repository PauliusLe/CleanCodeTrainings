using BurgerQueen.Model;

namespace BurgerQueen.Hardware.Api
{
    internal interface IPrinter
    {
        void Print(Receipt item);
    }
}