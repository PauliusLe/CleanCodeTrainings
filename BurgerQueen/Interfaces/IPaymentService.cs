using BurgerQueen.Model;

namespace BurgerQueen.Interfaces
{
    public interface IPaymentService
    {
        void Charge(PaymentDetails paymentDetails, Order order);
    }
}