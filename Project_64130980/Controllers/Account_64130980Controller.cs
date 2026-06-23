using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Project_64130980.Models;

namespace Project_64130980.Controllers
{
    public class Account_64130980Controller : Controller
    {
        private Project_64130980Entities db = new Project_64130980Entities();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            var nhanVien = db.NhanViens.FirstOrDefault(n => n.SoDienThoai == username && n.MatKhau == password);
            if (nhanVien != null)
            {
                Session["UserID"] = nhanVien.MaNV;
                Session["Username"] = nhanVien.HoTen;
                Session["Role"] = nhanVien.QuyenSuDung;

                return RedirectToAction("Index", "ThongKe_64130980");
            }

            ViewBag.ErrorMessage = "Sai số điện thoại hoặc mật khẩu.";
            return View();
        }

        // GET: Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}