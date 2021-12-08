// Use this file for your unit tests.
// When you are ready to submit, REMOVE all using statements to Festival Manager (entities/controllers/etc)
// Test ONLY the Stage class. 
namespace FestivalManager.Tests
{
    using FestivalManager.Entities;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestFixture]
    public class StageTests
    {
        private Performer performer;
        private Song song;
        private Stage stage;

        [SetUp]
        public void Setup()
        {
            performer = new Performer("Volodya", "Stoyanov", 61);
            song = new Song("Ako umram, il zaginam", TimeSpan.FromMinutes(3));
            stage = new Stage();
            performer.SongList.Add(song);
            stage.AddSong(song);
            stage.AddPerformer(performer);
        }

        [Test]
        public void VerifyAddPerformerMethod()
        {
            int expected = 2;
            Performer temp = new Performer("Slavcho", "Chalgarya", 28);
            stage.AddPerformer(temp);
            Assert.AreEqual(expected, stage.Performers.Count);
        }

        [Test]
        public void VerifyReadOnlyCollection()
        {
            var expectedCollection = stage.Performers;
            Assert.True(expectedCollection is IReadOnlyCollection<Performer>);
        }

        [Test]
        public void AddPerformerMethodThrowsExceptionWhenPerformerIsNull()
        {
            performer = null;
            Assert.Throws<ArgumentNullException>(() => stage.AddPerformer(performer));
        }

        [Test]
        public void AddPerformerMethodThrowsExceptionWhenPerformersAgeIsBellow18()
        {
            Performer temp = new Performer("Slavcho", "Chalgarya", 6);
            Assert.Throws<ArgumentException>(() => stage.AddPerformer(temp));
        }

        [Test]
        public void VerifyAddSongMethod()
        {
            int expected = 2;
            Song temp = new Song("Edno ferari", TimeSpan.FromMinutes(4));
            performer.SongList.Add(temp);
            stage.AddSong(temp);
            Assert.AreEqual(expected, performer.SongList.Count);
        }

        [Test]
        public void AddSongMethodThrowsExceptionWhenSongIsNull()
        {
            song = null;
            Assert.Throws<ArgumentNullException>(() => stage.AddSong(song));
        }

        [Test]
        public void AddSongMethodThrowsExceptionWhenSongIsUnderOneMinute()
        {
            Song temp = new Song("Edno ferari", TimeSpan.FromMinutes(0.50));
            Assert.Throws<ArgumentException>(() => stage.AddSong(temp));
        }

        [Test]
        public void VerifyAddSongToPerformerMethod()
        {
            string expectedMessage = $"{song} will be performed by {performer}";
            Assert.AreEqual(expectedMessage, stage.AddSongToPerformer(song.Name, performer.FullName));
        }

        [Test]
        public void AddSongToPerformerMethodThrowsExceptionWhenSongNameIsNull()
        {
            song = null;
            Assert.Throws<ArgumentNullException>(() => stage.AddSongToPerformer(song?.Name, performer.FullName));
        }

        [Test]
        public void AddSongToPerformerMethodThrowsExceptionWhenPerformerNameIsNull()
        {
            performer = null;
            Assert.Throws<ArgumentNullException>(() => stage.AddSongToPerformer(song.Name, performer?.FullName));
        }

        [Test]
        public void VerifyPlayMethod()
        {
            string expectedMessage = $"{stage.Performers.Count} performers played {performer.SongList.Count} songs";
            Assert.AreEqual(expectedMessage, stage.Play());
        }

    }

}
