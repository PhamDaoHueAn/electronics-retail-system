using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDienMay.Models;


namespace WebDienMay.Extension
{
    public class AuthorizeRoleAttribute : ActionFilterAttribute
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        private readonly string[] _allowedRoles;

        public AuthorizeRoleAttribute(params string[] requiredRole)
        {
            _allowedRoles = requiredRole;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Kiểm tra nếu `idTaiKhoan` không tồn tại trong session (chưa đăng nhập)
            var TaiKhoanID = HttpContext.Current.Session["TaiKhoanID"];
            if (TaiKhoanID == null)
            {
                // Chuyển hướng đến trang đăng nhập nếu chưa đăng nhập
                filterContext.Result = new RedirectResult("~/Login/DangNhap");
                var returnUrl = filterContext.HttpContext.Request.RawUrl;
            HttpContext.Current.Session["ReturnUrl"] = returnUrl;
                return;
            }

            // Truy vấn quyền của người dùng từ cơ sở dữ liệu
            var userRole = db.TaiKhoans.FirstOrDefault(p => p.TaiKhoanID == (int)TaiKhoanID);

            // Kiểm tra quyền truy cập
            if (!_allowedRoles.Contains(userRole.VaiTro))
            {
                // Chuyển hướng đến trang lỗi hoặc thông báo không đủ quyền truy cập
                filterContext.Result = new RedirectResult("~/Error/Unauthorized");
            }

            base.OnActionExecuting(filterContext);
        }

    }
}