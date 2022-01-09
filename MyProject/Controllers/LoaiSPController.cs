using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyProject.Builder;

namespace MyProject.Controllers
{
    public class LoaiSPController : Controller
    {
        private QLBanQuanAoDataContext db = new QLBanQuanAoDataContext();

        // GET: Admin/Hangsanxuats
        public ActionResult Index()
        {
            return View(db.LoaiSanPhams.ToList());
        }

        // GET: Admin/Hangsanxuats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //LoaiSanPham hangsanxuat = db.LoaiSanPhams.Find(id);
            var hangsanxuat = db.LoaiSanPhams.OrderBy(sp => sp.MaLoaiSP).ToList();

            if (hangsanxuat == null)
            {
                return HttpNotFound();
            }
            return View(hangsanxuat);
        }

        // GET: Admin/Hangsanxuats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Hangsanxuats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string ten1)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }
            if (ModelState.IsValid)
            {
                //ten = "123";
                var loai = new LoaiSanPhamBuilder().AddTenSP(ten1).Build();
                db.LoaiSanPhams.InsertOnSubmit(loai);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }

            return View();
            //LoaiSanPham sanPham = new LoaiSanPham();
            //if (sanPham == null)
            //{
            //    return HttpNotFound();
            //}
            //if(ten != "")
            //{
            //    var loai = new LoaiSPBuilder().AddTenLoaiSP(ten).Build();
            //    db.LoaiSanPhams.InsertOnSubmit(loai);
            //    db.SubmitChanges();
            //    return RedirectToAction("Index");

            //}


        }

        // GET: Admin/Hangsanxuats/Edit/5
        public ActionResult Edit(int maSP)
        {
            LoaiSanPham sp = new LoaiSanPham();

            return View(db.SanPhams.Where(s => s.MaSP == maSP).FirstOrDefault());
        }

        // POST: Admin/Hangsanxuats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LoaiSanPham sp, int maSP)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }
            LoaiSanPham sanPham = db.LoaiSanPhams.Single(ma => ma.MaLoaiSP == maSP);
            if (sanPham == null)
            {
                return HttpNotFound();
            }

            LoaiSanPham sp1 = db.LoaiSanPhams.Single(x => x.MaLoaiSP == maSP);

            if (sp1 != null)
            {
                sp1.TenLoaiSP = sp.TenLoaiSP;



                db.SubmitChanges();
                return RedirectToAction("DanhMucCacSanPham", "Admin");
            }
            return View();
        }

        // GET: Admin/Hangsanxuats/Delete/5
        public ActionResult Delete(int maSP)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }
            LoaiSanPham sanPham = db.LoaiSanPhams.Single(ma => ma.MaLoaiSP == maSP);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            db.LoaiSanPhams.DeleteOnSubmit(sanPham);
            db.SubmitChanges();
            return RedirectToAction("Index", "LoaiSP");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
