using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcSample;
using MvcSample.Controllers;

namespace MvcSample.Tests.Controllers {
    [TestClass]
    public class HomeControllerTest {
        private HomeController controller;

        [TestInitialize]
        public void MyTestInitialize() {
            controller = new HomeController();
        }

        [TestMethod]
        public void Index() {
            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("Modify this template to jump-start your ASP.NET MVC application.", result.ViewBag.Message);
        }

        [TestMethod]
        public void About() {
            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Contact() {
            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
