using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tudong.Models
{
    public class NguoiDungHienTai
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string IdMua { get; set; }
        public string IdNguoiDung { get; set; }
    }
}