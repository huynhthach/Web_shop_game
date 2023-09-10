using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using tudong.Models;

namespace tudong.Controllers
{
    public class CHITIET_MUAGAMEController : Controller
    {
        private QLCuaHangGameEntities4 db = new QLCuaHangGameEntities4();

        // GET: CHITIET_MUAGAME
        public ActionResult Index()
        {
            var cHITIET_MUAGAME = db.CHITIET_MUAGAME.Include(c => c.GAME).Include(c => c.MUAGAME);
            return View(cHITIET_MUAGAME.ToList());
        }
        // GET: CHITIET_MUAGAME/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIET_MUAGAME cHITIET_MUAGAME = db.CHITIET_MUAGAME.Find(id);
            if (cHITIET_MUAGAME == null)
            {
                return HttpNotFound();
            }
            return View(cHITIET_MUAGAME);
        }

        // GET: CHITIET_MUAGAME/Create
        public ActionResult Create()
        {
            ViewBag.idGame = new SelectList(db.GAMEs, "idGame", "name");
            ViewBag.idMua = new SelectList(db.MUAGAMEs, "idMua", "trangThai");
            return View();
        }

        // POST: CHITIET_MUAGAME/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idChiTIet,idGame,idMua,discount,gia")] CHITIET_MUAGAME cHITIET_MUAGAME)
        {
            if (ModelState.IsValid)
            {
                db.CHITIET_MUAGAME.Add(cHITIET_MUAGAME);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idGame = new SelectList(db.GAMEs, "idGame", "name", cHITIET_MUAGAME.idGame);
            ViewBag.idMua = new SelectList(db.MUAGAMEs, "idMua", "trangThai", cHITIET_MUAGAME.idMua);
            return View(cHITIET_MUAGAME);
        }

        // GET: CHITIET_MUAGAME/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIET_MUAGAME cHITIET_MUAGAME = db.CHITIET_MUAGAME.Find(id);
            if (cHITIET_MUAGAME == null)
            {
                return HttpNotFound();
            }
            ViewBag.idGame = new SelectList(db.GAMEs, "idGame", "name", cHITIET_MUAGAME.idGame);
            ViewBag.idMua = new SelectList(db.MUAGAMEs, "idMua", "trangThai", cHITIET_MUAGAME.idMua);
            return View(cHITIET_MUAGAME);
        }

        // POST: CHITIET_MUAGAME/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idChiTIet,idGame,idMua,discount,gia")] CHITIET_MUAGAME cHITIET_MUAGAME)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cHITIET_MUAGAME).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idGame = new SelectList(db.GAMEs, "idGame", "name", cHITIET_MUAGAME.idGame);
            ViewBag.idMua = new SelectList(db.MUAGAMEs, "idMua", "trangThai", cHITIET_MUAGAME.idMua);
            return View(cHITIET_MUAGAME);
        }

        // GET: CHITIET_MUAGAME/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIET_MUAGAME cHITIET_MUAGAME = db.CHITIET_MUAGAME.Find(id);
            if (cHITIET_MUAGAME == null)
            {
                return HttpNotFound();
            }
            return View(cHITIET_MUAGAME);
        }

        // POST: CHITIET_MUAGAME/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CHITIET_MUAGAME cHITIET_MUAGAME = db.CHITIET_MUAGAME.Find(id);
            db.CHITIET_MUAGAME.Remove(cHITIET_MUAGAME);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
