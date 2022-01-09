using System.Collections.Generic;
using System.Linq;
using MyProject.Factory;
using PagedList;
namespace MyProject.Models
{
    public class Product {

        QLBanQuanAoDataContext db = new QLBanQuanAoDataContext();
        public enum CodeSP
        {
            InvalidTenSP,
            Valid
        }
        interface MVC_AddSP
        {
            CodeSP createSP(string tenSP, string moTa, string gioiTinh, decimal? giaBan, decimal? giaNhap, string anh, int? maLoaiSP, int? maNCC, int? soLuongTon);

        }
        public class Proxy : MVC_AddSP
        {
            private SanPham _sp;
            public Proxy(SanPham sp)
            {
                _sp = sp;
            }
            public CodeSP createSP(string tenSP, string moTa, string gioiTinh, decimal? giaBan, decimal? giaNhap, string anh, int? maLoaiSP, int? maNCC, int? soLuongTon)
            {
                QLBanQuanAoDataContext db = new QLBanQuanAoDataContext();

                string filePath = @"D:\MTTK\Cuoi_Ky\Do_An\MyProject\FileTXT\demo.txt";
                string[] lines;
                string str;
                string s = _sp.TenSP;
                if (System.IO.File.Exists(filePath))
                {
                    lines = System.IO.File.ReadAllLines(filePath);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (tenSP == lines[i])
                        {                          
                            return CodeSP.InvalidTenSP;
                        }
                    }
                    str = System.IO.File.ReadAllText(filePath);               
                }
                SPFactory spF = new SPFactory();
                db.SanPhams.InsertOnSubmit(spF.CreateModel(tenSP, moTa, gioiTinh, giaBan, giaNhap, anh, maLoaiSP, maNCC, soLuongTon));
                db.SubmitChanges();
                return CodeSP.Valid;
                //string notAllow = "Ho Chi Minh";

            }
            
        }
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public string Mota { get; set; }
        public string GioiTinh { get; set; }
        public decimal? GiaBan { get; set; }
        public decimal? GiaNhap { get; set; }
        public string Anh { get; set; }
        public int? MaLoaiSP { get; set; }
        public int? MaNCC { get; set; }
        public int? SoLuongTon { get; set; }

        public  IEnumerable<SanPham> ListAll(int page, int pageSize)
        {
            return db.SanPhams.OrderByDescending(n => n.MaSP).ToPagedList(page, pageSize);
        }

        public static implicit operator Product(SanPham nguoiDung)
        {
            return new Product
            {
                MaSP = nguoiDung.MaSP,
                TenSP = nguoiDung.TenSP,
                Mota = nguoiDung.MoTa,
                GioiTinh= nguoiDung.GioiTinh,
                GiaBan = nguoiDung.GiaBan,
                GiaNhap = nguoiDung.GiaNhap,
                Anh = nguoiDung.Anh,
                MaLoaiSP = nguoiDung.MaLoaiSP,
                MaNCC = nguoiDung.MaNCC,
                SoLuongTon = nguoiDung.SoLuongTon


            };
        }

        public static implicit operator SanPham(Product nguoiDung)
        {
            return new SanPham
            {
                MaSP = nguoiDung.MaSP,
                TenSP = nguoiDung.TenSP,
                MoTa = nguoiDung.Mota,
                GioiTinh = nguoiDung.GioiTinh,
                GiaBan = nguoiDung.GiaBan,
                GiaNhap = nguoiDung.GiaNhap,
                Anh = nguoiDung.Anh,
                MaLoaiSP = nguoiDung.MaLoaiSP,
                MaNCC = nguoiDung.MaNCC,
                SoLuongTon = nguoiDung.SoLuongTon
            };
        }

    }
}