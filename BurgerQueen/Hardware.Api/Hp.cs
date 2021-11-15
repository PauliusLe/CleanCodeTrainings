using System;
using BurgerQueen.Model;

namespace BurgerQueen.Hardware.Api
{
    //Our new hp multi functional printer
    public class Hp : IMachine
    {
        public void Printer(Receipt b)
        {
            //Printing receipt to console
            Console.WriteLine("Printing receipt...");           
            Console.WriteLine(b.ReceiptTitle);           
            Console.WriteLine(b.ReceiptBody);           
        }

        //We dont need fax yet...
        public void Fax(Receipt a)
        {
            throw new NotImplementedException();
        }

        //We dont need scan...
        public void Scan(Receipt item)
        {
            throw new NotImplementedException();
        }
    }
}