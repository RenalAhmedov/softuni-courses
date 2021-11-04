using System;
using System.Collections.Generic;
using System.Text;

namespace Cars
{
    public class Tesla : IElectricCar, ICar
    {
        public Tesla(string model, string color, int battery)
        {
            Battery = battery;
            Model = model;
            Color = color;
        }

        public int Battery { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }

        public string Start()
             => "Engine start";

        public string Stop()
            => "Breaaak!";

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Color} Tesla {Model} with {Battery} Batteries");
            sb.AppendLine($"{Start()}");
            sb.Append($"{Stop()}");
            return sb.ToString();
        }
    }
}
