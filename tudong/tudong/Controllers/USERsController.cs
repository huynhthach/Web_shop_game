using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using tudong.Models;
using WebGrease.Css.Extensions;

namespace tudong.Controllers
{
    public class USERsController : Controller
    {
        private readonly DbContext _dbContext; // Thay thế DbContext bằng lớp DbContext thực tế của bạn
        private QLCuaHangGameEntities4 db = new QLCuaHangGameEntities4();

        // GET: USERs
        public ActionResult Index()
        {
            
            var uSERS = db.USERS.Include(u => u.IDROLE1);

            return View(uSERS.ToList());
        }
        [HttpPost]
        public ActionResult Index(string maND)
        {
            var uSERS = db.USERS.Include(u => u.IDROLE1);

            if (!string.IsNullOrEmpty(maND))
            {
                uSERS = uSERS.Where(s => s.id.ToString().Contains(maND)||s.tentaikhoan.Contains(maND));
            }
            return View(uSERS.ToList());
        }

        // GET: USERs/Details/5
        public ActionResult Details(int? idtk)
        {
            if (idtk == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USERS.Find(idtk);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // GET: USERs/Create
        public ActionResult Create()
        {
            ViewBag.idRole = new SelectList(db.IDROLEs, "idRole1", "ten");
            int i = db.USERS.Count() + 10;
            ViewBag.iddk = ""+i;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: USERs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        public ActionResult Create([Bind(Include = "id,email,tentaikhoan,matKhau,sdt,idRole,ngaySinh,FuName,Avartar,sodu")] USER uSER)
        {
            //System.Web.HttpPostedFileBase Avatar;
            var imgNV = Request.Files["Avatar"];
            //Lấy thông tin từ input type=file có tên Avatar
            string postedFileName = System.IO.Path.GetFileName(imgNV.FileName);
            //Lưu hình đại diện về Server
            var path = Server.MapPath("/Images/" + postedFileName);
            imgNV.SaveAs(path);
            if (ModelState.IsValid)
            {
                uSER.Avartar = postedFileName;
                db.USERS.Add(uSER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idRole = new SelectList(db.IDROLEs, "idRole1", "ten", uSER.idRole);
            return View(uSER);
        }

        // GET: USERs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USERS.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            ViewBag.idRole = new SelectList(db.IDROLEs, "idRole1", "ten", uSER.idRole);
            return View(uSER);
        }

        // POST: USERs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,email,tentaikhoan,matKhau,sdt,idRole,ngaySinh,FuName,Avartar,sodu")] USER uSER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idRole = new SelectList(db.IDROLEs, "idRole1", "ten", uSER.idRole);
            return View(uSER);
        }

        // GET: USERs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USERS.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // POST: USERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            USER uSER = db.USERS.Find(id);
            db.USERS.Remove(uSER);
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
        public ActionResult Login() {
            PLV.role = null;
            PLV.addgame("test");
            PLV.MaMua = null;
            PLV.Name = null;
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginVM model)
        {
            
            if (ModelState.IsValid)
            {
               
                // Kiểm tra thông tin đăng nhập trong CSDL
                bool isValid = CheckLoginCredentials(model.Username, model.Password);

                if (isValid)
                {

                    // Đăng nhập thành công, thực hiện các tác vụ khác (ví dụ: lưu thông tin đăng nhập vào session)
                    // Chuyển hướng đến trang chính hoặc trang nào đó
                    PLV.Id = checkID(model.Username, model.Password);
                    PLV.role = db.USERS.FirstOrDefault(p => p.id.ToString() == PLV.Id).idRole.ToString();
                    PLV.Name = db.USERS.FirstOrDefault(p => p.id.ToString() == PLV.Id).FuName;
                   
                    int tt = db.MUAGAMEs.Count() + 1;
                    PLV.MaMua = "M" + tt;
                    PLV.idgame.Clear();
                    PLV.ListMuaGame.Clear();
                    PLV.ListMaMuaBF.Clear();
                    PLV.ListNgayMua.Clear();
                    PLV.addgame("test");       
                    PLV.ListMaMuaBF.Add("test");
                    PLV.ListNgayMua.Add("test");
                    PLV.ListMuaGame.Add("test");
                    PLV.tongTien = 0;
                    foreach (var s in db.MUAGAMEs.ToList())
                    {
                        if(s.idND.ToString()==PLV.Id)
                            PLV.ListMuaGame.Add(s.idMua);
                        PLV.ListNgayMua.Add(s.ngayMua.ToString());
                    }
                    
                    foreach (var s in db.MUAGAMEs.ToList()) {
                        if(s.idND.ToString()==PLV.Id)
                            foreach(var r in db.CHITIET_MUAGAME.ToList())
                            {
                                if (r.idMua == s.idMua)
                                    PLV.ListMaMuaBF.Add(r.idGame);
                            }
                    }
                    return RedirectToAction("TrangTru", "GAMEs");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không hợp lệ.");
                }
            }

            // Nếu thông tin đăng nhập không hợp lệ, hiển thị lại trang đăng nhập với thông báo lỗi
            return View(model);
        }

        private bool CheckLoginCredentials(string username, string password)
        {
            // Thực hiện kiểm tra thông tin đăng nhập trong CSDL sử dụng Entity Framework
            if (db.USERS.Any(p => p.tentaikhoan == username && p.matKhau == password))
                return true;
            return false;
        }
        private string checkID(string username, string password)
        {
            if (db.USERS.Any(p => p.tentaikhoan == username && p.matKhau == password))
            {
                var user = db.USERS.FirstOrDefault(p => p.tentaikhoan == username && p.matKhau == password);
                if (user != null)
                {
                    return user.id.ToString();
                }
            }
            return null;
        }


        public ActionResult test() {
            return View();
                }

    }
}
