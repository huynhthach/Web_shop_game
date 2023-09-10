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
    public class MUAGAMEsController : Controller
    {
        private QLCuaHangGameEntities4 db = new QLCuaHangGameEntities4();

        // GET: MUAGAMEs
        public ActionResult Index()
        {
            var mUAGAMEs = db.MUAGAMEs.Include(m => m.USER);
            return View(mUAGAMEs.ToList());
        }
        [HttpPost]
        public ActionResult Index(string maHD)
        {
            var mUAGAMEs = db.MUAGAMEs.Include(m => m.USER);

            if (!string.IsNullOrEmpty(maHD))
            {
                mUAGAMEs = mUAGAMEs.Where(s => s.idMua.Contains(maHD));
            }
            return View(mUAGAMEs.ToList());
        }
        public ActionResult Cart()
        {
            
            return View();
        }
        // GET: MUAGAMEs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MUAGAME mUAGAME = db.MUAGAMEs.Find(id);
            if (mUAGAME == null)
            {
                return HttpNotFound();
            }
            return View(mUAGAME);
        }

        // GET: MUAGAMEs/Create
        public ActionResult Create(string idtk)
        {
            PLV.MaMua = "M" + db.MUAGAMEs.Count();
            ViewBag.idND = new SelectList(db.USERS, "id", "email");
            return View();
        }

        // POST: MUAGAMEs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idMua,idND,Deleted,trangThai,ngayMua")] MUAGAME mUAGAME)
        {
            if (ModelState.IsValid)
            {
                db.MUAGAMEs.Add(mUAGAME);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idND = new SelectList(db.USERS, "id", "email", mUAGAME.idND);
            return View(mUAGAME);
        }

        // GET: MUAGAMEs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MUAGAME mUAGAME = db.MUAGAMEs.Find(id);
            if (mUAGAME == null)
            {
                return HttpNotFound();
            }
            ViewBag.idND = new SelectList(db.USERS, "id", "email", mUAGAME.idND);
            return View(mUAGAME);
        }

        // POST: MUAGAMEs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idMua,idND,Deleted,trangThai,ngayMua")] MUAGAME mUAGAME)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mUAGAME).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idND = new SelectList(db.USERS, "id", "email", mUAGAME.idND);
            return View(mUAGAME);
        }

        // GET: MUAGAMEs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MUAGAME mUAGAME = db.MUAGAMEs.Find(id);
            if (mUAGAME == null)
            {
                return HttpNotFound();
            }
            return View(mUAGAME);
        }

        // POST: MUAGAMEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            MUAGAME mUAGAME = db.MUAGAMEs.Find(id);
            
            db.MUAGAMEs.Remove(mUAGAME);
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
        public ActionResult GioHang(string id)
        {
            if (id != null)
            {
                ViewBag.idnd = id;
                ViewBag.role = db.USERS.FirstOrDefault(p => p.id.ToString() == id).idRole.ToString();
                ViewBag.name = db.USERS.FirstOrDefault(p => p.id.ToString() == id).FuName;
            }
            else
            {
                ViewBag.role = "0";
                ViewBag.idnd = "ERROR";
                ViewBag.name = "ERROR";
            }

            ViewBag.idmua = "M" + db.MUAGAMEs.Count();
            
            ViewBag.idND = new SelectList(db.USERS, "id", "email");

            return View();
        }
        
        
        // POST: MUAGAMEs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GioHang([Bind(Include = "idMua,idND,Deleted,trangThai,ngayMua")] MUAGAME mUAGAME)
        {
            if (ModelState.IsValid)
            {
                db.MUAGAMEs.Add(mUAGAME);
                db.SaveChanges();
                return RedirectToAction("Index","GAMEs");
            }
            ViewBag.idND = new SelectList(db.USERS, "id", "email", mUAGAME.idND);
            return View(mUAGAME);
        }
    }
}
