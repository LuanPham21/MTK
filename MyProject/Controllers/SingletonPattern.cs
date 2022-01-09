using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MyProject.Controllers
{
    public sealed class SingletonPattern
    {
        QLBanQuanAoDataContext db = new QLBanQuanAoDataContext();
        public IEnumerable<SanPham> ListAll(int page, int pageSize)
        {
            return list.OrderByDescending(n => n.MaSP);
        }
        public static SingletonPattern instance = new SingletonPattern();
        public List<SanPham> list { get; } = new List<SanPham>();
        public SingletonPattern() { }
        public List<SanPham> toList(QLBanQuanAoDataContext db)
        {
            var item = db.SanPhams.ToList();

            if (list.Count == 0)
            {
                foreach (var i in item)
                {
                    list.Add(i);
                }
            }
            return list;
        }
        public List<SanPham> Reset(QLBanQuanAoDataContext db)
        {
            list.Clear();
            toList(db);
            return list;

        }
    }
}