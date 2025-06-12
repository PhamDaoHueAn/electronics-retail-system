using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebDienMay.Models
{
    public class ProfileViewModel
    {
        public TaiKhoan TaiKhoan { get; set; } // Thông tin tài khoản
        public List<DonHang> DonHangs { get; set; } // Danh sách đơn hàng
    }
}
