using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Builder
{
    public interface ILoaiSanPham
    {
        ILoaiSanPham AddTenSP(string tenHang);
        LoaiSanPham Build();
    }
}