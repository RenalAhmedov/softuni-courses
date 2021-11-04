using System;
using System.Collections.Generic;
using System.Text;

namespace Telephony
{
    public class Stationery_phone : ICallOtherPhones
    {
        public void CallOtherPhonesMethod(string phoneNumber)
        {
            Console.WriteLine($"Dialing... {phoneNumber}");
        }
    }
}
