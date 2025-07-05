using System.Linq;
using System.Web.Mvc;
using WebDienMay.Models;
using BCrypt.Net;
using System;

namespace WebDienMay.Controllers
{
    public class LoginController : Controller
    {
        private readonly DataClasses1DataContext db = new DataClasses1DataContext();

        // GET: Đăng ký
        [HttpGet]
        public ActionResult DangKi()
        {
            return View();
        }

        // POST: Đăng ký
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKi(TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra email đã tồn tại chưa
                var existingUser = db.TaiKhoans.FirstOrDefault(u => u.Email == taiKhoan.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Email này đã được sử dụng.");
                    return View(taiKhoan);
                }

                taiKhoan.VaiTro = "Khách hàng";
                taiKhoan.Active = true;
                taiKhoan.NgayTao = DateTime.Now;
                taiKhoan.MatKhau = BCrypt.Net.BCrypt.HashPassword(taiKhoan.MatKhau);
                db.TaiKhoans.InsertOnSubmit(taiKhoan);
                db.SubmitChanges();

                return RedirectToAction("DangNhap", "Login");
            }
            return View(taiKhoan);
        }

        // GET: Đăng nhập
        public ActionResult DangNhap()
        {
            return View();
        }

        // POST: Đăng nhập
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(TaiKhoan taiKhoan)
        {
            if (string.IsNullOrWhiteSpace(taiKhoan.Email))
                ModelState.AddModelError("Email", "Email không được để trống.");

            if (string.IsNullOrWhiteSpace(taiKhoan.MatKhau))
                ModelState.AddModelError("MatKhau", "Mật khẩu không được để trống.");

            if (!ModelState.IsValid)
                return View(taiKhoan);

            var user = db.TaiKhoans.FirstOrDefault(u => u.Email == taiKhoan.Email);
            if (user != null && BCrypt.Net.BCrypt.Verify(taiKhoan.MatKhau, user.MatKhau))
            {
                if (!user.Active)
                {
                    TempData["ErrorMessage"] = "Tài khoản đã bị khóa";
                    return View();
                }

                // Lưu thông tin người dùng vào session
                Session["UserEmail"] = user.Email;
                Session["TaiKhoanID"] = user.TaiKhoanID;
                Session["VaiTro"] = user.VaiTro;

                var returnUrl = Session["ReturnUrl"] as string;
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    Session.Remove("ReturnUrl");

                    // Nếu là POST thì không nên redirect về lại POST URL → redirect về trang trước đó
                    if (returnUrl == "/Cart/AddToCart")
                    {
                        string dt = Session["ReturnUrlDT"] as string;
                        Session["ReturnUrlDT"] = null;

                        if (!string.IsNullOrEmpty(dt))
                            return Redirect(dt); // redirect về trang chi tiết sản phẩm
                        else
                            return RedirectToAction("HienThiSanPham", "Home");
                    }

                    return Redirect(returnUrl);
                }


                if (user.VaiTro != "Khách hàng")
                    return Redirect("/Admin/AdminTaiKhoan");

                return RedirectToAction("HienThiSanPham", "Home");
            }

            ModelState.AddModelError("", "Email hoặc mật khẩu không đúng.");
            return View(taiKhoan);
        }

        // Đăng xuất
        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("HienThiSanPham", "Home");
        }
    }
}