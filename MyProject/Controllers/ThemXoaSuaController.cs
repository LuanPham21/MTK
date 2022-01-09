using MyProject.Models;
using System.Linq;
using System.Web.Mvc;
using MyProject.Extensions;
using static MyProject.Models.Product;
using MyProject.Factory;

namespace MyProject.Controllers
{
    public class ThemXoaSuaController : ControllerTemplateMethod
    {
        //
        // GET: /ThemXoaSua/
        //private readonly ILogger<ThemXoaSuaController> _logger;
        public ActionResult ThemXoaSua() {
            //PrintInformation();
            return View();
        }
        QLBanQuanAoDataContext db = new QLBanQuanAoDataContext();

        public ActionResult ThemSanPham(string tenSP, string moTa, string gioiTinh, decimal? giaBan, decimal? giaNhap, string anh, int? maLoaiSP, int? maNCC, int? soLuongTon) {
            if (Session["Admin"] == null) {
                return RedirectToAction("DangNhap", "DangNhap");
            }
            SanPham sanPham = new SanPham();
            if (sanPham == null) {
                return HttpNotFound();
            }
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams, "MaloaiSP", "TenLoaiSP");
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC");
            if (tenSP != "" && moTa != "" && gioiTinh != "" && giaBan != null && giaNhap != null && anh != "" && maLoaiSP != null && maNCC != null && soLuongTon != null) {
                /*sanPham.TenSP = tenSP;
                sanPham.MoTa = moTa;
                sanPham.GioiTinh = gioiTinh;
                sanPham.GiaBan = giaBan;
                sanPham.GiaNhap = giaNhap;
                sanPham.Anh = anh;
                sanPham.MaLoaiSP = maLoaiSP;
                sanPham.MaNCC = maNCC;
                if (soLuongTon <= 0) {
                    sanPham.SoLuongTon = 1;
                }
                else {
                    sanPham.SoLuongTon = soLuongTon;
                }*/
                //ProxySanPham proxySanPham = new ProxySanPham(sanPham);
                //var codeUpdate = proxySanPham.UpdateDatabase(_sanPham);
                //if(codeUpdate == CodeAppUser.InvalidFullName)
                //{
                //    ViewBag.TenSP = "Tên không được phép";
                //}else if (codeUpdate == CodeAppUser.InvalidBirthday)
                //{
                //    ViewBag.GiaBan = "Nhập lại giá bán";
                //}
                //else
                //{
                //    ViewBag.MaSP = "Đã được thêm";

                //}
                Proxy p = new Proxy(sanPham);
                var code = p.createSP(tenSP, moTa, gioiTinh, giaBan, giaNhap, anh, maLoaiSP, maNCC, soLuongTon);
                if (code == CodeSP.Valid)
                {
                    PrintNotiSuccess();
                    return RedirectToAction("ThemSanPham", "ThemXoaSua");


                }
                else
                {
                    //ViewBag.Error = "Tên nhập không hợp lệ";
                    PrintNotiError();                    
                    return RedirectToAction("ThemSanPham", "ThemXoaSua");
                }
                //SPFactory sp = new SPFactory();

                //db.SanPhams.InsertOnSubmit(sp.CreateModel(tenSP, moTa, gioiTinh, giaBan, giaNhap, anh, maLoaiSP, maNCC, soLuongTon));
                //db.SubmitChanges();
                //return Redirect("ThemSanPham");

            }
            db.SubmitChanges();
            return View();
        }

        public ActionResult XoaSanPham(int maSP) {
            if (Session["Admin"] == null) {
                return RedirectToAction("DangNhap", "DangNhap");
            }
            SanPham sanPham = db.SanPhams.Single(ma => ma.MaSP == maSP);
            if (sanPham == null) {
                return HttpNotFound();
            }
            db.SanPhams.DeleteOnSubmit(sanPham);
            db.SubmitChanges();
            return RedirectToAction("DanhMucCacSanPham", "Admin");
        }

