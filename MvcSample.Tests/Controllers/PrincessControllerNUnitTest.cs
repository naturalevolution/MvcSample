using System.Collections.Generic;
using System.Web.Mvc;
using MvcSample.Controllers;
using MvcSample.Domain;
using MvcSample.Repositories;
using MvcSample.Resources.Models;
using MvcSample.Resources.Shared;
using MvcSample.Tests.Factory;
using NUnit.Framework;
using Rhino.Mocks;

namespace MvcSample.Tests.Controllers {
    [TestFixture]
    public class PrincessControllerNUnitTest {

        private PrincessController controller;
        private MockRepository mocks;
        private IPrincessRepository repository;

        [SetUp]
        public void SetUp() {
            mocks = new MockRepository();
            repository = mocks.StrictMock<IPrincessRepository>();

            controller = new PrincessController(repository);
        }

        [Test]
        public void CreatePrincessShouldShowModelInPartialView() {

            var result = controller.Create();

            Assert.IsInstanceOf(typeof(PartialViewResult), result);
        }

        [Test]
        public void MirrorOfPrincessShouldShowMessageInJsonView() {

            var result = controller.MirrorOfPrincess();

            Assert.IsInstanceOf(typeof(JsonResult), result);
        }

        [Test]
        public void MirrorOfPrincessShouldTellTheTruth() {

            var result = controller.MirrorOfPrincess() as JsonResult;
            
            Assert.IsTrue(result.Data.ToString().Contains("MyJsonValue"));
            Assert.IsTrue(result.Data.ToString().Contains(PersonResource.UniversalQuestion));
        }

        [Test]
        public void CreatePrincessShouldRedirectToIndexWhenSuccess() {
            Princess user = PersonFactory.CreatePrincess("Justin");

            var result = controller.Create(user) as JsonResult;

            Assert.IsTrue(result.Data.ToString().Contains("Redirect"));
            Assert.IsTrue(result.Data.ToString().Contains("Princess/"));
        }

        [Test]
        public void CreatePrincessShouldShowErrorWhenModelIsInvalid() {
            controller.ModelState.AddModelError("key", "model is invalid");

            var result = controller.Create(Arg<Princess>.Is.Anything) as PartialViewResult;

            Assert.AreEqual(Messages.Error_Field_Check, result.ViewBag.Message);
            Assert.AreEqual(PersonResource.Title_CreatePrincess, result.ViewBag.Title);
        }

        [Test]
        public void CreatePrincessShouldShowErrorWhenModelIsNull() {
            var result = controller.Create(Arg<Princess>.Is.Anything) as PartialViewResult;

            Assert.AreEqual(Messages.Error_Field_Check, result.ViewBag.Message);
            Assert.AreEqual(PersonResource.Title_CreatePrincess, result.ViewBag.Title);
        }

        [Test]
        public void DeletePrincessConfirmationShouldRedirectToIndexOnSuccess() {
            int id = 5;
            repository.Expect(x => x.Remove(id)).Return(MessageType.Success);
            mocks.ReplayAll();

            var result = controller.DeleteConfirmed(id) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
            repository.AssertWasCalled(x => x.Remove(id));
        }

        [Test]
        public void DeletePrincessConfirmationShowHttpNotFoundResultWhenBadId() {
            int id = 5;
            repository.Expect(x => x.Remove(Arg<int>.Is.Equal(id))).Return(MessageType.Error);
            mocks.ReplayAll();

            ActionResult result = controller.DeleteConfirmed(id);

            Assert.IsInstanceOf(typeof (HttpNotFoundResult), result);
        }

        [Test]
        public void DeletePrincessShouldShowDeleteConfirmation() {
            Princess princess = PersonFactory.CreatePrincess("Princess Of Something");
            int id = 5;
            repository.Expect(x => x.FindById(Arg<int>.Is.Equal(id))).Return(princess);
            mocks.ReplayAll();

            var result = controller.Delete(id) as ViewResult;

            Assert.AreEqual(princess, result.Model);
            repository.VerifyAllExpectations();
        }

        [Test]
        public void DeletePrincessShowHttpNotFoundResultWhenBadId() {
            ActionResult result = controller.Delete(0);

            Assert.IsInstanceOf(typeof (HttpNotFoundResult), result);
        }

        [Test]
        public void EditPrincessShouldBeRedirectToIndexWhenUpdated() {
            Princess princess = PersonFactory.CreatePrincess("Jayce");
            repository.Expect(x => x.Update(princess)).Return(MessageType.Success);
            mocks.ReplayAll();

            var result = controller.Edit(princess) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
            repository.AssertWasCalled(x => x.Update(princess));
        }

        [Test]
        public void EditPrincessShouldFillTheModel() {
            Princess princess = PersonFactory.CreatePrincess("Cailyn");
            int id = 5;
            repository.Expect(x => x.FindById(id)).Return(princess);
            mocks.ReplayAll();

            var result = controller.Edit(id) as ViewResult;

            Assert.AreEqual(princess, result.Model);
            repository.AssertWasCalled(x => x.FindById(id));
        }

        [Test]
        public void EditPrincessShouldRedirectToHttpNotFoundResultWhenNullModel() {

            mocks.ReplayAll();
            ActionResult result = controller.Edit(null);

            Assert.IsInstanceOf(typeof(HttpNotFoundResult), result);
            mocks.VerifyAll();
        }

        [Test]
        public void EditPrincessShouldRedirectToIndexWhenBadId() {
            Princess princess = null;
            int id = 5;
            repository.Expect(x => x.FindById(id)).Return(princess);
            mocks.ReplayAll();

            var result = controller.Edit(id) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
            repository.AssertWasCalled(x => x.FindById(id));
        }

        [Test]
        public void EditPrincessShouldRetrieveTheModel() {
            Princess princess = PersonFactory.CreatePrincess("Jeny");
            int id = 5;
            repository.Expect(x => x.FindById(id)).Return(princess);
            mocks.ReplayAll();

            var result = controller.Edit(id) as ViewResult;

            Assert.AreEqual(princess, result.Model);
            repository.AssertWasCalled(x => x.FindById(id));
        }

        [Test]
        public void IndexShouldShowPrincesses() {
            var princesses = new List<Princess> {
                                                    PersonFactory.CreatePrincess("Benoit XVI"),
                                                    PersonFactory.CreatePrincess("Mafalda")
                                                };
            repository.Expect(x => x.FindAll()).Return(princesses);
            mocks.ReplayAll();
            
            var result = controller.Index() as ViewResult;

            Assert.AreEqual(princesses, result.Model);
            repository.AssertWasCalled(x => x.FindAll());
        }

    }
}