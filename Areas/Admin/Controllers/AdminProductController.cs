using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDienMay.Models;
using System.IO;
using WebDienMay.Extension;
namespace WebDienMay.Areas.Admin.Controllers
{
    [AuthorizeRole("Quản lý", "Nhân viên")]

    public class AdminProductController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        //
        // GET: /Admin/AdminProduct/

        public ActionResult Index(string ActiveFilter = null)
        {
            var statusList = new List<SelectListItem>
        {
            new SelectListItem { Value = "0", Text = "Bị khóa" },
            new SelectListItem { Value = "1", Text = "Hoạt động" }
        };
            ViewBag.ActiveList = new SelectList(statusList, "Value", "Text");

            List<SanPham> dssp = new List<SanPham>();
            if(ActiveFilter == null)
            {
                dssp = db.SanPhams.ToList();
            }
            if (ActiveFilter == "1")
            {
                dssp = db.SanPhams.Where(s => s.Active== true).ToList();
            }
            if (ActiveFilter == "0")
            {
                dssp = db.SanPhams.Where(s => s.Active == false).ToList();
            }
            
            return View(dssp);
        }

        //
        // GET: /Admin/AdminProduct/Details/5

        public ActionResult Details(int id)
        {
            SanPham sp = db.SanPhams.FirstOrDefault(s => s.SanPhamID == id);
            return View(sp);
        }

        //
        // GET: /Admin/AdminProduct/Create

        public ActionResult Create()
        {

            ViewData["DanhMuc"] = new SelectList(db.DanhMucs, "DanhMucID", "TenDanhMuc");


            ViewData["ThuongHieu"] = new SelectList(db.ThuongHieus, "ThuongHieuID", "TenThuongHieu");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SanPham sanPham, HttpPostedFileBase HinhAnhFile)
        {

            if (string.IsNullOrEmpty(sanPham.TenSanPham))
            {
                ModelState.AddModelError("TenSanPham", "Tên sản phẩm không được để trống.");
            }

            if (sanPham.DanhMucID == 0)
            {
                ModelState.AddModelError("DanhMucID", "Vui lòng chọn danh mục.");
            }

            if (sanPham.ThuongHieuID == 0)
            {
                ModelState.AddModelError("ThuongHieuID", "Vui lòng chọn thương hiệu.");
            }

            if (sanPham.Gia <= 0)
            {
                ModelState.AddModelError("Gia", "Giá sản phẩm phải là số dương.");
            }

            if (sanPham.TonKho < 0)
            {
                ModelState.AddModelError("TonKho", "Số lượng tồn kho phải là số dương.");
            }

            if (string.IsNullOrEmpty(sanPham.MoTa))
            {
                ModelState.AddModelError("MoTa", "Mô tả không được để trống.");
            }

            if (string.IsNullOrEmpty(sanPham.CongSuat))
            {
                ModelState.AddModelError("CongSuat", "Công suất không được để trống.");
            }
            if (sanPham.TenSanPham != null && sanPham.DanhMucID != 0 && sanPham.ThuongHieuID != 0 && sanPham.MoTa != null && sanPham.CongSuat != null && sanPham.TonKho > 0 && sanPham.Gia > 0)
            {
                // Xử lý hình ảnh
                if (HinhAnhFile != null && HinhAnhFile.ContentLength > 0)
                {
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
                    string fileExtension = "";

                    if (HinhAnhFile.FileName != null)
                    {
                        fileExtension = Path.GetExtension(HinhAnhFile.FileName).ToLower();
                    }

                    // Kiểm tra định dạng file
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError("", "Chỉ chấp nhận các định dạng ảnh: jpg, jpeg, png, gif.");
                        ReloadDropDownLists();
                        return View(sanPham);
                    }

                    // Tạo tên file ngẫu nhiên
                    string fileName = string.Format("{0}{1}", Guid.NewGuid(), fileExtension);
                    string filePath = Path.Combine(Server.MapPath("~/Content/IMG"), fileName);
                    // Lưu file vào thư mục
                    HinhAnhFile.SaveAs(filePath);
                    sanPham.HinhAnh = fileName;
                    sanPham.Active = true;
                }

                // Thêm sản phẩm vào cơ sở dữ liệu
                db.SanPhams.InsertOnSubmit(sanPham);
                db.SubmitChanges();

                // Sau khi sản phẩm đã được thêm vào CSDL, cập nhật lại tên ảnh với SanPhamID
                if (!string.IsNullOrEmpty(sanPham.HinhAnh))
                {
                    // Tạo tên file mới theo SanPhamID
                    string newFileName = string.Format("{0}{1}", sanPham.SanPhamID, Path.GetExtension(sanPham.HinhAnh));
                    string newFilePath = Path.Combine(Server.MapPath("~/Content/IMG"), newFileName);

                    // Lấy đường dẫn cũ của ảnh
                    string oldFilePath = Path.Combine(Server.MapPath("~/Content/IMG"), sanPham.HinhAnh);

                    // Di chuyển ảnh từ tên file cũ sang tên file mới
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Move(oldFilePath, newFilePath);
                        // Cập nhật tên ảnh trong CSDL
                        sanPham.HinhAnh = newFileName;
                        db.SubmitChanges();
                    }
                }

