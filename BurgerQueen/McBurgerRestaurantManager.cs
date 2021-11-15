using System;
using BurgerQueen.Hardware.Api;
using BurgerQueen.Model;
using BurgerQueen.Services;
using BurgerQueen.Utilities;
using BurgerQueen.Utilities.Exceptions;

namespace BurgerQueen
{
    public class McBurgerRestaurantManager
    {
        readonly Hp printer;

        public McBurgerRestaurantManager()
        {
            printer = new Hp();
        }

        public OrderInfo DoStuff(OrderInfo orderInfo, PaymentDetails paymentDetails, bool printReceipt)
        {
            //Calculate amount
            var total = 0d;
            foreach (var i in orderInfo.Items)
            {
                if (i.ItemId == Const.Drink)
                {
                    var setsOfThree = i.Quantity / 3;
                    total += (i.Quantity - setsOfThree) * i.Price;
                }
                else if (i.ItemId == Const.CheeseBurger)
                {
                    total += i.Price * i.Quantity;
                }
                else if (i.ItemId == Const.CheeseBurgerMeal)
                {
                    total += i.Price * i.Quantity * 0.9;
                }
            }
            orderInfo.Total = total;
            
            
            
            if (paymentDetails.PaymentMethod == PaymentMethod.ContactCreditCard)
            {
                ChargeCard(paymentDetails, orderInfo);
            }
            else if (paymentDetails.PaymentMethod == PaymentMethod.ContactLessCreditCard)
            {
                //AuthorizePayment
                if (orderInfo.Total > 20) throw new UnAuthorizedContactLessPayment("Amount is too big");
                Logger.Info(string.Format("Payment for {0} has been authorized", orderInfo.Total));
                
                ChargeCard(paymentDetails, orderInfo);
            }
            else
            {
                throw new NotValidPaymentException("Can not charge customer");
            }

            //PrepareOrder
            var cooker = new CookingService();
            foreach (var item in orderInfo.Items)
            {
                item.Item = cooker.Prepare(item.ItemId);
            }

            if (printReceipt)
            {
                //PrintReceipt
                string customer = orderInfo.Customer;
                if (!string.IsNullOrEmpty(customer))
                {
                    var receipt = new Receipt
                    {
                        ReceiptTitle = "Receipt for your order placed on " + DateTime.Now,
                        ReceiptBody = "Your order details: \n "
                    };
                    foreach (var orderItem in orderInfo.Items)
                    {
                        receipt.ReceiptBody += orderItem.Quantity + " of item " + orderItem.ItemId;
                    }

                    try
                    {
                        printer.Printer(receipt);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("Problem sending notification email", ex);
                    }

                }
            }

            return orderInfo;
        }
        
        private void ChargeCard(PaymentDetails details, OrderInfo orderInfo)
        {
            using (var ccm = new CreditCardMachine())
            {
                try
                {
                    ccm.CardNumber = details.CreditCardNumber;
                    ccm.ExpiresMonth = details.ExpiresMonth;
                    ccm.ExpiresYear = details.ExpiresYear;
                    ccm.NameOnCard = details.CardholderName;
                    ccm.AmountToCharge = orderInfo.Total;

                    ccm.Charge();
                }
                catch (RejectedCardException ex)
                {
                    throw new OrderException("The card gateway rejected the card.", ex);
                }
            }
        }
    }
}
