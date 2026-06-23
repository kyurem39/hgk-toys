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
    [AuthorizeRole_64130980("0", "1", "2")]
    public class GioHangs_64130980Controller : Controller
    {
        private Project_64130980Entities db = new Project_64130980Entities();

        // GET: GioHangs_64130980
        string LaySoHD()
        {
            var maMax = db.GioHangs.ToList().Select(n => n.SoHD).Max();
            int maHD = string.IsNullOrEmpty(maMax) || maMax.Length < 3 ? 1 : int.Parse(maMax.Substring(2)) + 1;
            string NV = String.Concat("00", maHD.ToString());
            return "HD" + NV.Substring(maHD.ToString().Length - 1);
        }

        public ActionResult Index(int? TinhTrang)
        {
            var gioHangs = db.GioHangs.AsQueryable(); // Khởi tạo queryable để có thể dễ dàng thêm filter

            // Nếu có giá trị TinhTrang, áp dụng lọc
            if (TinhTrang.HasValue)
            {
                gioHangs = gioHangs.Where(g => g.TinhTrang == TinhTrang.Value);
            }

            return View(gioHangs.ToList());
        }
        [AuthorizeRole_64130980("0", "1", "2")]
        // GET: GioHangs_64130980/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            GioHang gioHang = db.GioHangs
                .Include(d => d.ChiTietGioHangs) // Bao gồm chi tiết đơn hàng
                .FirstOrDefault(d => d.SoHD == id);

            if (gioHang == null)
            {
                return HttpNotFound();
            }
            return View(gioHang);
        }
        [AuthorizeRole_64130980("0", "1")]
        // GET: GioHangs_64130980/Create
        public ActionResult Create()
        {
            ViewBag.SoHD = LaySoHD();
            ViewBag.NgayDatHang = DateTime.Now;
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoTen");
            ViewBag.MaNVGiaoHang = new SelectList(db.NhanViens, "MaNV", "HoTen");
            ViewBag.ListSanPham = new SelectList(db.SanPhams, "MaSP", "TenSP", "SoLuong");
            return View();
        }

        // POST: GioHangs_64130980/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(List<ChiTietGioHang> chiTietGioHangs, [Bind(Include = "SoHD,NgayDatHang,MaKH,MaNVGiaoHang,LoaiThanhToan,TinhTrang")] GioHang gioHang)
        {
            if (ModelState.IsValid)
            {
                gioHang.SoHD = LaySoHD();
                gioHang.NgayDatHang = DateTime.Now;
                db.GioHangs.Add(gioHang);
                db.SaveChanges();

                // Thêm các chi tiết đơn hàng
                foreach (var chiTiet in chiTietGioHangs)
                {
                    var sanPham = db.SanPhams.Find(chiTiet.MaSP);
                    if (sanPham != null)
                    {
                        chiTiet.SoHD = gioHang.SoHD;
                        chiTiet.DonGiaBan = sanPham.DonGia;
                        db.ChiTietGioHangs.Add(chiTiet);

                        // Cập nhật tổng tiền và số lượng tồn
                        gioHang.TongTien += chiTiet.DonGiaBan * chiTiet.SoLuong;
                        sanPham.SoLuong -= chiTiet.SoLuong;

                        if (sanPham.SoLuong < 0)
                        {
                            ModelState.AddModelError("", "Số lượng tồn kho không đủ cho sản phẩm: " + sanPham.TenSP);
                            return View(gioHang);
                        }
                    }
                }
                db.Entry(gioHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoTen", gioHang.MaKH);
            ViewBag.MaNVGiaoHang = new SelectList(db.NhanViens, "MaNV", "HoTen", gioHang.MaNVGiaoHang);
            ViewBag.ListSanPham = new SelectList(db.SanPhams, "MaSP", "TenSP", "SoLuong");
            return View(gioHang);
        }
        public JsonResult GetGiaBan(string maSP)
        {
            var giaBan = db.SanPhams.Where(t => t.MaSP == maSP).Select(t => t.DonGia).FirstOrDefault();
            return Json(giaBan, JsonRequestBehavior.AllowGet);
        }
        [AuthorizeRole_64130980("0", "1")]
        // GET: GioHangs_64130980/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GioHang gioHang = db.GioHangs
                .Include(d => d.ChiTietGioHangs) // Bao gồm chi tiết đơn hàng
                .FirstOrDefault(d => d.SoHD == id);
            if (gioHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoTen", gioHang.MaKH);
            ViewBag.MaNVGiaoHang = new SelectList(db.NhanViens, "MaNV", "HoTen", gioHang.MaNVGiaoHang);
            ViewBag.ListSanPham = new SelectList(db.SanPhams, "MaSP", "TenSP");
            return View(gioHang);
        }

        // POST: GioHangs_64130980/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(List<ChiTietGioHang> chiTietGioHangs, [Bind(Include = "SoHD,NgayDatHang,MaKH,MaNVGiaoHang,LoaiThanhToan,TinhTrang")] GioHang gioHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gioHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoTen", gioHang.MaKH);
            ViewBag.MaNVGiaoHang = new SelectList(db.NhanViens, "MaNV", "HoTen", gioHang.MaNVGiaoHang);
            ViewBag.ListSanPham = new SelectList(db.SanPhams, "MaSP", "TenSP");
            return View(gioHang);
        }
        [AuthorizeRole_64130980("0", "1")]
        // GET: GioHangs_64130980/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GioHang gioHang = db.GioHangs.Find(id);
            if (gioHang == null)
            {
                return HttpNotFound();
            }
            return View(gioHang);
        }

        // POST: GioHangs_64130980/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            GioHang gioHang = db.GioHangs.Find(id);
            db.GioHangs.Remove(gioHang);
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
