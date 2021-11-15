using System.Linq;
using AutoFixture;
using BurgerQueen.Model;
using BurgerQueen.Services;
using BurgerQueen.Services.Calculator;
using BurgerQueen.Utilities.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace BurgerQueen
{
    [TestFixture]
    public class McBurgerRestaurantTests
    {
        private Fixture _fixture;
        private McBurgerRestaurant _restaurant;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            var calculatorService = new CalculatorService();
            var paymentService = new PaymentService();
            var cookingService = new CookingService();
            var printService = new PrintService();
            
            _restaurant = new McBurgerRestaurant(calculatorService, paymentService, printService, cookingService);
        }

        [Test]
        public void Should_contain_cheeseBurger_ingredients()
        {
            var orderItems = _fixture.Build<OrderItem>()
                .With(c => c.Quantity, 1)
                .With(c => c.Price, 10)
                .With(c => c.ItemId, Constants.CheeseBurger)
                .CreateMany(1);
            var order = _fixture.Build<Order>()
                .With(o => o.Items, orderItems)
                .Create();
            var fakePaymentDetails = _fixture.Build<PaymentDetails>()
                .With(c => c.PaymentMethod, PaymentMethod.ContactCreditCard)
                .Create();
            var fakePrintReceipt = true;

            var executedOrder = _restaurant.ExecuteOrder(order, fakePaymentDetails, fakePrintReceipt);

            
            var preparedItem = executedOrder.Items.Single().MenuItem; 
            
            Assert.True(preparedItem.IsPrepared);
            Assert.True(preparedItem.IsSentToService);
            
            Assert.Contains("Bread", preparedItem.Ingredients);
            Assert.Contains("Ham", preparedItem.Ingredients);
            Assert.Contains("Salad", preparedItem.Ingredients);
        }
        
        [Test]
        public void Should_contain_cheeseBurgerMeal_ingredients()
        {
            var orderItems = _fixture.Build<OrderItem>()
                .With(c => c.Quantity, 1)
                .With(c => c.Price, 10)
                .With(c => c.ItemId, Constants.CheeseBurgerMeal)
                .CreateMany(1);
            var order = _fixture.Build<Order>()
                .With(o => o.Items, orderItems)
                .Create();
            var fakePaymentDetails = _fixture.Build<PaymentDetails>()
                .With(c => c.PaymentMethod, PaymentMethod.ContactCreditCard)
                .Create();
            var fakePrintReceipt = true;

            var executedOrder = _restaurant.ExecuteOrder(order, fakePaymentDetails, fakePrintReceipt);

            
            var preparedItem = executedOrder.Items.Single().MenuItem; 
            
            Assert.True(preparedItem.IsPrepared);
            Assert.True(preparedItem.IsSentToService);
            
            Assert.Contains("Bread", preparedItem.Ingredients);
            Assert.Contains("Ham", preparedItem.Ingredients);
            Assert.Contains("Salad", preparedItem.Ingredients);
            
            Assert.Contains("Fries", preparedItem.Ingredients);
            Assert.Contains("Coca-cola", preparedItem.Ingredients);
        }
        
        [Test]
        public void Should_drink_be_sent_to_service()
        {
            var orderItems = _fixture.Build<OrderItem>()
                .With(c => c.Quantity, 1)
                .With(c => c.Price, 10)
                .With(c => c.ItemId, Constants.Drink)
                .CreateMany(1);
            var order = _fixture.Build<Order>()
                .With(o => o.Items, orderItems)
                .Create();
            var fakePaymentDetails = _fixture.Build<PaymentDetails>()
                .With(c => c.PaymentMethod, PaymentMethod.ContactCreditCard)
                .Create();
            var fakePrintReceipt = true;

            var executedOrder = _restaurant.ExecuteOrder(order, fakePaymentDetails, fakePrintReceipt);

            
            var preparedItem = executedOrder.Items.Single().MenuItem; 
            
            Assert.False(preparedItem.IsPrepared);
            Assert.True(preparedItem.IsSentToService);
            
            Assert.Contains("Coca-cola", preparedItem.Ingredients);
        }                
        
        [Test]
        public void Should_execute_order_when_payment_is_with_contact_and_print_receipt()
        {
            var orderItems = _fixture.Build<OrderItem>()
                .With(c => c.Quantity, 1)
                .With(c => c.Price, 10)
                .With(c => c.ItemId, Constants.CheeseBurgerMeal)
                .CreateMany(1);
            var order = _fixture.Build<Order>()
                .With(o => o.Items, orderItems)
                .Create();
            var fakePaymentDetails = _fixture.Build<PaymentDetails>()
                .With(c => c.PaymentMethod, PaymentMethod.ContactCreditCard)
                .Create();
            var fakePrintReceipt = true;

            _restaurant.ExecuteOrder(order, fakePaymentDetails, fakePrintReceipt);
        }

        [Test]
        public void Should_execute_order_when_order_is_3_drinks_and_payment_is_contactless_and_print_receipt()
        {
            var orderItems = _fixture.Build<OrderItem>()
                .With(c => c.Quantity, 1)
                .With(c => c.Price, 5)
                .With(c => c.ItemId, Constants.Drink)
                .CreateMany(3);
            var fakeOrder = _fixture.Build<Order>()
                .With(c => c.Items, orderItems)
                .Create();

            var fakePaymentDetails = _fixture.Build<PaymentDetails>()
                .With(c => c.PaymentMethod, PaymentMethod.ContactLessCreditCard)
                .Create();
            var fakePrintReceipt = true;

            _restaurant.ExecuteOrder(fakeOrder, fakePaymentDetails, fakePrintReceipt);
        }

        [Test]
        public void Should_throw_exception_when_order_is_5_burgers_and_payment_is_with_contactless()
        {
            var orderItems = _fixture.Build<OrderItem>()
                .With(c => c.Quantity, 5)
                .With(c => c.Price, 5)
                .With(c => c.ItemId, Constants.CheeseBurger)
                .CreateMany(1);

            var fakeOrder = _fixture.Build<Order>()
                .With(c => c.Items, orderItems)
                .Create();
            var fakePaymentDetails = _fixture.Build<PaymentDetails>()
                .With(c => c.PaymentMethod, PaymentMethod.ContactLessCreditCard)
                .Create();

            var fakePrintReceipt = false;

            _restaurant.Invoking(y => y.ExecuteOrder(fakeOrder, fakePaymentDetails, fakePrintReceipt))
                .Should().Throw<UnAuthorizedContactLessPayment>()
                .WithMessage("Amount is too big");
        }

        [Test]
        public void Should_throw_NotValidPaymentException_when_Payment_Method_is_mobile()
        {
            var fakeOrder = _fixture.Build<Order>()
                .Create();
            var fakePaymentDetails = _fixture.Build<PaymentDetails>()
                .With(c => c.PaymentMethod, PaymentMethod.Mobile)
                .Create();
            var fakePrintReceipt = true;

            _restaurant.Invoking(y => y.ExecuteOrder(fakeOrder, fakePaymentDetails, fakePrintReceipt))
                .Should().Throw<NotValidPaymentException>()
                .WithMessage("Can not charge customer");
        }

        [Test]
        public void Should_execute_order_when_Payment_with_contact_but_without_print_receipt()
        {
            var orderItems = _fixture.Build<OrderItem>()
                .With(c => c.Quantity, 1)
                .With(c => c.Price, 10)
                .With(c => c.ItemId, Constants.CheeseBurgerMeal)
                .CreateMany(1);
            var fakeOrder = _fixture.Build<Order>()
                .With(o=>o.Items, orderItems)
                .Create();
            var fakePaymentDetails = _fixture.Build<PaymentDetails>()
                .With(c => c.PaymentMethod, PaymentMethod.ContactCreditCard)
                .Create();
            var fakePrintReceipt = false;

            _restaurant.ExecuteOrder(fakeOrder, fakePaymentDetails, fakePrintReceipt);
        }

        [Test]
        public void Should_execute_order_when_order_is_1_menu_and_Payment_with_contactless_but_without_print_receipt()
        {
            var orderItems = _fixture.Build<OrderItem>()
                .With(c => c.Quantity, 1)
                .With(c => c.Price, 10)
                .With(c => c.ItemId, Constants.CheeseBurgerMeal)
                .CreateMany(1);
            var fakeOrder = _fixture.Build<Order>()
                .With(c => c.Items, orderItems)
                .Create();
            var fakePaymentDetails = _fixture.Build<PaymentDetails>()
                .With(c => c.PaymentMethod, PaymentMethod.ContactLessCreditCard)
                .Create();
            var fakePrintReceipt = false;

            _restaurant.ExecuteOrder(fakeOrder, fakePaymentDetails, fakePrintReceipt);
        }
    }
}
