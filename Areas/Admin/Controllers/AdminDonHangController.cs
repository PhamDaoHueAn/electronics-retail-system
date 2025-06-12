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

    public class AdminDonHangController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        //
        // GET: /Admin/AdminDonHang/

        public ActionResult Index()
        {
            List<DonHang> dsdh = db.DonHangs.ToList();
            return View(dsdh);
        }
        public ActionResult Details(int id)
        {
            DonHang dh = db.DonHangs.FirstOrDefault(s => s.DonHangID == id);
            if (dh != null)
            {
                ViewBag.DonHang = dh;
            }
            List<ChiTietDonHang> DS_CTDH = db.ChiTietDonHangs.Where(p => p.DonHangID == id).ToList();
            return View(DS_CTDH);
        }
        public ActionResult CapNhat(int id, string status)
        {
            DonHang dh = db.DonHangs.FirstOrDefault(s => s.DonHangID == id);
            {
                if (dh != null)
                {
                    if (status == "Đã hủy")
                    {
                        dh.TrangThai = status;
                        List<ChiTietDonHang> dsctdh = db.ChiTietDonHangs.Where(s => s.DonHangID == dh.DonHangID).ToList();
                        foreach (ChiTietDonHang ctdh in dsctdh)
                        {
                            int idSP = ctdh.SanPhamID;
                            SanPham sp = db.SanPhams.FirstOrDefault(s => s.SanPhamID == idSP);
                            sp.TonKho += ctdh.SoLuong;
                        }
                        db.SubmitChanges();
                        TempData["SuccessMessage"] = "Hủy thành công";
                    }
                    else
                    {
                        dh.TrangThai = status;
                        TempData["SuccessMessage"] = "cập nhật thành công";
                        db.SubmitChanges();
                    }

                }
                else
                {
                    TempData["ErrorMessage"] = "Lỗi";

                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Search( string sdt)
        {
            List<DonHang> dsdh = db.DonHangs.Where(s => s.SoDienThoai == sdt).ToList();
            return View("Index", dsdh);
        }

    }
}
