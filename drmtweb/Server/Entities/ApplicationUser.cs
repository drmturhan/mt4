using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DrMturhan.Server.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class Kullanici : IdentityUser<int>
    {
        public bool IsEnabled { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        public int? KisiNo { get; set; }
        public Kisi Kisi { get; set; }
        //[StringLength(250)]
        //public string FirstName { get; set; }
        //[StringLength(250)]
        //public string LastName { get; set; }
        //[NotMapped]
        //public string Name
        //{
        //    get
        //    {
        //        return this.FirstName + " " + this.LastName;
        //    }
        //}
    }
    public class Kisi
    {
        public int KisiId { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string IkinciAd { get; set; }
        public string Unvan { get; set; }
        public DateTime DogumTarihi { get; set; }
        public int? CinsiyetNo { get; set; }
        public Cinsiyet Cinsiyet { get; set; }
        public int? MedeniHalNo { get; set; }
        public MedeniHal MedeniHal { get; internal set; }
        public bool? Aktif { get; set; }
        public virtual ICollection<Kullanici> Kullanicilari { get; set; } = new List<Kullanici>();

    }
    public class Cinsiyet
    {
        public int CinsiyetId { get; set; }
        public string CinsiyetAdi { get; set; }
        public virtual IEnumerable<Kisi> Kisiler { get; set; } = new List<Kisi>();
    }
    public class MedeniHal
    {
        public int MedeniHalId { get; set; }
        public string MedeniHalAdi { get; set; }
        public virtual IEnumerable<Kisi> Kisiler { get; set; } = new List<Kisi>();
    }


}
