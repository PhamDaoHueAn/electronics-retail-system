using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebDienMay.Extension;
using WebDienMay.Models;

namespace WebDienMay.Controllers
{
    [AuthorizeRole("Khách hàng", "Nhân viên", "Quản lý")]
    public class CartController : Controller
    {
        private readonly DataClasses1DataContext db = new DataClasses1DataContext();

        // Hiển thị giỏ hàng của người dùng hiện tại
        public ActionResult Index()
        {
            int userId = (int)Session["TaiKhoanID"];
            var cartItems = db.GioHangs.Where(gh => gh.TaiKhoan.TaiKhoanID == userId).ToList();
            return View(cartItems);
        }

        // Thêm sản phẩm vào giỏ hàng
        [HttpPost]
        public ActionResult AddToCart(int? sanphamId, int quantity = 1)
        {
            // ❗ 1. Kiểm tra đăng nhập
            if (Session["TaiKhoanID"] == null)
            {
                // Lưu thông tin để quay lại sau khi đăng nhập
                Session["ReturnUrl"] = "/Cart/AddToCart";
                Session["ReturnUrlDT"] = Request.UrlReferrer?.ToString(); // Quay lại trang sản phẩm
                TempData["ErrorMessage"] = "Bạn cần đăng nhập để thêm vào giỏ hàng.";
                return RedirectToAction("DangNhap", "Login");
            }

            // ❗ 2. Kiểm tra tham số hợp lệ
            if (sanphamId == null)
            {
                TempData["ErrorMessage"] = "Thiếu mã sản phẩm.";
                return RedirectToAction("HienThiSanPham", "Home");
            }

            int userId = (int)Session["TaiKhoanID"];
            string returnUrl = Session["ReturnUrlAddToCart"] as string;
            Session["ReturnUrlAddToCart"] = null; // Xóa sau khi dùng

            var product = db.SanPhams.FirstOrDefault(s => s.SanPhamID == sanphamId);

            if (product == null || !product.Active)
            {
                TempData["ErrorMessage"] = "Sản phẩm không tồn tại hoặc đã ngừng kinh doanh.";
                return Redirect(returnUrl ?? Url.Action("HienThiSanPham", "Home"));
            }

            var cartItem = db.GioHangs.FirstOrDefault(
                gh => gh.TaiKhoan.TaiKhoanID == userId && gh.SanPhamID == sanphamId);

            if (cartItem != null)
            {
                if (cartItem.SoLuong + quantity > product.TonKho)
                {
                    TempData["ErrorMessage"] = "Sản phẩm không đủ hàng.";
                }
                else
                {
                    cartItem.SoLuong += quantity;
                    db.SubmitChanges();
                    TempData["SuccessMessage"] = "Đã cập nhật số lượng sản phẩm trong giỏ.";
                }
            }
            else
            {
                if (quantity > product.TonKho)
                {
                    TempData["ErrorMessage"] = "Sản phẩm không đủ hàng.";
                }
                else
                {
                    var newCartItem = new GioHang
                    {
                        GioHangID = userId,
                        SanPhamID = sanphamId.Value,
                        SoLuong = quantity
                    };
                    db.GioHangs.InsertOnSubmit(newCartItem);
                    db.SubmitChanges();
                    TempData["SuccessMessage"] = "Thêm vào giỏ hàng thành công.";
                }
            }

            return Redirect(returnUrl ?? Url.Action("HienThiSanPham", "Home"));
        }


        // Tăng số lượng sản phẩm trong giỏ
        public ActionResult Increase(int sanphamId)
        {
            int userId = (int)Session["TaiKhoanID"];
            var cartItem = db.GioHangs.FirstOrDefault(
                gh => gh.TaiKhoan.TaiKhoanID == userId && gh.SanPhamID == sanphamId);
            var product = db.SanPhams.FirstOrDefault(s => s.SanPhamID == sanphamId);

            if (cartItem != null && product != null)
            {
                if (cartItem.SoLuong + 1 > product.TonKho)
                {
                    TempData["ErrorMessage"] = "Sản phẩm không đủ hàng.";
                }
                else
                {
                    cartItem.SoLuong += 1;
                    db.SubmitChanges();
                }
            }
            return RedirectToAction("Index");
        }

        // Giảm số lượng sản phẩm trong giỏ
        public ActionResult Decrease(int sanphamId)
        {
            int userId = (int)Session["TaiKhoanID"];
            var cartItem = db.GioHangs.FirstOrDefault(
                gh => gh.TaiKhoan.TaiKhoanID == userId && gh.SanPhamID == sanphamId);

            if (cartItem != null)
            {
                if (cartItem.SoLuong > 1)
                {
                    cartItem.SoLuong -= 1;
                }
                else
                {
                    db.GioHangs.DeleteOnSubmit(cartItem);
                }
                db.SubmitChanges();
            }
            return RedirectToAction("Index");
        }

        // Xóa sản phẩm khỏi giỏ hàng
        public ActionResult Remove(int sanphamId)
        {
            int userId = (int)Session["TaiKhoanID"];
            var cartItem = db.GioHangs.FirstOrDefault(
                gh => gh.TaiKhoan.TaiKhoanID == userId && gh.SanPhamID == sanphamId);

            if (cartItem != null)
            {
                db.GioHangs.DeleteOnSubmit(cartItem);
                db.SubmitChanges();
            }
            return RedirectToAction("Index");
        }

        // Đếm tổng số lượng sản phẩm trong giỏ (dùng cho icon giỏ hàng)
        public ActionResult SP_TrongGio()
        {
            if (Session["TaiKhoanID"] == null)
            {
                ViewBag.soluong = 0;
                return PartialView("SP_TrongGio");
            }

            int userId = (int)Session["TaiKhoanID"];
            int totalQuantity = db.GioHangs
                .Where(gh => gh.TaiKhoan.TaiKhoanID == userId)
                .Sum(gh => (int?)gh.SoLuong) ?? 0;

            ViewBag.soluong = totalQuantity;
            return PartialView("SP_TrongGio");
        }


    }
}