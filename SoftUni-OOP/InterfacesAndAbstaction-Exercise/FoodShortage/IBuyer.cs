﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PersonInfo
{
    interface IBuyer
    {
        public void BuyFood();
        public int Food { get; set; }
    }
}
