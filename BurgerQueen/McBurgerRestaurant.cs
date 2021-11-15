using BurgerQueen.Interfaces;
using BurgerQueen.Model;

namespace BurgerQueen
{
    public class McBurgerRestaurant
    {
        private readonly ICalculatorService _calculatorService;
        private readonly IPaymentService _paymentService;
        private readonly IPrintService _printService;
        private readonly ICookingService _cookingService;

        public McBurgerRestaurant(ICalculatorService calculatorService,
            IPaymentService paymentService, IPrintService printService,
            ICookingService cookingService)
        {
            _calculatorService = calculatorService;
            _paymentService = paymentService;
            _printService = printService;
            _cookingService = cookingService;
        }

        public Order ExecuteOrder(Order order, PaymentDetails paymentDetails, bool printReceipt)
        {
            order.TotalAmount = _calculatorService.CalculateAmount(order.Items);
            _paymentService.Charge(paymentDetails, order);
            _cookingService.Prepare(order);

            if (printReceipt)
            {
                _printService.PrintReceipt(order);
            }

            return order;
        }
    }
}