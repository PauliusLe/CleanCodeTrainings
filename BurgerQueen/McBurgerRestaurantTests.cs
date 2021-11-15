using System.Linq;
using AutoFixture;
using BurgerQueen.Model;
using BurgerQueen.Utilities.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace BurgerQueen
{
    [TestFixture]
    public class McBurgerRestaurantTests
    {
        private Fixture fixture;
        private McBurgerRestaurantManager _restaurantManager;

        [SetUp]
        public void SetUp()
        {
            fixture = new Fixture();
            _restaurantManager = new McBurgerRestaurantManager();
        }
        
        [Test]
        public void Should_contain_cheeseBurger_ingredients()
        {
            var orderItems = fixture.Build<OrderItem>()
                .With(c => c.Quantity, 1)
                .With(c => c.Price, 10)
                .With(c => c.ItemId, Const.CheeseBurger)
                .CreateMany(1);
            var order = fixture.Build<OrderInfo>()
                .With(o => o.Items, orderItems)
                .Create();
            var fakePaymentDetails = fixture.Build<PaymentDetails>()
                .With(c => c.PaymentMethod, PaymentMethod.ContactCreditCard)
                .Create();
            var fakePrintReceipt = true;

            var executedOrder = _restaurantManager.DoStuff(order, fakePaymentDetails, fakePrintReceipt);

            
            var preparedItem = executedOrder.Items.Single().Item; 
            
            Assert.True(preparedItem.Prepared);
            Assert.True(preparedItem.IsSentToService);
            
            Assert.Contains("Bread", preparedItem.List);
            Assert.Contains("Ham", preparedItem.List);
            Assert.Contains("Salad", preparedItem.List);
        }
        
        [Test]
        public void Should_contain_cheeseBurgerMeal_ingredients()
        {
            var orderItems = fixture.Build<OrderItem>()
                .With(c => c.Quantity, 1)
                .With(c => c.Price, 10)
                .With(c => c.ItemId, Const.CheeseBurgerMeal)
                .CreateMany(1);
            var order = fixture.Build<OrderInfo>()
                .With(o => o.Items, orderItems)
                .Create();
            var fakePaymentDetails = fixture.Build<PaymentDetails>()
                .With(c => c.PaymentMethod, PaymentMethod.ContactCreditCard)
                .Create();
            var fakePrintReceipt = true;

            var executedOrder = _restaurantManager.DoStuff(order, fakePaymentDetails, fakePrintReceipt);

            
            var preparedItem = executedOrder.Items.Single().Item; 
            
            Assert.True(preparedItem.Prepared);
            Assert.True(preparedItem.IsSentToService);
            
            Assert.Contains("Bread", preparedItem.List);
            Assert.Contains("Ham", preparedItem.List);
            Assert.Contains("Salad", preparedItem.List);
            
            Assert.Contains("Fries", preparedItem.List);
            Assert.Contains("Coca-cola", preparedItem.List);
        }
        
        [Test]
        public void Should_drink_be_sent_to_service()
        {
            var orderItems = fixture.Build<OrderItem>()
                .With(c => c.Quantity, 1)
                .With(c => c.Price, 10)
                .With(c => c.ItemId, Const.Drink)
                .CreateMany(1);
            var order = fixture.Build<OrderInfo>()
                .With(o => o.Items, orderItems)
                .Create();
            var fakePaymentDetails = fixture.Build<PaymentDetails>()
                .With(c => c.PaymentMethod, PaymentMethod.ContactCreditCard)
                .Create();
            var fakePrintReceipt = true;

            var executedOrder = _restaurantManager.DoStuff(order, fakePaymentDetails, fakePrintReceipt);

            
            var preparedItem = executedOrder.Items.Single().Item; 
            
            Assert.False(preparedItem.Prepared);
            Assert.True(preparedItem.IsSentToService);
            
            Assert.Contains("Coca-cola", preparedItem.List);
        }        

        [Test]
        public void Should_execute_order_when_payment_is_with_contact_and_print_receipt()
        {
            var orderItems = fixture.Build<OrderItem>()
                .With(c => c.Quantity, 1)
                .With(c => c.Price, 10)
                .With(c => c.ItemId, Const.CheeseBurgerMeal)
                .CreateMany(1);
            var order = fixture.Build<OrderInfo>()
                .With(o => o.Items, orderItems)
                .Create();
            var fakePaymentDetails = fixture.Build<PaymentDetails>()
                .With(c => c.PaymentMethod, PaymentMethod.ContactCreditCard)
                .Create();
            var fakePrintReceipt = true;

            _restaurantManager.DoStuff(order, fakePaymentDetails, fakePrintReceipt);
        }

        [Test]
        public void Should_execute_order_when_order_is_3_drinks_and_payment_is_contactless_and_print_receipt()
        {
            var orderItems = fixture.Build<OrderItem>()
                .With(c => c.Quantity, 1)
                .With(c => c.Price, 5)
                .With(c => c.ItemId, Const.Drink)
                .CreateMany(3);
            var fakeOrder = fixture.Build<OrderInfo>()
                .With(c => c.Items, orderItems)
                .Create();

            var fakePaymentDetails = fixture.Build<PaymentDetails>()
                .With(c => c.PaymentMethod, PaymentMethod.ContactLessCreditCard)
                .Create();
            var fakePrintReceipt = true;

            _restaurantManager.DoStuff(fakeOrder, fakePaymentDetails, fakePrintReceipt);
        }

        [Test]
        public void Should_throw_exception_when_order_is_5_burgers_and_payment_is_with_contactless()
        {
            var orderItems = fixture.Build<OrderItem>()
                .With(c => c.Quantity, 5)
                .With(c => c.Price, 5)
                .With(c => c.ItemId, Const.CheeseBurger)
                .CreateMany(1);

            var fakeOrder = fixture.Build<OrderInfo>()
                .With(c => c.Items, orderItems)
                .Create();
            var fakePaymentDetails = fixture.Build<PaymentDetails>()
                .With(c => c.PaymentMethod, PaymentMethod.ContactLessCreditCard)
                .Create();

            var fakePrintReceipt = false;

            _restaurantManager.Invoking(y => y.DoStuff(fakeOrder, fakePaymentDetails, fakePrintReceipt))
                .Should().Throw<UnAuthorizedContactLessPayment>()
                .WithMessage("Amount is too big");
        }

        [Test]
        public void Should_throw_NotValidPaymentException_when_Payment_Method_is_mobile()
        {
            var fakeOrder = fixture.Build<OrderInfo>()
                .Create();
            var fakePaymentDetails = fixture.Build<PaymentDetails>()
                .With(c => c.PaymentMethod, PaymentMethod.Mobile)
                .Create();
            var fakePrintReceipt = true;

            _restaurantManager.Invoking(y => y.DoStuff(fakeOrder, fakePaymentDetails, fakePrintReceipt))
                .Should().Throw<NotValidPaymentException>()
                .WithMessage("Can not charge customer");
        }

        [Test]
        public void Should_execute_order_when_Payment_with_contact_but_without_print_receipt()
        {
            var orderItems = fixture.Build<OrderItem>()
                .With(c => c.Quantity, 1)
                .With(c => c.Price, 10)
                .With(c => c.ItemId, Const.CheeseBurgerMeal)
                .CreateMany(1);
            var fakeOrder = fixture.Build<OrderInfo>()
                .With(o=>o.Items, orderItems)
                .Create();
            var fakePaymentDetails = fixture.Build<PaymentDetails>()
                .With(c => c.PaymentMethod, PaymentMethod.ContactCreditCard)
                .Create();
            var fakePrintReceipt = false;

            _restaurantManager.DoStuff(fakeOrder, fakePaymentDetails, fakePrintReceipt);
        }

        [Test]
        public void Should_execute_order_when_order_is_1_menu_and_Payment_with_contactless_but_without_print_receipt()
        {
            var orderItems = fixture.Build<OrderItem>()
                .With(c => c.Quantity, 1)
                .With(c => c.Price, 10)
                .With(c => c.ItemId, Const.CheeseBurgerMeal)
                .CreateMany(1);
            var fakeOrder = fixture.Build<OrderInfo>()
                .With(c => c.Items, orderItems)
                .Create();
            var fakePaymentDetails = fixture.Build<PaymentDetails>()
                .With(c => c.PaymentMethod, PaymentMethod.ContactLessCreditCard)
                .Create();
            var fakePrintReceipt = false;

            _restaurantManager.DoStuff(fakeOrder, fakePaymentDetails, fakePrintReceipt);
        }
    }
}
