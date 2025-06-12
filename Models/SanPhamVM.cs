using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDienMay.Models
{
    public class SanPhamVM
    {
        public SanPham sp { get; set; }
        public List<SanPham> sp_tuong_tu { get; set; }
    }
}