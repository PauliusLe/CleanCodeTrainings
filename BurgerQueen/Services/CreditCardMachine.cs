using System;
using BurgerQueen.Utilities;
using BurgerQueen.Utilities.Exceptions;

namespace BurgerQueen.Services
{
    public class CreditCardMachine : IDisposable
    {
        public void Dispose()
        {
            //Disposing            
        }

        public string CardNumber { get; set; }
        public string ExpiresMonth { get; set; }
        public string ExpiresYear { get; set; }
        public string NameOnCard { get; set; }
        public double AmountToCharge { get; set; }

        public void Charge()
        {
            Connect("127.0.0.1", CardNumber, ExpiresMonth, ExpiresYear, NameOnCard);
            Charge(AmountToCharge);            
        }

        private void Charge(double amountToCharge)
        {
            //Charge amount
        }

        private void Connect(string gatewayAddress, string cardNumber, string expiresMonth, string expiresYear, string nameOnCard)
        {
            try
            {
                //connect to gateway
            }
            catch (GatewayConnectionException gcException)
            {
                Logger.Error(gcException.Message, gcException);
                throw new OrderException("Can not connect to gateway", gcException);
            }
        }
    }
}