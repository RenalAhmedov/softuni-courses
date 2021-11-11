using System;
using WildFarm.Food;

namespace WildFarm.Animals
{
    public abstract class Animal : IAnimal
    {
        protected Animal(string name, double weight)
        {
            this.Name = name;
            this.Weight = weight;
        }

        public string Name { get; set; }

        public double Weight { get; set; }

        public int FoodEaten { get; set; }

        public abstract void Eat(IFood food);

        public abstract string ProduceSound();

        protected void ThrowInvalidOperationExceptionForFood(
            IAnimal animal,
            IFood food)
        {
            throw new InvalidOperationException(
                $"{animal.GetType().Name} does not eat {food.GetType().Name}!");
        }
    }
}
