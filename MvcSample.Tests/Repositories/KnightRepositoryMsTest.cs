using System;
using System.Data.Entity;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcSample.Domain;
using MvcSample.Repositories;
using MvcSample.Tests.Factory;

namespace MvcSample.Tests.Repositories {
    [TestClass]
    public class KnightRepositoryMsTest : RepositoryTestFixture {

        [TestInitialize]
        public virtual void TestInitialize() {
            LoadContext();
            KnightRepository = new KnightRepository(_context);
        }

        [TestCleanup]
        public virtual void TestCleanup() {
            DisposeContext();
        }

        [TestMethod]
        public void RetrieveKnightById() {
            Knight expected = PersonFactory.CreateKnight("Arthur");
            _context.Knights.Add(expected);
            _context.SaveChanges();

            Knight actual = KnightRepository.FindById(expected.Id);
;
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void KnightCanBeUpdated() {
            Knight expected = PersonFactory.CreateKnight("Arthur");
            _context.Knights.Add(expected);
            _context.SaveChanges();

            expected.Email = "newemail@email.com";
            expected.FirstName = "Arthur Junior";

            MessageType message = KnightRepository.Update(expected);

            Assert.AreEqual(MessageType.Success, message);
            Assert.AreEqual(expected, _context.Knights.Find(expected.Id));
        }

        [TestMethod]
        public void KnightCanNotEditANullKnight() {
            MessageType message = KnightRepository.Update(null);

            Assert.AreEqual(MessageType.Error, message);
        }


        [TestMethod]
        public void ShouldNotRetrieveKnightWithId0() {

            Knight actual = KnightRepository.FindById(0);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void CanAddKnight() {
            Knight expected = PersonFactory.CreateKnight("Arthur");

            KnightRepository.Add(expected);

            var knights = _context.Knights.ToList();
            Assert.AreEqual(1, knights.Count());
            Assert.AreEqual(expected, knights.FirstOrDefault());
        }

        [TestMethod]
        public void ShouldNotAddNullKnight() {

            KnightRepository.Add(null);

            var knights = _context.Knights.ToList();
            Assert.AreEqual(0, knights.Count());
        }

        [TestMethod]
        public void CanDeleteKnight() {
            Knight expected1 = PersonFactory.CreateKnight("Arthur");
            Knight expected2 = PersonFactory.CreateKnight("Lancelot");
            _context.Knights.Add(expected1);
            _context.Knights.Add(expected2);
            _context.SaveChanges();

            KnightRepository.Remove(expected1.Id);

            var knights = _context.Knights.ToList();
            Assert.AreEqual(1, knights.Count());
            Assert.AreEqual(expected2, knights.FirstOrDefault());
        }

        [TestMethod]
        public void KnightShouldNotUseBadIdToDelete() {
            Knight expected1 = PersonFactory.CreateKnight("Arthur");
            Knight expected2 = PersonFactory.CreateKnight("Lancelot");
            _context.Knights.Add(expected1);
            _context.Knights.Add(expected2);
            _context.SaveChanges();

            KnightRepository.Remove(6);

            var knights = _context.Knights.ToList();
            Assert.AreEqual(2, knights.Count());
        }
    }
}
