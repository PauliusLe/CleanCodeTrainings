namespace BurgerQueen.Utilities.Exceptions
{
    public class NotValidPaymentException : OrderException
    {
        public NotValidPaymentException(string message)
            :base(message)
        {
        }
    }
}