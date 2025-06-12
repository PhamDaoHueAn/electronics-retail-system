using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDienMay.Models;
using WebDienMay.Extension;

namespace WebDienMay.Areas.Admin.Controllers
{
    [AuthorizeRole("Quản lý", "Nhân viên")]

    public class AdminTaiKhoanController : Controller
    {
        //
        // GET: /Admin/AdminTaiKhoan/
        DataClasses1DataContext db = new DataClasses1DataContext();
        public ActionResult Index(string vaiTroFilter = null)
        {
            var vaiTroList = db.TaiKhoans
                                     .Select(t => t.VaiTro)
                                     .Distinct()
                                     .ToList();
            List<TaiKhoan> dstk = new List<TaiKhoan>();
            ViewBag.VaiTroList = new SelectList(vaiTroList);
            if (!string.IsNullOrEmpty(vaiTroFilter))
            {
                dstk = db.TaiKhoans.Where(s => s.VaiTro == vaiTroFilter).ToList();
                return View(dstk);
            }
            dstk = db.TaiKhoans.ToList();  // Lấy tất cả tài khoản từ cơ sở dữ liệu
            return View(dstk);
        }

        // Thêm tài khoản
        public ActionResult Create()
        {
            TaiKhoan tkdn = db.TaiKhoans.FirstOrDefault(s => s.TaiKhoanID == (int)Session["TaiKhoanID"]);
            if (tkdn.VaiTro != "Quản lý")
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập chức năng này";
                return RedirectToAction("Index");
            }
            var vaiTroList = db.TaiKhoans
                       .Select(t => t.VaiTro)
                       .Distinct()
                       .ToList();

            // Truyền danh sách vai trò vào ViewBag
            ViewBag.VaiTroList = new SelectList(vaiTroList);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaiKhoan taiKhoan)
        {
            if (string.IsNullOrEmpty(taiKhoan.HoTen))
            {
                ModelState.AddModelError("HoTen", "Họ tên không được để trống.");
            }
            if (string.IsNullOrEmpty(taiKhoan.Email))
            {
                ModelState.AddModelError("Email", "Họ tên không được để trống.");
            }
            if (CheckEmail.IsValidEmail(taiKhoan.Email) == false)
            {
                ModelState.AddModelError("Email", "Email không hợp lệ");
            }
            if (string.IsNullOrEmpty(taiKhoan.SoDienThoai))
            {
                ModelState.AddModelError("SoDienThoai", "SDT không được để trống.");
            }
            if (string.IsNullOrEmpty(taiKhoan.VaiTro))
            {
                ModelState.AddModelError("VaiTro", "Vui lòng chọn vai trò.");
            }
            var vaiTroList = db.TaiKhoans
                       .Select(t => t.VaiTro)
                       .Distinct()
                       .ToList();
            taiKhoan.MatKhau = "123456";
            taiKhoan.NgayTao = DateTime.Now;
            taiKhoan.Active = true;
            // Truyền danh sách vai trò vào ViewBag
            ViewBag.VaiTroList = new SelectList(vaiTroList);
            if (ModelState.IsValid)
            {
                db.TaiKhoans.InsertOnSubmit(taiKhoan);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(taiKhoan);
        }

        // Sửa tài khoản
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var taiKhoan = db.TaiKhoans.FirstOrDefault(s => s.TaiKhoanID == id);
            TaiKhoan tkdn = db.TaiKhoans.FirstOrDefault(s => s.TaiKhoanID == (int)Session["TaiKhoanID"]);
            if (taiKhoan != null)
            {
                if (tkdn.VaiTro != "Quản lý" && (taiKhoan.VaiTro == "Quản lý" || taiKhoan.VaiTro == "Nhân viên"))
                {
                    TempData["ErrorMessage"] = "Bạn không có quyền truy cập chức năng này";
                    return RedirectToAction("Index");
                }
            }
            var vaiTroList = db.TaiKhoans
                       .Select(t => t.VaiTro)
                       .Distinct()
                       .ToList();

            // Truyền danh sách vai trò vào ViewBag
            ViewBag.VaiTroList = new SelectList(vaiTroList);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaiKhoan taiKhoan, int id)
        {
            if (ModelState.IsValid)
            {
                var existingTaiKhoan = db.TaiKhoans.FirstOrDefault(t => t.TaiKhoanID == id);
                if (existingTaiKhoan != null)
                {
                    existingTaiKhoan.HoTen = taiKhoan.HoTen;
                    existingTaiKhoan.Email = taiKhoan.Email;
                    existingTaiKhoan.SoDienThoai = taiKhoan.SoDienThoai;
                    existingTaiKhoan.DiaChi = taiKhoan.DiaChi;
                    existingTaiKhoan.VaiTro = taiKhoan.VaiTro;
                    existingTaiKhoan.Active = taiKhoan.Active;
                    db.SubmitChanges();  // Lưu thay đổi vào cơ sở dữ liệu
                    return RedirectToAction("Index");
                }
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // Xem tài khoản
        public ActionResult Details(int id)
        {
            var taiKhoan = db.TaiKhoans.FirstOrDefault(s => s.TaiKhoanID == id);
            TaiKhoan tkdn = db.TaiKhoans.FirstOrDefault(s => s.TaiKhoanID == (int)Session["TaiKhoanID"]);
            if (taiKhoan != null)
            {
                if (tkdn.VaiTro != "Quản lý" && (taiKhoan.VaiTro == "Quản lý" || taiKhoan.VaiTro == "Nhân viên"))
                {
                    TempData["ErrorMessage"] = "Bạn không có quyền truy cập chức năng này";
                    return RedirectToAction("Index");
                }
            }
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // Xóa tài khoản

        public ActionResult Delete(int id)
        {
            var taiKhoan = db.TaiKhoans.FirstOrDefault(s => s.TaiKhoanID == id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            TaiKhoan tkdn = db.TaiKhoans.FirstOrDefault(s => s.TaiKhoanID == (int)Session["TaiKhoanID"]);
            if (taiKhoan != null)
            {
                if (tkdn.VaiTro != "Quản lý" && (taiKhoan.VaiTro == "Quản lý" || taiKhoan.VaiTro == "Nhân viên"))
                {
                    TempData["ErrorMessage"] = "Bạn không có quyền truy cập chức năng này";
                    return RedirectToAction("Index");
                }
            }
            List<GioHang> dsgh = db.GioHangs.Where(s => s.GioHangID == id).ToList();
            List<DonHang> dsdh = db.DonHangs.Where(s => s.KhachHangID == id).ToList();

            if (dsdh.Count() != 0 || dsdh.Count() != 0)
            {
                TempData["ErrorMessage"] = "không thể xóa vì khách hàng này đã mua hàng";
                return RedirectToAction("Index");
            }
            db.TaiKhoans.DeleteOnSubmit(taiKhoan);
            TempData["SuccessMessage"] = "Xóa thành công";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Search(string keyword)
        {
            var vaiTroList = db.TaiKhoans
                                     .Select(t => t.VaiTro)
                                     .Distinct()
                                     .ToList();
            List<TaiKhoan> tk = db.TaiKhoans.Where(s => s.SoDienThoai == keyword).ToList();
            ViewBag.VaiTroList = new SelectList(vaiTroList);
            return View("Index", tk);
        }
    }
}
