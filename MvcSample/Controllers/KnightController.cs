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
    public class KnightController : BaseController {

        public KnightController() {
             KnightRepository = new KnightRepository(_context);
        }

        public KnightController(IKnightRepository repository) {
            ViewBag.Title = PersonResource.Title_Knights;
            KnightRepository = repository ?? new KnightRepository(_context);
        }
        //
        // GET: /Knight/

        public ActionResult Index()
        {
            return View(KnightRepository.FindAll());
        }

        //
        // GET: /Knight/Details/5

        public ActionResult Details(int id = 0)
        {
            Knight knight = KnightRepository.FindById(id);
            if (knight == null)
            {
                return HttpNotFound();
            }
            return View(knight);
        }

        //
        // GET: /Knight/Create

        public ActionResult Create() {
            ViewBag.Title = PersonResource.Title_CreateKnight;
            return View();
        }

        //
        // POST: /Knight/Create

        [HttpPost]
        public ActionResult Create(Knight knight)
        {
            if (ModelState.IsValid && knight != null)
            {
                KnightRepository.Add(knight);
                return RedirectToAction("Index");
            }
            ViewBag.Message = Messages.Error_Field_Check;
            ViewBag.Title = PersonResource.Title_CreateKnight;
            return View(knight);
        }

        //
        // GET: /Knight/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Knight knight = KnightRepository.FindById(id);
            if (knight == null) {
                return RedirectToAction("Index");
            }
            return View(knight);
        }

        //
        // POST: /Knight/Edit/5

        [HttpPost]
        public ActionResult Edit(Knight knight)
        {
            if (knight == null) {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                MessageType messageType = KnightRepository.Update(knight);
                if (messageType == MessageType.Success) {
                    return RedirectToAction("Index");
                }
            }
            return View(knight);
        }

        //
        // GET: /Knight/Delete/5

        public ActionResult Delete(int id = 0)
        {
            if (id > 0) {
                Knight knight = KnightRepository.FindById(id);
                if (knight != null) {
                    return View(knight);
                }
                
            }
            return HttpNotFound();
        }

        //
        // POST: /Knight/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id) {
            MessageType messageType = KnightRepository.Remove(id);
            if (messageType == MessageType.Success) {
                return RedirectToAction("Index");
            }
            return HttpNotFound();
        }

        protected override void Dispose(bool disposing)
        {
           // db.Dispose();
            base.Dispose(disposing);
        }
    }
}