namespace Robots.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;

    public class RobotsTests
    {
        private RobotManager robotManager;
        private Robot robot;
        private const string RobotName = "Renal";
        private const int MaximumBattery = 101;
        private int Capacity = 1;
        [SetUp]
        public void Setup()
        {
            this.robot = new Robot(RobotName, MaximumBattery);
            this.robotManager = new RobotManager(Capacity);
            this.robotManager.Add(this.robot);
        }
        [Test]

        public void Constructor_ShouldWorkProperly()
        {
            int expectedCapacity = Capacity;
            Assert.AreEqual(expectedCapacity, this.robotManager.Capacity);
        }
        [Test]
        public void Capacity_IsLessThanZero()
        {
            Assert.Throws<ArgumentException>(() => this.robotManager = new RobotManager(-1));
        }
        [Test]
        public void Adding_RobotWithSameName()
        {
            this.robotManager = new RobotManager(3);
            this.robotManager.Add(this.robot);
            Assert.Throws<InvalidOperationException>(() => this.robotManager.Add(this.robot));
        }
        [Test]
        public void Adding_WhileCapacityIsFull()
        {
            Robot dummy = new Robot("Kircho", 20);
            Assert.Throws<InvalidOperationException>(() => this.robotManager.Add(dummy));
        }
        [Test]
        public void Remove_RobotWhenNull()
        {
            Assert.Throws<InvalidOperationException>(() => this.robotManager.Remove(null));
        }
        [Test]
        public void Remove_RobotProperly()
        {
            int expectedCount = 0;
            this.robotManager.Remove("Renal");
            Assert.AreEqual(expectedCount, this.robotManager.Count);
        }
        [Test]
        public void Work_RobotIsNull()
        {
            Assert.Throws<InvalidOperationException>(() => this.robotManager.Work("Renal", "Constructor", 200));
        }
        [Test]
        public void Work_RobotBatteryIsLess_ThanBatteryUsage()
        {
            Assert.Throws<InvalidOperationException>(() => this.robotManager.Work("Renal", "construction", 300));
        }
        [Test]

        public void Work_ShouldBehaveProperly()
        {
            int expectedBattery = this.robot.Battery - 100;
            this.robotManager.Work(this.robot.Name, "cleaning", 100);

            Assert.AreEqual(expectedBattery, this.robot.Battery);
        }
        [Test]
        public void Charge_RobotIsNull()
        {
            Assert.Throws<InvalidOperationException>(() => this.robotManager.Charge("mariicho"));
        }
        [Test]
        public void Charge_ShouldWorkProperly()
        {
            int expectedBattery = this.robot.MaximumBattery;
            this.robotManager.Work(this.robot.Name, "security", 100);
            this.robotManager.Charge(this.robot.Name);

            Assert.AreEqual(expectedBattery, this.robot.Battery);
        }
    }
}
