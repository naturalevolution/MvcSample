using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSample.Domain;
using MvcSample.Repositories;

namespace MvcSample.Controllers
{
    public class CastleController : Controller
    {
        private MvcSampleContext db = new MvcSampleContext();

        //
        // GET: /Castle/

        public ActionResult Index()
        {
            return View(db.Castles.ToList());
        }

        //
        // GET: /Castle/Details/5

        public ActionResult Details(int id = 0)
        {
            Castle castle = db.Castles.Find(id);
            if (castle == null)
            {
                return HttpNotFound();
            }
            return View(castle);
        }

        //
        // GET: /Castle/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Castle/Create

        [HttpPost]
        public ActionResult Create(Castle castle)
        {
            if (ModelState.IsValid)
            {
                db.Castles.Add(castle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(castle);
        }

        //
        // GET: /Castle/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Castle castle = db.Castles.Find(id);
            if (castle == null)
            {
                return HttpNotFound();
            }
            return View(castle);
        }

        //
        // POST: /Castle/Edit/5

        [HttpPost]
        public ActionResult Edit(Castle castle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(castle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(castle);
        }

        //
        // GET: /Castle/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Castle castle = db.Castles.Find(id);
            if (castle == null)
            {
                return HttpNotFound();
            }
            return View(castle);
        }

        //
        // POST: /Castle/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Castle castle = db.Castles.Find(id);
            db.Castles.Remove(castle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}