        public ActionResult ChiTietSanPham(int maSP) {
            if (Session["Admin"] == null) {
                return RedirectToAction("DangNhap", "DangNhap");
            }
            SanPham sanPham = db.SanPhams.Single(ma => ma.MaSP == maSP);
            if (sanPham == null) {
                return HttpNotFound();
            }
            else {
                Product p = new Product();
                p = sanPham;
                return View(p);
            }
        }
        public ActionResult SuaSanPham(int maSP)
        {
            SanPham sp = new SanPham();

            return View(db.SanPhams.Where(s => s.MaSP == maSP).FirstOrDefault());
        }

        [ValidateInput(false)] // Cho phép nhập đoạn mã html vào csdl. Nhập đoạn mã html ở thẻ input nào thì khi binding ra giao diện nhớ ghi @Html.Raw(data hiển thị). VD: Xem ở view chi tiết chỗ @Html.Raw(Model.MoTa).
         // Do mô tả chứa đoạn mã html ( <br />) nên phải sử dụng cú pháp razor mvc @Html.Raw(). Đọc đoạn mã html
        [HttpPost]
        public ActionResult SuaSanPham(SanPham sp,int maSP)
        {
            if (Session["Admin"] == null) {
                return RedirectToAction("DangNhap", "DangNhap");
            }
            SanPham sanPham = db.SanPhams.Single(ma => ma.MaSP == maSP);
            if (sanPham == null) {
                return HttpNotFound();
            }
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams, "MaloaiSP", "TenLoaiSP");
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC");
            SanPham sp1 = db.SanPhams.Single(x => x.MaSP == maSP);

