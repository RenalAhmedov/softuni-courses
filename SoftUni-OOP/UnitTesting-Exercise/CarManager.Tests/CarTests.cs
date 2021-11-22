using NUnit.Framework;

namespace Tests
{
    public class CarTests
    {
        private Car car;
        private string make;
        private string model;
        private double fuelConsumption;
        private double fuelCapacity;
        private double fuelAmount;
        private double fuelToRefuel;
        private double distance;


        [Test]
        public void TestConstructorWithArguments()
        {
            make = "BMW";
            model = "X6";
            fuelConsumption = 1;
            fuelCapacity = 2;
            fuelAmount = 0;

            car = new Car(make, model, fuelConsumption, fuelCapacity);

            Assert.AreEqual(car.Make, make);
            Assert.AreEqual(car.Model, model);
            Assert.AreEqual(car.FuelConsumption, fuelConsumption);
            Assert.AreEqual(car.FuelCapacity, fuelCapacity);
            Assert.AreEqual(car.FuelAmount, fuelAmount);

        }

        [Test]
        public void MakeThrowsException()
        {
            make = null;
            model = "X6";
            fuelConsumption = 1;
            fuelCapacity = 2;

            Assert.That(() => car = new Car(make, model, fuelConsumption, fuelCapacity),
                Throws.ArgumentException);
        }

        [Test]
        public void ModelThrowsException()
        {
            make = "BMW";
            model = null;
            fuelConsumption = 1;
            fuelCapacity = 2;

            Assert.That(() => car = new Car(make, model, fuelConsumption, fuelCapacity),
                Throws.ArgumentException);
        }

        [Test]
        public void FuelConsumptionThrowsException()
        {
            make = "BMW";
            model = "X6";
            fuelConsumption = -1;
            fuelCapacity = 2;

            Assert.That(() => car = new Car(make, model, fuelConsumption, fuelCapacity),
                Throws.ArgumentException);
        }

        [Test]
        public void FuelCapacityThrowsException()
        {
            make = "BMW";
            model = "X6";
            fuelConsumption = 1;
            fuelCapacity = -2;

            Assert.That(() => car = new Car(make, model, fuelConsumption, fuelCapacity),
                Throws.ArgumentException);
        }

        [Test]
        public void RefuelThrowsException()
        {
            make = "BMW";
            model = "X6";
            fuelConsumption = 1;
            fuelCapacity = 2;
            fuelToRefuel = -1;

            car = new Car(make, model, fuelConsumption, fuelCapacity);

            Assert.That(() => car.Refuel(fuelToRefuel),
                Throws.ArgumentException);
        }
        [Test]
        public void RefuelThrowsExceptionWithZero()
        {
            make = "BMW";
            model = "X6";
            fuelConsumption = 1;
            fuelCapacity = 2;
            fuelToRefuel = -0;

            car = new Car(make, model, fuelConsumption, fuelCapacity);

            Assert.That(() => car.Refuel(fuelToRefuel),
                Throws.ArgumentException);
        }

        [Test]
        public void RefuelShouldBeEvenWithFuelAmount()
        {
            make = "BMW";
            model = "X6";
            fuelConsumption = 1;
            fuelCapacity = 2;
            fuelToRefuel = 1;

            car = new Car(make, model, fuelConsumption, fuelCapacity);

            car.Refuel(fuelToRefuel);

            Assert.AreEqual(car.FuelAmount, fuelToRefuel);

        }

        [Test]
        public void FuelAmountShouldNotExtendFuelCapacity()
        {
            make = "BMW";
            model = "X6";
            fuelConsumption = 1;
            fuelCapacity = 2;
            fuelToRefuel = 3;

            car = new Car(make, model, fuelConsumption, fuelCapacity);

            car.Refuel(fuelToRefuel);

            Assert.AreEqual(car.FuelAmount, car.FuelCapacity);
        }

        [Test]
        public void FuelNeededShouldNotExtendFuelAmount()
        {
            make = "BMW";
            model = "X6";
            fuelConsumption = 1;
            fuelCapacity = 2;
            distance = 100;

            car = new Car(make, model, fuelConsumption, fuelCapacity);

            Assert.That(() => car.Drive(distance),
                Throws.InvalidOperationException);
        }

        [Test]
        public void FuelAmountShoulNotBeNegative()
        {
            make = "BMW";
            model = "X6";
            fuelConsumption = 3;
            fuelCapacity = 5;
            distance = 100;
            fuelToRefuel = 5;

            car = new Car(make, model, fuelConsumption, fuelCapacity);
            car.Refuel(fuelToRefuel);
            var expectedFuel = car.FuelAmount - 3;
            car.Drive(distance);

            Assert.That(car.FuelAmount, Is.EqualTo(expectedFuel));
        }
    }
}