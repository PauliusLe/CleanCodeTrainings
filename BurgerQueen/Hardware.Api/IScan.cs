using BurgerQueen.Model;

namespace BurgerQueen.Hardware.Api
{
    public interface IScan
    {
        void Scan(Receipt receipt);
    }
}