using Project_64130980.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_64130980.Controllers
{
    [AuthorizeRole_64130980("0", "1", "2")]
    public class ThongKe_64130980Controller : Controller
    {
        private Project_64130980Entities db = new Project_64130980Entities();
        public ActionResult DoanhThu()
        {
            // Lấy dữ liệu doanh thu nhóm theo tháng
            var doanhThuTheoThang = db.GioHangs
                .GroupBy(g => new { g.NgayDatHang.Month, g.NgayDatHang.Year })
                .Select(g => new
                {
                    Thang = g.Key.Month,
                    Nam = g.Key.Year,
                    TongTien = g.Sum(x => x.TongTien)
                })
                .OrderBy(x => x.Thang).ThenBy(x => x.Nam)
                .ToList();

            return Json(doanhThuTheoThang, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLowStockProducts()
        {
            var lowStockProducts = db.SanPhams
                                     .Where(p => p.SoLuong <= 10)
                                     .ToList();
            return Json(lowStockProducts, JsonRequestBehavior.AllowGet);
        }

        // GET: ThongKe_64130980
        public ActionResult Index()
        {
            var sortedModel = db.GioHangs.OrderByDescending(item => item.NgayDatHang).Take(5).ToList();
            var lowStockProducts = db.SanPhams
                          .Where(p => p.SoLuong <= 10)   // Lọc sản phẩm có số lượng <= 10
                          .OrderBy(p => p.SoLuong)       // Sắp xếp từ ít đến nhiều
                          .ToList();

            ViewBag.SortedModel = sortedModel;
            ViewBag.LowStockProducts = lowStockProducts;
            var gioHangs = db.GioHangs.AsQueryable(); // Khởi tạo queryable để có thể dễ dàng thêm filter
            return View();
        }
    }
}