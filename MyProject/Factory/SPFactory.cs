using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyProject.Models;
namespace MyProject.Factory
{
    public class SPFactory :Factory
    {
        public SanPham CreateModel(string tenSP, string moTa, string gioiTinh, decimal? giaBan, decimal? giaNhap, string anh, int? maLoaiSP, int? maNCC, int? soLuongTon)
        {
            SanPham sanPham = new SanPham();
            sanPham.TenSP = tenSP;
            sanPham.MoTa = moTa;
            sanPham.GioiTinh = gioiTinh;
            sanPham.GiaBan = giaBan;
            sanPham.GiaNhap = giaNhap;
            sanPham.Anh = anh;
            sanPham.MaLoaiSP = maLoaiSP;
            sanPham.MaNCC = maNCC;
            if (soLuongTon <= 0)
            {
                sanPham.SoLuongTon = 1;
            }
            else
            {
                sanPham.SoLuongTon = soLuongTon;
            }
            return sanPham;
        }
    }
}