using BurgerQueen.Model;

namespace BurgerQueen.Hardware.Api
{
    internal interface IMachine
    {
        void Print(Receipt item);
        void Fax(Receipt item);
        void Scan(Receipt item);
    }
}