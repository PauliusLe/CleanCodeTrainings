using BurgerQueen.Model;

namespace BurgerQueen.Hardware.Api
{
    internal interface IMachine
    {
        void Printer(Receipt b);
        void Fax(Receipt a);
        void Scan(Receipt item);
    }
}