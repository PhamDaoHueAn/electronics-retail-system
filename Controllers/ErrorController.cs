using System.Web.Mvc;

namespace WebDienMay.Controllers
{
    public class ErrorController : Controller
    {
        // Trang lỗi không có quyền truy cập
        public ActionResult Unauthorized()
        {
            return View();
        }
    }
}