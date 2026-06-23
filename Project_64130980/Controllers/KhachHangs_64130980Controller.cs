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
    [AuthorizeRole_64130980("0", "1")]
    public class KhachHangs_64130980Controller : Controller
    {
        private Project_64130980Entities db = new Project_64130980Entities();

        // GET: KhachHangs_64130980
        string LayMaKH()
        {
            var maMax = db.KhachHangs.ToList().Select(n => n.MaKH).Max();
            int maKH = string.IsNullOrEmpty(maMax) || maMax.Length < 3 ? 1 : int.Parse(maMax.Substring(2)) + 1;
            string NV = String.Concat("00", maKH.ToString());
            return "KH" + NV.Substring(maKH.ToString().Length - 1);
        }

        public async Task<ActionResult> TimKiemKhachHang(string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                // Nếu không có từ khóa tìm kiếm, trả về toàn bộ danh sách
                return View("Index", await db.KhachHangs.ToListAsync());
            }

            // Tìm kiếm theo họ tên hoặc số điện thoại
            var results = await db.KhachHangs
                                  .Where(kh => kh.HoTen.Contains(searchQuery) || kh.SoDienThoai.Contains(searchQuery))
                                  .ToListAsync();

            return View("Index", results); // Sử dụng lại view Index để hiển thị kết quả
        }

        public ActionResult Index()
        {
            return View(db.KhachHangs.ToList());
        }

        // GET: KhachHangs_64130980/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // GET: KhachHangs_64130980/Create
        public ActionResult Create()
        {
            ViewBag.MaKH = LayMaKH();
            return View();
        }

        // POST: KhachHangs_64130980/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKH,HoTen,GioiTinh,Email,SoDienThoai,DiaChi")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                khachHang.MaKH = LayMaKH();
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(khachHang);
        }

        // GET: KhachHangs_64130980/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: KhachHangs_64130980/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKH,HoTen,GioiTinh,Email,SoDienThoai,DiaChi")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khachHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(khachHang);
        }

        // GET: KhachHangs_64130980/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: KhachHangs_64130980/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            KhachHang khachHang = db.KhachHangs.Find(id);
            db.KhachHangs.Remove(khachHang);
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