                // Chuyển hướng về trang Index sau khi tạo sản phẩm thành công
                return RedirectToAction("Index");
            }

            // Nếu ModelState không hợp lệ, load lại dropdown
            ReloadDropDownLists();
            return View(sanPham);
        }

        private void ReloadDropDownLists()
        {
            ViewData["DanhMuc"] = new SelectList(db.DanhMucs, "DanhMucID", "TenDanhMuc");


            ViewData["ThuongHieu"] = new SelectList(db.ThuongHieus, "ThuongHieuID", "TenThuongHieu");
        }





        //
        // POST: /Admin/AdminProduct/Create


        //
        // GET: /Admin/AdminProduct/Edit/5

        public ActionResult Edit(int id)
        {
            var sanPham = db.SanPhams.FirstOrDefault(sp => sp.SanPhamID == id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewData["DanhMuc"] = new SelectList(db.DanhMucs, "DanhMucID", "TenDanhMuc", sanPham.DanhMucID);
            ViewData["ThuongHieu"] = new SelectList(db.ThuongHieus, "ThuongHieuID", "TenThuongHieu", sanPham.ThuongHieuID);

            return View(sanPham);
        }

        //
        // POST: /Admin/AdminProduct/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, SanPham sanPham, HttpPostedFileBase HinhAnhFile)
        {
            if (ModelState.IsValid)
            {
                var existingSanPham = db.SanPhams.FirstOrDefault(sp => sp.SanPhamID == id);
                if (existingSanPham == null)
                {
                    return HttpNotFound();
                }

                // Kiểm tra nếu có hình ảnh mới
                if (HinhAnhFile != null && HinhAnhFile.ContentLength > 0)
                {
                    // Xóa hình ảnh cũ nếu tồn tại
                    if (!string.IsNullOrEmpty(existingSanPham.HinhAnh))
                    {
                        string oldFilePath = Path.Combine(Server.MapPath("~/Content/IMG"), existingSanPham.HinhAnh);
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath); // Xóa ảnh cũ
                        }
                    }

                    // Xử lý hình ảnh mới
                    string fileExtension = Path.GetExtension(HinhAnhFile.FileName).ToLower();
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError("HinhAnh", "Chỉ chấp nhận các định dạng ảnh: jpg, jpeg, png, gif.");
                        return View(sanPham);
                    }

                    string fileName = string.Format("{0}{1}", Guid.NewGuid(), fileExtension);
                    string filePath = Path.Combine(Server.MapPath("~/Content/IMG"), fileName);
                    HinhAnhFile.SaveAs(filePath);
                    existingSanPham.HinhAnh = fileName; // Cập nhật tên ảnh mới vào sản phẩm
                }

                // Cập nhật các thông tin khác của sản phẩm
                existingSanPham.TenSanPham = sanPham.TenSanPham;
                existingSanPham.Gia = sanPham.Gia;
                existingSanPham.MoTa = sanPham.MoTa;
                existingSanPham.Active = sanPham.Active;
                // Cập nhật thêm các trường khác nếu cần

                // Lưu thay đổi
                db.SubmitChanges();

                return RedirectToAction("Index"); // Chuyển hướng về trang danh sách sản phẩm
            }
            ViewData["DanhMuc"] = new SelectList(db.DanhMucs, "DanhMucID", "TenDanhMuc", sanPham.DanhMucID);
            ViewData["ThuongHieu"] = new SelectList(db.ThuongHieus, "ThuongHieuID", "TenThuongHieu", sanPham.ThuongHieuID);

            return View(sanPham);
        }


        //
        // GET: /Admin/AdminProduct/Delete/5

        public ActionResult Delete(int id)
        {
            List<GioHang> gh = db.GioHangs.Where(s => s.SanPhamID == id).ToList();
            List<ChiTietDonHang> dh = db.ChiTietDonHangs.Where(s => s.SanPhamID == id).ToList();
            SanPham sp = db.SanPhams.FirstOrDefault(s => s.SanPhamID == id);
            if (sp != null)
            {
                if(dh.Count() != 0 || gh.Count() != 0)
                {
                    TempData["ErrorMessage"] = "không thể xóa vì sản phẩm này đã được mua";
                    return RedirectToAction("Index");
                }
                db.SanPhams.DeleteOnSubmit(sp);
                TempData["SuccessMessage"] = "Xóa thành công";
            }

            return RedirectToAction("Index");
        }

        //
        // POST: /Admin/AdminProduct/Delete/5

        
        [HttpPost]
        public ActionResult Search(string keyword)
        {
            SanPham sp = db.SanPhams.FirstOrDefault(s => s.TenSanPham.Contains(keyword));
            return View(sp);
        }
        public bool QuanLy()
        {
            TaiKhoan tkdn = db.TaiKhoans.FirstOrDefault(s => s.TaiKhoanID == (int)Session["TaiKhoanID"]);
            if (tkdn.VaiTro != "Quản lý")
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập chức năng này";
                return false;
            }
            return true;
        }
    }
}
