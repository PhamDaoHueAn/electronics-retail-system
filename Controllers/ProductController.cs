using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebDienMay.Models;

namespace WebDienMay.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataClasses1DataContext db = new DataClasses1DataContext();

        // Hiển thị chi tiết sản phẩm
        public ActionResult Details(int id)
        {
            Session["ReturnUrlAddToCart"] = Request.Url.ToString();

            var sanpham = db.SanPhams.FirstOrDefault(p => p.SanPhamID == id && p.Active);
            if (sanpham == null)
                return RedirectToAction("Unauthorized", "Error");

            var dssp = db.SanPhams
                .Where(s => s.DanhMuc.DanhMucID == sanpham.DanhMuc.DanhMucID && s.Active && s.SanPhamID != id)
                .ToList();

            var detail = new SanPhamVM
            {
                sp = sanpham,
                sp_tuong_tu = dssp
            };

            return View(detail);
        }
    }
}