using System.Linq;
using System.Web.Mvc;
using WebDienMay.Models;

namespace WebDienMay.Controllers
{
    public class ProfileController : Controller
    {
        private readonly DataClasses1DataContext _db = new DataClasses1DataContext();

        // GET: /Profile/
        public ActionResult TrangProfile()
        {
            // Kiểm tra đăng nhập
            if (Session["UserEmail"] == null || Session["TaiKhoanID"] == null)
            {
                return RedirectToAction("DangNhap", "Login");
            }

            // Lấy ID tài khoản từ session
            int taiKhoanId = (int)Session["TaiKhoanID"];

            // Tìm tài khoản người dùng
            var taiKhoan = _db.TaiKhoans.FirstOrDefault(u => u.TaiKhoanID == taiKhoanId);
            if (taiKhoan == null)
            {
                return HttpNotFound("Không tìm thấy tài khoản.");
            }

            // Lấy danh sách đơn hàng của khách hàng
            var donHangs = _db.DonHangs
                .Where(d => d.KhachHangID == taiKhoanId)
                .OrderByDescending(d => d.NgayDat)
                .ToList();

            // Truyền model vào view
            var viewModel = new ProfileViewModel
            {
                TaiKhoan = taiKhoan,
                DonHangs = donHangs
            };

            return View(viewModel);
        }
    }
}
