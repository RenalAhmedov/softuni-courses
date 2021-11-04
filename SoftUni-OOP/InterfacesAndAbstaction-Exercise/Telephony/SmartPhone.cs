using System;
using System.Collections.Generic;
using System.Text;

namespace Telephony
{
    public class Smartphone : ICallOtherPhones, IBrowsingWorldWideWeb
    {
        public void BrowsingWorldWideWebMethod(string url)
        {
            Console.WriteLine($"Browsing: {url}!");
        }

        public void CallOtherPhonesMethod(string phoneNumber)
        {
            Console.WriteLine($"Calling... {phoneNumber}");
        }
    }
}
