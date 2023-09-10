using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace tudong.Models
{
    public class PLV
    {
        public static String Name;
        public static String Id;
        public static String MaMua;
        public static String role;
        
        public static List<string>  idgame = new List<string>();
        public static void addgame(String name) {
            idgame.Add(name);
        }
        public static int tongTien=0;
        public static List<string> ListMaMuaBF = new List<string>();
        public static List<string> ListMuaGame = new List<string>();
        public static List<string> ListNgayMua = new List<string>();
    }
}