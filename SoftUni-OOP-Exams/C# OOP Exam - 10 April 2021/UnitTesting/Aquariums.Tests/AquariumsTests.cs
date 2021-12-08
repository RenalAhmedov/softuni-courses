namespace Aquariums.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public class AquariumsTests
    {
        [Test]
        public void TestConstructor()
        {
            Aquarium aquarium = new Aquarium("ivo", 2);
            Assert.AreEqual(2, aquarium.Capacity);
        }
        [Test]
        public void NameIsNullOrEmptyThrowsExcp()
        {
            Assert.Throws<ArgumentNullException>(() => new Aquarium(null, 1));
            Assert.Throws<ArgumentNullException>(() => new Aquarium(string.Empty, 1));
        }
        [Test]
        public void CapacitySetUnderZeroThrowsExcp()
        {
            Assert.Throws<ArgumentException>(() => new Aquarium("renal", -1));   
        }
        [Test]
        public void CountGivesRightCounts()
        {
            Aquarium aquarium = new Aquarium("a", 1);
            aquarium.Add(new Fish("a"));

            Assert.AreEqual(1, aquarium.Count);

        }
        [Test]
        public void CapacityIsFull()
        {
            Aquarium aquarium = new Aquarium("renal", 0);

            Assert.Throws<InvalidOperationException>(() => aquarium.Add(new Fish("renal")));
        }
        [Test]
        public void RemoveFishThrowExcp()
        {
            Aquarium aquarium = new Aquarium("renal", 1);

            Assert.Throws<InvalidOperationException>(() => aquarium.RemoveFish(null));
        }
        [Test]
        public void RemoveFishCorrect()
        {
            Aquarium aquarium = new Aquarium("renal", 1);
            aquarium.Add(new Fish("ivan"));

            aquarium.RemoveFish("ivan");
            Assert.AreEqual(aquarium.Count, 0);

        }
        [Test]
        public void SellFishThrowExcp()
        {
            Aquarium aquarium = new Aquarium("renal", 1);

            Assert.Throws<InvalidOperationException>(() => aquarium.SellFish(null));
        }
        [Test]
        public void SellFish()
        {

            Aquarium aquarium = new Aquarium("renal", 1);
            aquarium.Add(new Fish("ivan"));

            Fish fish = aquarium.SellFish("ivan");
            Assert.AreEqual(fish.Name, "ivan");
            //Assert.AreEqual(fish.Available, false);
        }
        [Test]
        public void Report()
        {
            Aquarium aquarium = new Aquarium("renal", 1);
            aquarium.Add(new Fish("ivan"));

            string expMessage = $"Fish available at renal: ivan";
            Assert.AreEqual(expMessage, aquarium.Report());
        }

    }
}
