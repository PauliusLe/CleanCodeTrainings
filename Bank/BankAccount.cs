using System;

namespace CleanTests
{
    /// <summary>
    /// Bank account demo class.
    /// </summary>
    public class BankAccount
    {
        private readonly string _customerName;
        private double _balance;

        private BankAccount() { }

        public BankAccount(string customerName, double balance)
        {
            _customerName = customerName;
            _balance = balance;
        }

        public string CustomerName => _customerName;

        public double Balance => _balance;

        public void Debit(double amount)
        {
            if (amount > _balance)
            {
                throw new ArgumentOutOfRangeException("amount");
            }

            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount");
            }

            _balance += amount;
        }

        public void Credit(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount");
            }

            _balance += amount;
        }

        public void TryDeleteMoney()
        {
            var random = new Random();
            var next = random.Next(100);
            if (next > 50)
            {
                _balance = 0;
            }
        }

        public void FeelingLucky()
        {
            var currentDate = DateTime.UtcNow.Date;
            if (currentDate.Day % 5 == 0)
            {
                Credit(500);
            }
        }
    }
}