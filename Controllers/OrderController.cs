using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebDienMay.Models;
using WebDienMay.Extension;

namespace WebDienMay.Controllers
{
    [AuthorizeRole("Khách hàng", "Nhân viên")]
    public class OrderController : Controller
    {
        private readonly DataClasses1DataContext db = new DataClasses1DataContext();

        // Hiển thị trang đặt hàng
        public ActionResult DonHang()
        {
            int userId = (int)Session["TaiKhoanID"];
            var cartItems = db.GioHangs.Where(gh => gh.TaiKhoan.TaiKhoanID == userId).ToList();
            if (!cartItems.Any())
            {
                TempData["ErrorMessage"] = "Vui lòng thêm sản phẩm vào giỏ hàng để thanh toán";
                return RedirectToAction("Index", "Cart");
            }

            var user = db.TaiKhoans.FirstOrDefault(p => p.TaiKhoanID == userId);
            var orderVM = new OrderVM
            {
                giohang = cartItems,
                hoten = user?.HoTen,
                tongtien = cartItems.Sum(p => p.SoLuong * p.SanPham.Gia)
            };
            return View(orderVM);
        }

        // Xử lý đặt hàng (POST)
        [HttpPost]
        public ActionResult DonHang(OrderVM dh)
        {
            int userId = (int)Session["TaiKhoanID"];
            if (string.IsNullOrWhiteSpace(dh.hoten))
                ModelState.AddModelError("hoten", "Họ tên không được để trống.");
            if (string.IsNullOrWhiteSpace(dh.sonha))
                ModelState.AddModelError("sonha", "Địa chỉ không được để trống.");
            if (string.IsNullOrWhiteSpace(dh.tp))
                ModelState.AddModelError("tp", "Tỉnh thành không được để trống.");
            if (string.IsNullOrWhiteSpace(dh.quan))
                ModelState.AddModelError("quan", "Quận/huyện không được để trống.");
            if (string.IsNullOrWhiteSpace(dh.sdt) || dh.sdt.Length < 10 || dh.sdt.Length > 11)
                ModelState.AddModelError("sdt", "SĐT không hợp lệ.");

            var cartItems = db.GioHangs.Where(gh => gh.TaiKhoan.TaiKhoanID == userId).ToList();
            var user = db.TaiKhoans.FirstOrDefault(p => p.TaiKhoanID == userId);
            var orderVM = new OrderVM
            {
                giohang = cartItems,
                hoten = user?.HoTen,
                tongtien = cartItems.Sum(p => p.SoLuong * p.SanPham.Gia)
            };

            if (!ModelState.IsValid)
                return View(orderVM);

            var donHang = new DonHang
            {
                KhachHangID = userId,
                HoTenNguoiNhan = dh.hoten,
                NgayDat = DateTime.Now,
                SoDienThoai = dh.sdt,
                DiaChiGiao = $"{dh.sonha} - {dh.quan} - {dh.tp}",
                TongTien = cartItems.Sum(m => m.SoLuong * m.SanPham.Gia) + 15000,
                TrangThai = "Đang xử lý"
            };
            db.DonHangs.InsertOnSubmit(donHang);
            db.SubmitChanges();

            foreach (var gh in cartItems)
            {
                var ctdh = new ChiTietDonHang
                {
                    DonHangID = donHang.DonHangID,
                    SanPhamID = gh.SanPhamID,
                    SoLuong = gh.SoLuong,
                    GiaBan = gh.SanPham.Gia
                };
                var sp = db.SanPhams.FirstOrDefault(s => s.SanPhamID == gh.SanPhamID);
                if (sp != null)
                    sp.TonKho -= gh.SoLuong;
                db.ChiTietDonHangs.InsertOnSubmit(ctdh);
            }

            // Xóa giỏ hàng sau khi đặt hàng
            var cartToDelete = db.GioHangs.Where(g => g.GioHangID == userId).ToList();
            db.GioHangs.DeleteAllOnSubmit(cartToDelete);

            db.SubmitChanges();
            TempData["SuccessMessage"] = "Đặt hàng thành công! Cảm ơn bạn đã mua sắm với chúng tôi.";
            return RedirectToAction("TrangProfile", "Profile");
        }

        // Xem chi tiết đơn hàng
        public ActionResult OrderDetails(int id)
        {
            var orderDetails = db.ChiTietDonHangs.Where(p => p.DonHangID == id).ToList();
            return View(orderDetails);
        }

        // Hủy đơn hàng
        public ActionResult Huy(int id)
        {
            var donHang = db.DonHangs.FirstOrDefault(s => s.DonHangID == id);
            if (donHang != null)
            {
                donHang.TrangThai = "Đã hủy";
                var dsctdh = db.ChiTietDonHangs.Where(s => s.DonHangID == donHang.DonHangID).ToList();
                foreach (var ctdh in dsctdh)
                {
                    var sp = db.SanPhams.FirstOrDefault(s => s.SanPhamID == ctdh.SanPhamID);
                    if (sp != null)
                        sp.TonKho += ctdh.SoLuong;
                }
                db.SubmitChanges();
                TempData["SuccessMessage"] = "Hủy thành công";
            }
            else
            {
                TempData["ErrorMessage"] = "Hủy thất bại";
            }
            return RedirectToAction("TrangProfile", "Profile");
        }
    }
}