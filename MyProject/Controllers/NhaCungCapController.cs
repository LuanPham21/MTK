using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Controllers
{
    public class NhaCungCapController : Controller
    {
        private QLBanQuanAoDataContext db = new QLBanQuanAoDataContext();

        // GET: Admin/Hangsanxuats
        public ActionResult Index(string name)
        {
            if (name == null)
            {
                return View(db.NhaCungCaps.ToList());

            }
            else
            {
                return View(db.NhaCungCaps.Where(s => s.TenNCC.Contains(name)).ToList());
            }
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
        public ActionResult Create(NhaCungCap ncc)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }
            //NhaCungCap ncc= new NhaCungCap();
            if (ncc == null)
            {
                return HttpNotFound();
            }
          
            if (ModelState.IsValid)
            {
                var check = db.NhaCungCaps.Where(s => s.TenNCC == ncc.TenNCC).FirstOrDefault();
                if (check == null)
                {
                    
                    db.NhaCungCaps.InsertOnSubmit(ncc);
                    db.SubmitChanges();
                    return RedirectToAction("Index", "nhacungcap");
                }
            }
            db.SubmitChanges();

            return View();
           


        }

        // GET: Admin/Hangsanxuats/Edit/5
        public ActionResult Edit(int id)
        {
            NhaCungCap sp = new NhaCungCap();

            return View(db.NhaCungCaps.Where(s => s.MaNCC == id).FirstOrDefault());
        }

        // POST: Admin/Hangsanxuats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NhaCungCap sp, int id)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }
            NhaCungCap sanPham = db.NhaCungCaps.FirstOrDefault(ma => ma.MaNCC == id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }

            NhaCungCap sp1 = db.NhaCungCaps.FirstOrDefault(x => x.MaNCC == id);

            if (sp1 != null)
            {
                sp1.TenNCC = sp.TenNCC;
                sp1.DiaChi = sp.DiaChi;
                sp1.SDT = sp.SDT;
                db.SubmitChanges();
                return RedirectToAction("index", "nhacungcap");
            }
            else

                return View();
        }
        public ActionResult Delete(int id)
        {
            NhaCungCap sp = new NhaCungCap();

            return View(db.NhaCungCaps.Where(s => s.MaNCC == id).FirstOrDefault());
        }
        [HttpPost]
        // GET: Admin/Hangsanxuats/Delete/5
        public ActionResult Delete(NhaCungCap sp, int id)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }
            NhaCungCap sanPham = db.NhaCungCaps.FirstOrDefault(ma => ma.MaNCC== id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            db.NhaCungCaps.DeleteOnSubmit(sanPham);
            db.SubmitChanges();
            return RedirectToAction("Index", "nhacungcap");


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
