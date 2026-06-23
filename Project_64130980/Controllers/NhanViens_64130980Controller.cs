using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Project_64130980.Models;

namespace Project_64130980.Controllers
{
    
    public class NhanViens_64130980Controller : Controller
    {
        private Project_64130980Entities db = new Project_64130980Entities();

        // GET: NhanViens_64130980
        string LayMaNV()
        {
            var maMax = db.NhanViens.ToList().Select(n => n.MaNV).Max();
            int maNV = string.IsNullOrEmpty(maMax) || maMax.Length < 3 ? 1 : int.Parse(maMax.Substring(2)) + 1;
            string NV = String.Concat("00", maNV.ToString());
            return "NV" + NV.Substring(maNV.ToString().Length - 1);
        }
        public async Task<ActionResult> TimKiemNhanVien(string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                // Nếu không có từ khóa tìm kiếm, trả về toàn bộ danh sách
                return View("Index", await db.NhanViens.ToListAsync());
            }

            // Tìm kiếm theo họ tên hoặc số điện thoại
            var results = await db.NhanViens
                                  .Where(kh => kh.HoTen.Contains(searchQuery) || kh.SoDienThoai.Contains(searchQuery))
                                  .ToListAsync();

            return View("Index", results); // Sử dụng lại view Index để hiển thị kết quả
        }

        [AuthorizeRole_64130980("0")]
        public ActionResult Index()
        {
            return View(db.NhanViens.ToList());
        }

        // GET: NhanViens_64130980/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // GET: NhanViens_64130980/Create
        public ActionResult Create()
        {
            ViewBag.MaNV = LayMaNV();
            return View();
        }

        // POST: NhanViens_64130980/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNV,HoTen,GioiTinh,SoDienThoai,Email,MatKhau,QuyenSuDung")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                nhanVien.MaNV = LayMaNV();
                db.NhanViens.Add(nhanVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nhanVien);
        }

        // GET: NhanViens_64130980/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: NhanViens_64130980/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNV,HoTen,GioiTinh,SoDienThoai,Email,MatKhau,QuyenSuDung")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhanVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhanVien);
        }

        // GET: NhanViens_64130980/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: NhanViens_64130980/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NhanVien nhanVien = db.NhanViens.Find(id);
            db.NhanViens.Remove(nhanVien);
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
