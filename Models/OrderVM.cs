using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDienMay.Models
{
    public class OrderVM
    {
        public List<GioHang> giohang { get; set; }
        public string hoten { get; set; }
        public string sdt { get; set; }
        public string sonha { get; set; }
        public string tp { get; set; }
        public string quan { get; set; }
        public double tongtien { get; set; }
    }
}