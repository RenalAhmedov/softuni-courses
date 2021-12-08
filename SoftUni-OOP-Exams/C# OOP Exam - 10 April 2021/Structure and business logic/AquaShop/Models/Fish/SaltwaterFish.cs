using System;
using System.Collections.Generic;
using System.Text;

namespace AquaShop.Models.Fish
{
    public class SaltwaterFish : Fish
    {
        private const int FreshwaterFishInitialSize = 5;
        public SaltwaterFish(string name, string species, decimal price) : base(name, species, price)
        {
        }

        public override void Eat()
        {
            this.Size += 2;
        }
    }
}
