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

    public class AdminThuongHieuController : Controller
    {
        //
        // GET: /Admin/AdminThuongHieu/

        DataClasses1DataContext db = new DataClasses1DataContext();
        public ActionResult Index()
        {
            List<ThuongHieu> dsth = db.ThuongHieus.ToList();
            return View(dsth);
        }
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ThuongHieu thuonghieu)
        {

            if (string.IsNullOrEmpty(thuonghieu.TenThuongHieu))
            {
                ModelState.AddModelError("TenThuongHieu", "Vui lòng nhập tên thương hiệu");
            }
            if (ModelState.IsValid)
            {
                thuonghieu.Active = true;
                db.ThuongHieus.InsertOnSubmit(thuonghieu);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(thuonghieu);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var thuonghieu = db.ThuongHieus.FirstOrDefault(s => s.ThuongHieuID == id);

            if (thuonghieu == null)
            {
                return HttpNotFound();
            }
            return View(thuonghieu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ThuongHieu th, int id)
        {
            if (ModelState.IsValid)
            {
                var existingthuonghieu = db.ThuongHieus.FirstOrDefault(t => t.ThuongHieuID == id);
                if (existingthuonghieu != null)
                {
                    existingthuonghieu.TenThuongHieu = th.TenThuongHieu;
                    existingthuonghieu.Active = th.Active;

                    db.SubmitChanges();  // Lưu thay đổi vào cơ sở dữ liệu
                    return RedirectToAction("Index");
                }
                return HttpNotFound();
            }
            return View(db);
        }
        public ActionResult Delete(int id)
        {
            var thuonghieu = db.ThuongHieus.FirstOrDefault(s => s.ThuongHieuID == id);
            if (thuonghieu == null)
            {
                return HttpNotFound();
            }
            SanPham sp = db.SanPhams.FirstOrDefault(s => s.ThuongHieuID == id);
            if (sp != null)
            {
                TempData["ErrorMessage"] = "Vui lòng xóa sản phẩm trước khi xóa thương hiệu";
                return RedirectToAction("Index");
            }
            db.ThuongHieus.DeleteOnSubmit(thuonghieu);
            db.SubmitChanges();
            TempData["SuccessMessage"] = "Xóa thành công";
            return RedirectToAction("Index");
        }
        

    }
}
