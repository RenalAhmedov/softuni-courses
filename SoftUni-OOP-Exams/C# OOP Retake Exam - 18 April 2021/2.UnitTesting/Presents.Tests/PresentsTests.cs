using System;
using System.Linq;

namespace Presents.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class PresentsTests
    {
        [Test]
        public void Create_Should_ThrowExc_If_Present_Is_null()
        {
            Bag bag = new Bag();
            Present present = null;

            Assert.Throws<ArgumentNullException>(() => bag.Create(present));
        }
        [Test]
        public void Create_Should_ThrowExc_If_Present_Is_Already_In_Bag()
        {
            Bag bag = new Bag();
            Present present = new Present("Name", 15);
            bag.Create(present);

            Assert.Throws<InvalidOperationException>(() => bag.Create(present));
        }
        [Test]
        public void Create_Should_Create_Present()
        {
            Bag bag = new Bag();
            Present present = new Present("Name", 15);
            bag.Create(present);

            Assert.AreEqual(present, bag.GetPresent("Name"));
        }
        [Test]
        public void Remove_Should_remove_present()
        {
            Bag bag = new Bag();
            Present present = new Present("Name", 15);
            bag.Create(present);

            Assert.True(bag.Remove(present));
        }

        [Test]
        public void GetPresentWithLeastMagic_Return_Present()
        {
            Bag bag = new Bag();
            Present present = new Present("here", 15);
            Present present2 = new Present("asd", 5);
            bag.Create(new Present("oke", 5.1));
            bag.Create(present2);
            bag.Create(present);

            Assert.AreEqual(present2, bag.GetPresentWithLeastMagic());
        }


        [Test]
        public void GetPresent_Return_Present_With_Same_Name()
        {
            Bag bag = new Bag();
            Present present = new Present("Name", 15);
            Present present2 = new Present("Name2", 5);
            bag.Create(new Present("Name3", 5.1));
            bag.Create(present2);
            bag.Create(present);

            Assert.AreEqual(present, bag.GetPresent("Name"));
        }

        [Test]
        public void GetPresents_Return_ReadOnly_Collection()
        {
            Bag bag = new Bag();
            Present present = new Present("Name", 15);
            Present present2 = new Present("Name2", 5);
            bag.Create(new Present("Name3", 5.1));
            bag.Create(present2);
            bag.Create(present);

            bag.GetPresents();
            Assert.AreEqual(3, bag.GetPresents().Count);
        }

        [Test]
        public void GetPresents_Contains_all_elements()
        {
            Bag bag = new Bag();
            Present present = new Present("Name", 15);
            Present present2 = new Present("Name2", 5);

            bag.GetPresents();

            foreach (var presentt in bag.GetPresents())
            {
                Assert.True(bag.GetPresents().Contains(presentt));
            }
        }

    }
}