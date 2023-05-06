using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace odevDagitimPortali.ViewModel
{
    public class KayitModel
    {
        public int kayitId { get; set; }
        public int kayitOdevId { get; set; }
        public int kayitUyeId { get; set; }
        public int kayitDurum { get; set; }
        public OdevModel odevBilgi { get; set; }
        public UyeModel uyeBilgi { get; set; }
    }
}