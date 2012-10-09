using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSample.Domain;
using MvcSample.Repositories;
using MvcSample.Resources.Models;
using MvcSample.Resources.Shared;

namespace MvcSample.Controllers
{
    public class PrincessController : BaseController
    {
        public PrincessController() {
            PrincessRepository = new PrincessRepository(_context);
        }

        public PrincessController(IPrincessRepository repository) {
            ViewBag.Title = PersonResource.Title_Princesses;
            PrincessRepository = repository ?? new PrincessRepository(_context);
        }

        public ActionResult Index() {
            return View(PrincessRepository.FindAll());
        }

        public ActionResult Details(int id = 0) {
            Princess princess = PrincessRepository.FindById(id);
            if (princess == null) {
                return HttpNotFound();
            }
            return View(princess);
        }

        public ActionResult Create() {
            ViewBag.Title = PersonResource.Title_CreatePrincess;
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(Princess princess) {
            if (ModelState.IsValid && princess != null) {
                PrincessRepository.Add(princess);
                return Json(new {Redirect = "Princess/"});
            }
            ViewBag.Message = Messages.Error_Field_Check;
            ViewBag.Title = PersonResource.Title_CreatePrincess;
            return PartialView("_Create", princess);
        }

        public ActionResult Edit(int id = 0) {
            Princess princess = PrincessRepository.FindById(id);
            if (princess == null) {
                return RedirectToAction("Index");
            }
            return View(princess);
        }

        [HttpPost]
        public ActionResult Edit(Princess princess) {
            if (princess == null) {
                return HttpNotFound();
            }
            if (ModelState.IsValid) {
                MessageType messageType = PrincessRepository.Update(princess);
                if (messageType == MessageType.Success) {
                    return RedirectToAction("Index");
                }
            }
            return View(princess);
        }

        public ActionResult Delete(int id = 0) {
            if (id > 0) {
                Princess princess = PrincessRepository.FindById(id);
                if (princess != null) {
                    return View(princess);
                }

            }
            return HttpNotFound();
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id) {
            MessageType messageType = PrincessRepository.Remove(id);
            if (messageType == MessageType.Success) {
                return RedirectToAction("Index");
            }
            return HttpNotFound();
        }

        public JsonResult MirrorOfPrincess() {
            return Json(new { MyJsonValue  = PersonResource.UniversalQuestion}, JsonRequestBehavior.AllowGet);
        }
    }
}