using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrMturhan.Server.DataAccess.DataLog
{
    public class DataLog
    {
        public int Id { get; set; }
        public string TabloAdi { get; set; }
        public string AnahtarAlanAdi { get; set; }
        public int AnahtarAlanDegeri { get; set; }
        public DateTime Tarih { get; set; }
        public int KullaniciNo { get; set; }
        public string Islem { get; set; }
        public string DegisimVerisi { get; set; }
        
    }
}