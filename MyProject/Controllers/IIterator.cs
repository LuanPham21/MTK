using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyProject.Models;

namespace MyProject.Controllers
{
    public interface IIterator
    {
        GioHang First();
        GioHang Next();
        bool IsDone { get; }
        GioHang CurrentItem { get; }
        void ForEachItem(Action<GioHang> func);
    }
    public class GioHangIterator : IIterator
    {
        List<GioHang> _listGioHang;
        int current = 0;
        int step = 1;
        public GioHangIterator(List<GioHang> listGioHang)
        {
            _listGioHang = listGioHang;
        }
        public bool IsDone
        {
            get { return current >= _listGioHang.Count; }
        }

        public GioHang CurrentItem  => _listGioHang[current];


        public GioHang First()
        {
            current = 0;
            if (_listGioHang.Count > 0)
                return _listGioHang[current];
            return null;
        }

        public GioHang Next()
        {
            current += step;
            if (!IsDone)
                return _listGioHang[current];
            else
                return null;
        }
        public void ForEachItem(Action<GioHang> func)
        {
            int i = 0;
            while (i < _listGioHang.Count)
            {
                func(_listGioHang[i++]);
            }
        }
    }
}