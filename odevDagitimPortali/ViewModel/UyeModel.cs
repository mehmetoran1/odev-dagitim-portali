using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace odevDagitimPortali.ViewModel
{
    public class UyeModel
    {
        public int uyeId { get; set; }
        public string kullaniciAdi { get; set; }
        public string adSoyad { get; set; }
        public string email { get; set; }
        public string parola { get; set; }
        public int uyeTipi { get; set; }
    }
}