using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MyProject.Models {
    //interface MVCEntityBlog
    //{
    //    void UpdateDatabase (UserManager<AppUser> userManager);
    //}
    public class HDKhachHangModel
    {
        public int MaKH { get; set; }
        public string TenKH { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string SoDienThoai { get; set; }
        public int MaHD { get; set; }
        public bool TinhTrang { get; set; }
    }
}