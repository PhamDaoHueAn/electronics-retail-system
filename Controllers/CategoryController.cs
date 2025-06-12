using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebDienMay.Models;

namespace WebDienMay.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DataClasses1DataContext db = new DataClasses1DataContext();

        // Hiển thị danh sách danh mục và số lượng sản phẩm từng danh mục
        public ActionResult Index()
        {
            var categories = db.DanhMucs.Select(dm => new DanhMucVM
            {
                MaDanhMuc = dm.DanhMucID,
                Tendanhmuc = dm.TenDanhMuc,
                soluong = db.SanPhams.Count(p => p.DanhMuc.DanhMucID == dm.DanhMucID && p.Active)
            }).ToList();

            return PartialView(categories);
        }
    }
}