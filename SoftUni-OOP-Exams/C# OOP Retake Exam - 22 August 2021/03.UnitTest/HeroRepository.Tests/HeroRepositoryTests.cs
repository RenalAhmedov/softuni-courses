using System;
using System.Linq;
using NUnit.Framework;

public class HeroRepositoryTests
{
    [Test]
    public void CreateHero_WithValidParameters_ShouldReturnSuccessMessage()
    {
        //Arrange
        Hero hero = new Hero("hero", 5);

        HeroRepository heroRepository = new HeroRepository();
        //Act 
        var mssg = heroRepository.Create(hero);

        //Assert    
        Assert.IsTrue(heroRepository.Heroes.Contains(hero));
        Assert.AreEqual(mssg, $"Successfully added hero {hero.Name} with level {hero.Level}");
    }

    [Test]
    public void CreateHero_WithNullParameters_ShouldThrowArgumentNullException()
    {
        //Arrange
        HeroRepository heroRepository = new HeroRepository();
        //Act   

        //Assert    
        Assert.Throws<ArgumentNullException>(() => { heroRepository.Create(null); }, "Hero is null");
    }

    [Test]
    public void CreateHero_WithDuplicateParameters_ShouldThrowInvalidOperationException()
    {
        //Arrange
        Hero hero = new Hero("hero", 5);
        Hero hero2 = new Hero("hero", 5);

        HeroRepository heroRepository = new HeroRepository();
        //Act 
        heroRepository.Create(hero);

        //Assert    
        Assert.Throws<InvalidOperationException>(() => { heroRepository.Create(hero2); }, $"Hero with name {hero.Name} already exists");
    }

    [Test]
    public void RemoveHero_WithValidParameter_ShouldReturnTrue()
    {
        //Arrange
        Hero hero = new Hero("hero", 5);

        HeroRepository heroRepository = new HeroRepository();
        //Act 
        heroRepository.Create(hero);
        var isRemoved = heroRepository.Remove(hero.Name);

        //Assert    
        Assert.IsFalse(heroRepository.Heroes.Contains(hero));
        Assert.IsTrue(isRemoved);
    }

    [Test]
    public void RemoveHero_WithNullParameter_ShouldThrowArgumentNullException()
    {
        //Arrange    
        HeroRepository heroRepository = new HeroRepository();
        //Act 
        //Assert    
        Assert.Throws<ArgumentNullException>(() => { heroRepository.Remove(null); }, "Name cannot be null");
    }
    [Test]
    public void RemoveHero_WithWhiteSpaceParameter_ShouldThrowArgumentNullException()
    {
        //Arrange    
        HeroRepository heroRepository = new HeroRepository();
        //Act 
        //Assert    
        Assert.Throws<ArgumentNullException>(() => { heroRepository.Remove(" "); }, "Name cannot be null");
    }

    [Test]
    public void RemoveHero_WithValidButNonExistingParameter_ShouldReturnFalse()
    {
        //Arrange
        Hero hero = new Hero("hero", 5);

        HeroRepository heroRepository = new HeroRepository();
        //Act 
        var isRemoved = heroRepository.Remove(hero.Name);

        //Assert    
        Assert.IsFalse(isRemoved);
    }

    [Test]
    public void GetHeroWithHighestLevel_WithNonElementsInTheList_ShouldThrowArgumentOutOfRange()
    {
        //Arrange

        HeroRepository heroRepository = new HeroRepository();
        //Act 

        //Assert    
        Assert.Throws<IndexOutOfRangeException>(() => { heroRepository.GetHeroWithHighestLevel(); });
    }

    [Test]
    public void GetHeroWithHighestLevel_WithHighestLevelInTheList_ShouldReturnHeroWithHighestLevel()
    {
        //Arrange
        Hero hero1 = new Hero("hero", 5);
        Hero hero2 = new Hero("enemy", 6);
        Hero hero3 = new Hero("boss", 7);
        HeroRepository heroRepository = new HeroRepository();
        //Act 
        heroRepository.Create(hero1);
        heroRepository.Create(hero2);
        heroRepository.Create(hero3);
        //Assert    
        Assert.AreEqual(hero3, heroRepository.GetHeroWithHighestLevel());
    }
    [Test]
    public void GetHeroWithHighestLevel_WithSameLevelInTheList_ShouldReturnHeroWhoEnteredFirst()
    {
        //Arrange
        Hero hero1 = new Hero("hero", 5);
        Hero hero2 = new Hero("enemy", 5);

        HeroRepository heroRepository = new HeroRepository();
        //Act 
        heroRepository.Create(hero1);
        heroRepository.Create(hero2);

        //Assert    
        Assert.AreEqual(hero1, heroRepository.GetHeroWithHighestLevel());
    }
    [Test]
    public void GetHeroByName_WithValidName_ShouldReturnHero()
    {
        //Arrange
        Hero hero1 = new Hero("hero", 5);
        Hero hero2 = new Hero("enemy", 6);

        HeroRepository heroRepository = new HeroRepository();
        //Act 
        heroRepository.Create(hero1);
        heroRepository.Create(hero2);

        var heroReturned = heroRepository.GetHero("hero");

        //Assert    
        Assert.AreNotSame(hero2, heroReturned);
        Assert.AreSame(hero1, heroReturned);
    }

    [Test]
    public void GetHeroByName_WithInValidName_ShouldReturnNull()
    {
        //Arrange
        Hero hero1 = new Hero("hero", 5);
        Hero hero2 = new Hero("enemy", 6);

        HeroRepository heroRepository = new HeroRepository();
        //Act 
        heroRepository.Create(hero1);
        heroRepository.Create(hero2);

        var heroReturned = heroRepository.GetHero("boss");

        //Assert    
        Assert.AreSame(null, heroReturned);

    }
}