using BurgerQueen.Model;

namespace BurgerQueen.Hardware.Api
{
    public interface IFax
    {
        void Fax(Receipt receipt);
    }
}