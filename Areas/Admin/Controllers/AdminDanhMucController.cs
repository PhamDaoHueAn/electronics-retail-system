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

    public class AdminDanhMucController : Controller
    {
        //
        // GET: /Admin/AdminDanhMuc/
        DataClasses1DataContext db = new DataClasses1DataContext();
        public ActionResult Index()
        {
            List<DanhMuc> dsdm = db.DanhMucs.ToList();
            return View(dsdm);
        }
        public ActionResult Create()
        {
            TaiKhoan tkdn = db.TaiKhoans.FirstOrDefault(s => s.TaiKhoanID == (int)Session["TaiKhoanID"]);
            if (tkdn.VaiTro != "Quản lý")
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập chức năng này";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DanhMuc danhmuc)
        {
            
            if (string.IsNullOrEmpty(danhmuc.TenDanhMuc))
            {
                ModelState.AddModelError("TenDanhMuc", "Vui lòng nhập tên danh muc.");
            }
            if (ModelState.IsValid)
            {
                danhmuc.Active = true;
                db.DanhMucs.InsertOnSubmit(danhmuc);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(danhmuc);
        }
        [HttpGet]

        public ActionResult Edit(int id)
        {
            var danhmuc = db.DanhMucs.FirstOrDefault(s => s.DanhMucID == id);

            if (danhmuc == null)
            {
                return HttpNotFound();
            }
            return View(danhmuc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DanhMuc dm, int id)
        {
            if (ModelState.IsValid)
            {
                var existingdanhmuc = db.DanhMucs.FirstOrDefault(t => t.DanhMucID == id);
                if (existingdanhmuc != null)
                {
                    existingdanhmuc.TenDanhMuc = dm.TenDanhMuc;
                    existingdanhmuc.Active = dm.Active;
                   
                    db.SubmitChanges();  // Lưu thay đổi vào cơ sở dữ liệu
                    return RedirectToAction("Index");
                }
                return HttpNotFound();
            }
            return View(db);
        }
        public ActionResult Delete(int id)
        {
            var danhmuc = db.DanhMucs.FirstOrDefault(s => s.DanhMucID == id);
            if (danhmuc == null)
            {
                return HttpNotFound();
            }
            SanPham sp = db.SanPhams.FirstOrDefault(s => s.DanhMucID == id);
            if (sp != null)
            {
                TempData["ErrorMessage"] = "Vui lòng xóa sản phẩm trước khi xóa danh mục";
                return RedirectToAction("Index");
            }
            db.DanhMucs.DeleteOnSubmit(danhmuc);
            db.SubmitChanges();    
            TempData["SuccessMessage"] = "Xóa thành công";
            return RedirectToAction("Index");
        }

    }
}
