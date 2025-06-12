using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebDienMay.Models;

namespace WebDienMay.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataClasses1DataContext db = new DataClasses1DataContext();

        // Trang chủ
        public ActionResult Index()
        {
            return View();
        }

        // Hiển thị danh sách sản phẩm (partial)
        public ActionResult HienThiSanPham()
        {
            Session["ReturnUrlAddToCart"] = Request.Url.ToString();
            var products = db.SanPhams.Where(s => s.Active).ToList();
            return PartialView(products);
        }

        // Hiển thị danh mục (partial)
        public ActionResult DanhMuc()
        {
            var categories = db.DanhMucs.Where(s => s.Active).ToList();
            return PartialView(categories);
        }

        // Hiển thị sản phẩm theo danh mục
        public ActionResult SanPhamTheoDanhMuc(int id)
        {
            Session["ReturnUrlAddToCart"] = Request.Url.ToString();
            var category = db.DanhMucs.FirstOrDefault(dm => dm.DanhMucID == id);
            if (category == null)
                return RedirectToAction("Unauthorized", "Error");

            var products = db.SanPhams.Where(sp => sp.DanhMucID == id && sp.Active).ToList();
            ViewBag.TenDanhMuc = category.TenDanhMuc;
            return PartialView(products);
        }

        // Hiển thị thương hiệu (partial)
        public ActionResult ThuongHieu()
        {
            var brands = db.ThuongHieus.Where(s => s.Active).ToList();
            return PartialView(brands);
        }

        // Hiển thị sản phẩm theo thương hiệu
        public ActionResult SanPhamTheoThuongHieu(int id)
        {
            Session["ReturnUrlAddToCart"] = Request.Url.ToString();
            var brand = db.ThuongHieus.FirstOrDefault(th => th.ThuongHieuID == id);
            if (brand == null)
                return RedirectToAction("Unauthorized", "Error");

            var products = db.SanPhams.Where(sp => sp.ThuongHieuID == id && sp.Active).ToList();
            ViewBag.TenDanhMuc = brand.TenThuongHieu;
            return PartialView(products);
        }

        // Tìm kiếm sản phẩm
        public ActionResult Search(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                return View("Search", null);

            var results = db.SanPhams
                .Where(sp => (sp.TenSanPham.Contains(keyword) || sp.MoTa.Contains(keyword)) && sp.Active)
                .ToList();

            return PartialView("Trangsanpham", results);
        }

        // Hiển thị trang sản phẩm, lọc theo danh mục nếu có
        public ActionResult Trangsanpham(int danhmuc = 0)
        {
            Session["ReturnUrlAddToCart"] = Request.Url.ToString();
            List<SanPham> products;
            if (danhmuc == 0)
            {
                products = db.SanPhams.Where(s => s.Active).ToList();
            }
            else
            {
                var category = db.DanhMucs.FirstOrDefault(dm => dm.DanhMucID == danhmuc);
                if (category == null)
                    return RedirectToAction("Unauthorized", "Error");

                products = db.SanPhams.Where(s => s.DanhMucID == danhmuc && s.Active).ToList();
            }
            return PartialView(products);
        }

        // Sắp xếp sản phẩm theo giá
        public ActionResult Fillter(string sortOrder)
        {
            var products = db.SanPhams.Where(s => s.Active);

            switch (sortOrder)
            {
                case "price_asc":
                    products = products.OrderBy(p => p.Gia);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.Gia);
                    break;
                default:
                    break;
            }
            return PartialView("Trangsanpham", products.ToList());
        }
    }
}