using NUnit.Framework;
using System;

namespace Tests
{
    public class ExtendedDatabaseTests
    {
        private ExtendedDatabase extendedDb;
        [SetUp]
        public void Setup()
        {
            extendedDb = new ExtendedDatabase();
        }

        [Test]
        public void ctor_AddPersonInsideTheDb()
        {
            var persons = new Person[5];
            for (int i = 0; i < persons.Length; i++)
            {
                persons[i] = new Person(i, $"Name:{i}");
            }
            extendedDb = new ExtendedDatabase(persons);
            Assert.That(extendedDb.Count, Is.EqualTo(persons.Length));

            foreach (var person in persons)
            {
                Person dbPerson = extendedDb.FindById(person.Id);
                Assert.That(person, Is.EqualTo(dbPerson));
            }
        }
        [Test]
        public void ctor_ThrowsExceptionWhenCapacityIsExceeded()
        {
            var persons = new Person[17];
            for (int i = 0; i < persons.Length; i++)
            {
                persons[i] = new Person(i, $"Pesho{i}");
            }
            Assert.Throws<ArgumentException>( () => extendedDb = new ExtendedDatabase(persons));
        }
        [Test]
        public void Add_ExceptionWhenCountIsExceeded()
        {
            var n = 16;
            for (int i = 0; i < n; i++)
            {
                extendedDb.Add(new Person(i, $"Name{i}"));
            }
            Assert.Throws<InvalidOperationException>(() => extendedDb.Add(new Person(16, "aidenisss")));
        }
        [Test]
        public void Add_ThrowsExceptionWhenUsernameIsExisting()
        {
            var name = "Pesho";
            extendedDb.Add(new Person(1, name));
           
            Assert.Throws<InvalidOperationException>(() => extendedDb.Add(new Person(16, name)));
        }
        [Test]
        public void Add_ThrowsExceptionWhenUsernameIsAlreadyUsed()
        {
            var id = 1;
            extendedDb.Add(new Person(id, "boyko borissov"));

            Assert.Throws<InvalidOperationException>(() => extendedDb.Add(new Person(id, "name")));
        }
        [Test]
        public void Add_IncrementCountWhenAllIsValid()
        {
            var expectedCount = 2;
            extendedDb.Add(new Person(1, "boyko borissov"));
            extendedDb.Add(new Person(2, "nedelcho borissov"));

            Assert.That(extendedDb.Count, Is.EqualTo(expectedCount));
        }
        [Test]
        public void Remove_ThrowsExceptionWhenDbIsEmpty()
        {
            Assert.Throws<InvalidOperationException>(() => extendedDb.Remove());
        }
        [Test]
        public void Remove_RemoveElementFromTheDb()
        {
            var n = 3;
            for (int i = 0; i < n; i++)
            {
                extendedDb.Add(new Person(i, $"Fresh{i}"));
            }
            extendedDb.Remove();
            Assert.That(extendedDb.Count, Is.EqualTo(n - 1));
            Assert.Throws<InvalidOperationException>(() => extendedDb.FindById(n - 1));
        }
        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void FindByUsername_ThrowsExceptionWhenUsernameIsInvalid(string username)
        {
            Assert.Throws<ArgumentNullException>(() => extendedDb.FindByUsername(username));
          
        }
        [Test]
        public void FindByUsername_ThrowsExceptionWhenUsernameIsNotExisting()
        {
            Assert.Throws<InvalidOperationException>(() => extendedDb.FindByUsername("askda"));

        }
        [Test]
        public void FindByUsername_ReturnsTheCorrectResult()
        {
            var person = new Person(1, "Pesho");
            extendedDb.Add(person);
            var dbPerson = extendedDb.FindByUsername(person.UserName);
            Assert.That(person, Is.EqualTo(dbPerson));

        }
        [Test]
        public void FindByID_ThrowExceptionForInvalid()
        {
            Assert.Throws<InvalidOperationException>(() => extendedDb.FindById(123));

        }
        [Test]
        [TestCase(-1)]
        [TestCase(-21)]
        public void FindByID_ThrowExceptionWhenIDIsNegativeValue(int id)
        {
            Assert.Throws<ArgumentOutOfRangeException>( () => extendedDb.FindById(id));
        }
        [Test]
        public void FindByID_ReturnsTheCorrectResult()
        {
            var person = new Person(1, "Pesho");
            extendedDb.Add(person);
            var dbPerson = extendedDb.FindById(person.Id);
            Assert.That(person, Is.EqualTo(dbPerson));

        }


    }
}