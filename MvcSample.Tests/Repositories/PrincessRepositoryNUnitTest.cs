using System;
using System.Data.Entity;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using MvcSample.Domain;
using MvcSample.Repositories;
using MvcSample.Tests.Factory;

namespace MvcSample.Tests.Repositories {
    [TestFixture]
    public class PrincessRepositoryNUnitTest : RepositoryTestFixture {

        [SetUp]
        public void SetUp() {
            LoadContext();
            PrincessRepository = new PrincessRepository(_context);
        }
        
        [TearDown]
        public virtual void TearDown() {
            DisposeContext();
        }

        [Test]
        public void RetrievePrincessById() {
            Princess expected = PersonFactory.CreatePrincess("Cendrion");
            _context.Princesses.Add(expected);
            _context.SaveChanges();

            Princess actual = PrincessRepository.FindById(expected.Id);
;
            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void PrincessCanBeUpdated() {
            Princess expected = PersonFactory.CreatePrincess("Esmeralda");
            _context.Princesses.Add(expected);
            _context.SaveChanges();

            expected.FirstName = "Reine Esmeralda";

            MessageType message = PrincessRepository.Update(expected);

            Assert.AreEqual(MessageType.Success, message);
            Assert.AreEqual(expected, _context.Princesses.Find(expected.Id));
        }

        [Test]
        public void PrincessCanNotEditANullPrincess() {
            MessageType message = PrincessRepository.Update(null);

            Assert.AreEqual(MessageType.Error, message);
        }


        [Test]
        public void ShouldNotRetrievePrincessWithId0() {

            Princess actual = PrincessRepository.FindById(0);

            Assert.IsNull(actual);
        }

        [Test]
        public void CanAddPrincess() {
            Princess expected = PersonFactory.CreatePrincess("Arthur");

            PrincessRepository.Add(expected);

            var princesses = _context.Princesses.ToList();
            Assert.AreEqual(1, princesses.Count());
            Assert.AreEqual(expected, princesses.FirstOrDefault());
        }

        [Test]
        public void ShouldNotAddNullPrincess() {

            PrincessRepository.Add(null);

            var princesses = _context.Princesses.ToList();
            Assert.AreEqual(0, princesses.Count());
        }

        [Test]
        public void CanDeletePrincess() {
            Princess expected1 = PersonFactory.CreatePrincess("Méchante reine");
            Princess expected2 = PersonFactory.CreatePrincess("Blanche neige");
            _context.Princesses.Add(expected1);
            _context.Princesses.Add(expected2);
            _context.SaveChanges();

            PrincessRepository.Remove(expected1.Id);

            var princesses = _context.Princesses.ToList();
            Assert.AreEqual(1, princesses.Count());
            Assert.AreEqual(expected2, princesses.FirstOrDefault());
        }

        [Test]
        public void PrincessShouldNotUseBadIdToDelete() {
            Princess expected1 = PersonFactory.CreatePrincess("Elisabeth");
            Princess expected2 = PersonFactory.CreatePrincess("Elisabeth II");
            _context.Princesses.Add(expected1);
            _context.Princesses.Add(expected2);
            _context.SaveChanges();

            PrincessRepository.Remove(6);

            var princesses = _context.Princesses.ToList();
            Assert.AreEqual(2, princesses.Count());
        }
    }
}
