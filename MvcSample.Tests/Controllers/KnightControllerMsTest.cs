using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcSample.Controllers;
using MvcSample.Domain;
using MvcSample.Repositories;
using MvcSample.Resources.Models;
using MvcSample.Resources.Shared;
using MvcSample.Tests.Factory;

namespace MvcSample.Tests.Controllers {
    [TestClass]
    public class KnightControllerMsTest {
        private KnightController controller;
        private Mock<IKnightRepository> repository;

        [TestInitialize]
        public void TestInitialize() {
            repository = new Mock<IKnightRepository>();
            controller = new KnightController(repository.Object);
        }

        [TestMethod]
        public void IndexShouldShowKnights() {
            var knights = new List<Knight> {
                                               PersonFactory.CreateKnight("user1"),
                                               PersonFactory.CreateKnight("user2")
                                           };
            repository.Setup(x => x.FindAll()).Returns(knights);

            var result = controller.Index() as ViewResult;

            Assert.AreEqual(knights, result.Model);
            repository.Verify(x => x.FindAll(), Times.Once());
        }


        [TestMethod]
        public void CreateKnightShouldShowModelInView() {

            var result = controller.Create();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void CreateKnightShouldRedirectToIndexWhenSuccess() {
            Knight user = PersonFactory.CreateKnight("user1");

            var result = controller.Create(user) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void CreateKnightShouldShowErrorWhenModelIsNull() {

            var result = controller.Create(It.IsAny<Knight>()) as ViewResult;

            Assert.AreEqual(Messages.Error_Field_Check, result.ViewBag.Message);
            Assert.AreEqual(PersonResource.Title_CreateKnight, result.ViewBag.Title);

        }

        [TestMethod]
        public void CreateKnightShouldShowErrorWhenModelIsInvalid() {
            controller.ModelState.AddModelError("key", "model is invalid");

            var result = controller.Create(It.IsAny<Knight>()) as ViewResult;

            Assert.AreEqual(Messages.Error_Field_Check, result.ViewBag.Message);
            Assert.AreEqual(PersonResource.Title_CreateKnight, result.ViewBag.Title);
        }

        [TestMethod]
        public void DeleteKnightShouldShowDeleteConfirmation() {
            var knight = PersonFactory.CreateKnight("ChevalierX");
            int id = 5;
            repository.Setup(x => x.FindById(id)).Returns(knight);

            var result = controller.Delete(id) as ViewResult;

            Assert.AreEqual(knight, result.Model);
            repository.Verify(x => x.FindById(id), Times.Once());
        }

        [TestMethod]
        public void DeleteKnightShowHttpNotFoundResultWhenBadId() {
            var result = controller.Delete(0);

            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult)); 
        }

        [TestMethod]
        public void DeleteKnightConfirmationShouldRedirectToIndexOnSuccess() {
            int id = 5;
            repository.Setup(x => x.Remove(id)).Returns(MessageType.Success);

            var result = controller.DeleteConfirmed(id) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
            repository.Verify(x => x.Remove(id), Times.Once());
        }

        [TestMethod]
        public void DeleteKnightConfirmationShowHttpNotFoundResultWhenBadId() {
            int id = 5;
            repository.Setup(x => x.Remove(id)).Returns(MessageType.Error);

            var result = controller.DeleteConfirmed(id);

            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult)); 
        }

        [TestMethod]
        public void EditKnightShouldRetrieveTheModel() {
            Knight knight = PersonFactory.CreateKnight("HellBoy");
            int id = 5;
            repository.Setup(x => x.FindById(id)).Returns(knight);

            var result = controller.Edit(id) as ViewResult;

            Assert.AreEqual(knight, result.Model);
            repository.Verify(x => x.FindById(id), Times.Once());
        }

        [TestMethod]
        public void EditKnightShouldBeRedirectToIndexWhenUpdated() {
            Knight knight = PersonFactory.CreateKnight("HellBoy");
            repository.Setup(x => x.Update(knight)).Returns(MessageType.Success);

            var result = controller.Edit(knight) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
            repository.Verify(x => x.Update(knight), Times.Once());
        }

        [TestMethod]
        public void EditKnightShouldRedirectToHttpNotFoundResultWhenNullModel() {

            var result = controller.Edit(null);

            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
            repository.Verify(x => x.Update(It.IsAny<Knight>()), Times.Never());
        }

        [TestMethod]
        public void EditKnightShouldRedirectToIndexWhenBadId() {
            Knight knight = null;
            int id = 5;
            repository.Setup(x => x.FindById(id)).Returns(knight);

            var result = controller.Edit(id) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
            repository.Verify(x => x.FindById(id), Times.Once());
        }

        [TestMethod]
        public void EditKnightShouldFillTheModel() {
            Knight knight = PersonFactory.CreateKnight("HellBoy");
            int id = 5;
            repository.Setup(x => x.FindById(id)).Returns(knight);

            var result = controller.Edit(id) as ViewResult;

            Assert.AreEqual(knight, result.Model);
            repository.Verify(x => x.FindById(id), Times.Once());
        }
    }
}