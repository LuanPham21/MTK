using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Builder
{
    public class LoaiSanPhamBuilder : ILoaiSanPham
    {
        LoaiSanPham sp = new LoaiSanPham();
        public ILoaiSanPham AddTenSP(string tenHang)
        {
            sp.TenLoaiSP = tenHang;
            return this;
        }

        public LoaiSanPham Build()
        {
            return sp;
        }
    }
}