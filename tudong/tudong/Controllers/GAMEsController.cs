using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using tudong.Models;

namespace tudong.Controllers
{
    public class GAMEsController : Controller
    {
        private QLCuaHangGameEntities4 db = new QLCuaHangGameEntities4();

        // GET: GAMEs
        public ActionResult IndexAdmin()
        {

            var gAMEs = db.GAMEs.Include(g => g.USER).Include(g => g.THELOAI);
           
            return View(gAMEs.ToList());
        }
        [HttpPost]
        public ActionResult IndexAdmin(string game)
        {
            var gAMEs = db.GAMEs.Include(g => g.USER).Include(g => g.THELOAI);

            if (!string.IsNullOrEmpty(game))
            {
                gAMEs = gAMEs.Where(s => s.name.Contains(game));
            }
            return View(gAMEs.ToList());
        }
        public ActionResult Index(string theLoai)
        {
           
            var gAMEs = db.GAMEs.Include(g => g.USER).Include(g => g.THELOAI);
            if (theLoai != "no")
                gAMEs = db.GAMEs.Where(abc => abc.idTheLoai == theLoai);
            return View(gAMEs.ToList());
        }

        [HttpPost]
        public ActionResult Index(string theLoai,String tenGame) {
            var gAMEs = db.GAMEs.SqlQuery("SELECT * FROM GAME WHERE name like '%"+@tenGame+"%'");
            return View(gAMEs.ToList());
        }
        public ActionResult AddCart(String idgame,int gia) {
            PLV.idgame.Add(idgame);
            PLV.tongTien += gia;
            return RedirectToAction("Cart");   
        }
        public ActionResult RemoveCart(String idgame,int gia)
        {
            PLV.idgame.Remove(idgame);
            PLV.tongTien -= gia;
            return RedirectToAction("Cart");
        }
        public ActionResult Cart()
        {
            var gAMEs = db.GAMEs.Include(g => g.USER).Include(g => g.THELOAI);
            ViewBag.sodu = db.USERS.FirstOrDefault(p => p.id.ToString() == PLV.Id).sodu;
            return View(gAMEs.ToList());
        }

        // GET: GAMEs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GAME gAME = db.GAMEs.Find(id);
            if (gAME == null)
            {
                return HttpNotFound();
            }
            return View(gAME);
        }

        // GET: GAMEs/Create
        public ActionResult Create()
        {
            ViewBag.idNPH = new SelectList(db.USERS, "id", "FuName");
            ViewBag.idTheLoai = new SelectList(db.THELOAIs, "idTheLoai", "tenTheLoai");
            ViewBag.idg = db.GAMEs.Count() + 1;
            return View();
        }
        public ActionResult TrangTru() {
            return View();
        }
        // POST: GAMEs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idGame,name,moTa,gia,idNPH,idTheLoai,image_url,ngayTao,giamGia")] GAME gAME)
        {
            var imgG = Request.Files["Avatar"];
            //Lấy thông tin từ input type=file có tên Avatar
            string postedFileName = System.IO.Path.GetFileName(imgG.FileName);
            //Lưu hình đại diện về Server
            var path = Server.MapPath("/Images/" + postedFileName);
            imgG.SaveAs(path);
            if (ModelState.IsValid)
            {
                gAME.image_url = postedFileName;
                db.GAMEs.Add(gAME);
                db.SaveChanges();
                return RedirectToAction("Index",new {theLoai="no"});
               

            }

            ViewBag.idNPH = new SelectList(db.USERS, "id", "email", gAME.idNPH);
            ViewBag.idTheLoai = new SelectList(db.THELOAIs, "idTheLoai", "tenTheLoai", gAME.idTheLoai);
            return View(gAME);
        }
        public ActionResult DuaHoaDonCSDL()
        {
            MUAGAME n = new MUAGAME(PLV.MaMua, PLV.Id.AsInt(), false, "Đã thanh toán", DateTime.Now);
            db.MUAGAMEs.Add(n);
            db.SaveChanges();
            return RedirectToAction("DuaCTHoaDon");
        }
        public ActionResult DuaCTHoaDon()
        {
            for (int i = 1; i < PLV.idgame.Count(); i++) {
                int tt = db.CHITIET_MUAGAME.Count() + 1;
                string maGame = PLV.idgame[i].ToString();
                string maMua = PLV.MaMua;
               CHITIET_MUAGAME m = new CHITIET_MUAGAME();
                m.idChiTIet = tt;
                m.idGame = maGame;
                m.idMua= maMua;
                m.discount = 0;
                m.gia = db.GAMEs.FirstOrDefault(p => p.idGame == maGame).gia;
                db.CHITIET_MUAGAME.Add(m);
                db.SaveChanges();
            }
            int t1 = db.MUAGAMEs.Count() + 1;
            PLV.MaMua = "M" + t1;
            PLV.idgame.Clear();
            PLV.ListMuaGame.Clear();
            PLV.ListMaMuaBF.Clear();
            PLV.ListNgayMua.Clear();
            PLV.addgame("test");
            PLV.ListMaMuaBF.Add("test");
            PLV.ListNgayMua.Add("test");
            PLV.ListMuaGame.Add("test");
            db.USERS.FirstOrDefault(p => p.id.ToString() == PLV.Id).sodu-=PLV.tongTien;
            db.SaveChanges();
            PLV.tongTien = 0;
            foreach (var s in db.MUAGAMEs.ToList())
            {
                if (s.idND.ToString() == PLV.Id)
                    PLV.ListMuaGame.Add(s.idMua);
                PLV.ListNgayMua.Add(s.ngayMua.ToString());
            }

            foreach (var s in db.MUAGAMEs.ToList())
            {
                if (s.idND.ToString() == PLV.Id)
                    foreach (var r in db.CHITIET_MUAGAME.ToList())
                    {
                        if (r.idMua == s.idMua)
                            PLV.ListMaMuaBF.Add(r.idGame);

                    }
            }



            return View();
        }
        // GET: GAMEs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GAME gAME = db.GAMEs.Find(id);
            if (gAME == null)
            {
                return HttpNotFound();
            }
            ViewBag.idNPH = new SelectList(db.USERS, "id", "email", gAME.idNPH);
            ViewBag.idTheLoai = new SelectList(db.THELOAIs, "idTheLoai", "tenTheLoai", gAME.idTheLoai);
            return View(gAME);
        }

        // POST: GAMEs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idGame,name,moTa,gia,idNPH,idTheLoai,image_url,ngayTao,giamGia")] GAME gAME)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gAME).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idNPH = new SelectList(db.USERS, "id", "email", gAME.idNPH);
            ViewBag.idTheLoai = new SelectList(db.THELOAIs, "idTheLoai", "tenTheLoai", gAME.idTheLoai);
            return View(gAME);
        }

        // GET: GAMEs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GAME gAME = db.GAMEs.Find(id);
            if (gAME == null)
            {
                return HttpNotFound();
            }
            return View(gAME);
        }

        // POST: GAMEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            GAME gAME = db.GAMEs.Find(id);
            db.GAMEs.Remove(gAME);
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
        public ActionResult HomePage() {
            return View();
        }
        public ActionResult MyIndex()
        {

            var gAMEs = db.GAMEs.Include(g => g.USER).Include(g => g.THELOAI);
            
            return View(gAMEs.ToList());
        }
    }
}
