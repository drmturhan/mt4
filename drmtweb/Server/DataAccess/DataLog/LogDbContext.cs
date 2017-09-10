using DrMturhan.Server.Extensions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrMturhan.Server.DataAccess.DataLog
{
    public class LogDbContext : DbContext
    {
        public DbSet<DataLog> Datalogs { get; set; }

        public LogDbContext(DbContextOptions<LogDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            modelBuilder.Entity<DataLog>(entity =>
            {
                entity.ToTable("Datalogs", "Log");
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Islem).HasMaxLength(1);//E:Ekleme, Ç:Çıkarma, S:Silme, D:Degistirme
                entity.HasIndex(k => new { k.TabloAdi }).IsUnique(false);
                entity.HasIndex(k => new { k.AnahtarAlanAdi }).IsUnique(false);
                entity.HasIndex(k => new { k.KullaniciNo }).IsUnique(false);
            });
        }
        public void Logla(string tabloAdi, string anahtarAlanAdi, int anahtarAlanDegeri, int kullaniciNo, string islem, DurumListesi degisenler = null, DateTime? tarih = null)
        {
            if (degisenler.Count == 0) return;
            if (tarih == null)
                tarih = DateTime.Now;
            string degisimVerisi = string.Empty;
            if (degisenler != null)
                degisimVerisi = DegisimBilgisiniUret(degisenler);
            if (degisimVerisi == "{}") return;
            DataLog yeni = new DataLog { TabloAdi = tabloAdi, AnahtarAlanAdi = anahtarAlanAdi, AnahtarAlanDegeri = anahtarAlanDegeri, KullaniciNo = kullaniciNo, Islem = islem, Tarih = DateTime.Now };
            if (!string.IsNullOrEmpty(degisimVerisi))
                yeni.DegisimVerisi = degisimVerisi;
            Datalogs.Add(yeni);
            int sonuc = SaveChanges();
        }

        private string DegisimBilgisiniUret(DurumListesi degisenler)
        {
            string content = JsonConvert.SerializeObject(degisenler,
                        new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            return content;

        }

        internal void Logla(object tABLE_NAME, object pRIMARY_KEY_NAME, object postaId, object p, string v, object degisenler)
        {
            throw new NotImplementedException();
        }
    }
}