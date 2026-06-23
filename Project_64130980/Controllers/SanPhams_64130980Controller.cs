using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_64130980.Models;

namespace Project_64130980.Controllers
{
    [AuthorizeRole_64130980("0", "1")]
    public class SanPhams_64130980Controller : Controller
    {
        private Project_64130980Entities db = new Project_64130980Entities();

        // GET: SanPhams_64130980
        string LayMaSP()
        {
            var maMax = db.SanPhams.ToList().Select(n => n.MaSP).Max();
            int maSP = string.IsNullOrEmpty(maMax) || maMax.Length < 3 ? 1 : int.Parse(maMax.Substring(2)) + 1;
            string NV = String.Concat("00", maSP.ToString());
            return "SP" + NV.Substring(maSP.ToString().Length - 1);
        }
        public ActionResult TimKiemSanPham(string tuKhoa, string loaiSanPham)
        {
            var sanPhams = db.SanPhams.Include(s => s.LoaiSanPham).AsQueryable();

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                sanPhams = sanPhams.Where(s => s.TenSP.Contains(tuKhoa));
            }

            if (!string.IsNullOrEmpty(loaiSanPham))
            {
                sanPhams = sanPhams.Where(s => s.MaLoaiSP == loaiSanPham);
            }

            var loaiSanPhamList = db.LoaiSanPhams.Select(l => new { l.MaLoaiSP, l.TenLoaiSP }).ToList();
            ViewBag.LoaiSanPham = new SelectList(loaiSanPhamList, "MaLoaiSP", "TenLoaiSP", loaiSanPham);
            ViewBag.TuKhoa = tuKhoa;

            return View("Index", sanPhams.ToList());
        }

        public ActionResult Index()
        {
            var loaiSanPham = db.LoaiSanPhams.Select(l => new { l.MaLoaiSP, l.TenLoaiSP }).ToList();
            ViewBag.LoaiSanPham = new SelectList(loaiSanPham, "MaLoaiSP", "TenLoaiSP");
            var sanPhams = db.SanPhams.Include(s => s.LoaiSanPham).ToList();
            return View(sanPhams);
        }

        // GET: SanPhams_64130980/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams_64130980/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams, "MaLoaiSP", "TenLoaiSP");
            ViewBag.MaSP = LayMaSP();
            return View();
        }

        // POST: SanPhams_64130980/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,MoTa,SoLuong,DonViTinh,AnhSanPham,DonGia,MaLoaiSP")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                sanPham.MaSP = LayMaSP();
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams, "MaLoaiSP", "TenLoaiSP", sanPham.MaLoaiSP);
            return View(sanPham);
        }

        // GET: SanPhams_64130980/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams, "MaLoaiSP", "TenLoaiSP", sanPham.MaLoaiSP);
            return View(sanPham);
        }

        // POST: SanPhams_64130980/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,MoTa,SoLuong,DonViTinh,AnhSanPham,DonGia,MaLoaiSP")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams, "MaLoaiSP", "TenLoaiSP", sanPham.MaLoaiSP);
            return View(sanPham);
        }

        // GET: SanPhams_64130980/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams_64130980/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
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