            if (sp1 != null) {
                sp1.TenSP = sp.TenSP;
                sp1.MoTa = sp.MoTa;
                sp1.GioiTinh = sp.GioiTinh;
                sp1.GiaBan = sp.GiaBan;
                sp1.GiaNhap = sp.GiaNhap;
                sp1.Anh = sp1.Anh;
                sp1.MaLoaiSP = sp1.MaLoaiSP;
                sp1.MaNCC = sp1.MaNCC;
                if (sp.SoLuongTon <= 0) {
                    sp1.SoLuongTon = 1;
                }
                else {
                    sp1.SoLuongTon = sp.SoLuongTon;
                }
                if (sp.GiaBan < sp.GiaNhap)
                {
                    this.AddNotification("Gía bán không được ít hơn giá ", NotificationType.ERROR);
                    return View();
                }
                else
                {
                    db.SubmitChanges();
                    return RedirectToAction("DanhMucCacSanPham", "Admin");
                }

            }
            return View();
        }
        public ActionResult Duplicate(int maSP)
        {
            SanPham sp = new SanPham();
            //List<MaLoaiSP> listTG = db.LoaiSanPhams.ToList();
            //ViewBag.MaTonGiao = new SelectList(db.TonGiaos, "MaTonGiao", "TenTonGiao", nv.MaTonGiao);
            //List<DanToc> listDT = db.DanTocs.ToList();
            //ViewBag.MaDanToc = new SelectList(db.DanTocs, "MaDanToc", "TenDanToc", nv.MaDanToc);
            return View(db.SanPhams.Where(s => s.MaSP == maSP).FirstOrDefault());
        }

        [ValidateInput(false)] // Cho phép nhập đoạn mã html vào csdl. Nhập đoạn mã html ở thẻ input nào thì khi binding ra giao diện nhớ ghi @Html.Raw(data hiển thị). VD: Xem ở view chi tiết chỗ @Html.Raw(Model.MoTa).
                               // Do mô tả chứa đoạn mã html ( <br />) nên phải sử dụng cú pháp razor mvc @Html.Raw(). Đọc đoạn mã html
        [HttpPost]
        public ActionResult Duplicate(SanPham sp, int maSP)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams, "MaloaiSP", "TenLoaiSP");
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC");
            SanPham sp2 = db.SanPhams.Single(x => x.MaSP == maSP);
            SanPham sp1 = new SanPham();
            if (sp1 != null)
            {
                //sp1.TenSP = sp.TenSP;
                //sp1.MoTa = sp.MoTa;
                //sp1.GioiTinh = sp.GioiTinh;
                //sp1.GiaBan = sp.GiaBan;
                //sp1.GiaNhap = sp.GiaNhap;
                //sp1.Anh = sp2.Anh;
                //sp1.MaLoaiSP = sp2.MaLoaiSP;
                //sp1.MaNCC = sp2.MaNCC;
                //if (sp.SoLuongTon <= 0)
                //{
                //    sp1.SoLuongTon = 1;
                //}
                //else
                //{
                //    sp1.SoLuongTon = sp.SoLuongTon;
                //}

                //ThemSanPham themSanPham = new ThemSanPham();
                //var themsanpham = themSanPham.Clone();
                //SanPham clonePost = new SanPham();
                //SanPham sp3 = (SanPham)themsanpham.Clone();
                //_context.Add(new PostCategory() { PostID = ((Post)clonePost).PostId, CategoryID = postCategory.CategoryID });
                //db.SanPhams.InsertOnSubmit(new SanPham() { MaSP = sp3.MaSP });
                var sp3 = sp2.Clone();
                db.SanPhams.InsertOnSubmit((SanPham)sp3);

                db.SubmitChanges();
                return RedirectToAction("DanhMucCacSanPham", "Admin");
            }
            return View();
        }

        public ActionResult timKiemSanPham(string tenSP) {
            if (Session["Admin"] == null) {
                return RedirectToAction("DangNhap", "DangNhap");
            }
            if (!string.IsNullOrEmpty(tenSP)) {
                var query = from sp in db.SanPhams where sp.TenSP.Contains(tenSP) || sp.LoaiSanPham.TenLoaiSP.Contains(tenSP) select sp;
                if (query.Count() == 0) {
                    return RedirectToAction("thongBaoRong", "ThemXoaSua");
                }
                return View(query);
            }
            return View();
        }

        public ActionResult thongBaoRong() {
            if (Session["Admin"] == null) {
                return RedirectToAction("DangNhap", "DangNhap");
            }
            ViewBag.stringEmpty = "Không tìm thấy sản phẩm";
            return View();
        }

        public ActionResult QuanLiDonHang() {
            if (Session["Admin"] == null) {
                return RedirectToAction("DangNhap", "User");
            }
            var loadData = db.ChiTietHoaDons;
            return View(loadData);
        }
        [HttpPost]
        public ActionResult DuyetDonHang(int maHD) {
            HoaDon hd = db.HoaDons.SingleOrDefault(n => n.MaHD.Equals(maHD));
            hd.TinhTrang = true;
            db.SubmitChanges();
            return RedirectToAction("QuanLiDonHang", "ThemXoaSua");
        }

        [HttpPost]
        public ActionResult HuyDH(int maHD) {
            HoaDon hd = db.HoaDons.SingleOrDefault(n => n.MaHD.Equals(maHD));
            db.HoaDons.DeleteOnSubmit(hd);
            db.SubmitChanges();
            return RedirectToAction("QuanLiDonHang", "ThemXoaSua");
        }

        public ActionResult QuanLiKhachHang() {
            ViewBag.GetList = from a in db.HoaDons
                                join b in db.KhachHangs
                                on a.MaKH equals b.MaKH
                                select new HDKhachHangModel {
                                    MaKH = b.MaKH,
                                    TenKH = b.TenKH,
                                    TaiKhoan = b.TaiKhoan,
                                    MatKhau = b.MatKhau,
                                    SoDienThoai = b.SDT,
                                    MaHD = a.MaHD,
                                    TinhTrang = (bool)a.TinhTrang,
                                };
            return View(ViewBag.GetList);
        }
        [HttpPost]
        public ActionResult XoaTaiKhoan(int maKH) {
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.MaKH.Equals(maKH));
            db.KhachHangs.DeleteOnSubmit(kh);
            db.SubmitChanges();
            return RedirectToAction("QuanLiKhachHang", "ThemXoaSua");
        }

        protected override void PrintNotiSuccess()
        {
            TempData["Success"] = "Thêm sản phẩm thành công";
        }

        protected override void PrintNotiError()
        {
            TempData["ErrorMessage"] = "Vui lòng nhập lại tên sản phẩm";
        }
    }
}
