using WildFarm.Food;

namespace WildFarm.Animals
{
    public interface IAnimal
    {
        public string Name { get; set; }

        public double Weight { get; set; }

        public int FoodEaten { get; set; }

        string ProduceSound();

        void Eat(IFood food);
    }
}
