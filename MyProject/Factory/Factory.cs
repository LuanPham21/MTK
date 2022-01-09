using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyProject.Models;
namespace MyProject.Factory
{ 
    public interface Factory
    {
        SanPham CreateModel(string tenSP, string moTa, string gioiTinh, decimal? giaBan, decimal? giaNhap, string anh, int? maLoaiSP, int? maNCC, int? soLuongTon);

    }
}