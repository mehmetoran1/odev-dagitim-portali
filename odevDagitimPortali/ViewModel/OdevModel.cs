using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace odevDagitimPortali.ViewModel      
{
    public class OdevModel
    {
        public int odevId { get; set; }
        public string odevAdi { get; set; }

        public string odevAciklama { get; set; }
        public System.DateTime odevSonTarih { get; set; }
        public int odevDersId { get; set; }
        public string odevDersAdi { get; set; }
    }
}