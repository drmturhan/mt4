using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DrMturhan.Server.DataAccess.DataLog;

namespace drmtRandevu.Server.Migrations.DataLog
{
    [DbContext(typeof(LogDbContext))]
    [Migration("20170910201849_DataLog_Baslangic")]
    partial class DataLog_Baslangic
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DrMturhan.Server.DataAccess.DataLog.DataLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnahtarAlanAdi");

                    b.Property<int>("AnahtarAlanDegeri");

                    b.Property<string>("DegisimVerisi");

                    b.Property<string>("Islem")
                        .HasMaxLength(1);

                    b.Property<int>("KullaniciNo");

                    b.Property<string>("TabloAdi");

                    b.Property<DateTime>("Tarih");

                    b.HasKey("Id");

                    b.HasIndex("AnahtarAlanAdi");

                    b.HasIndex("KullaniciNo");

                    b.HasIndex("TabloAdi");

                    b.ToTable("Datalogs","Log");
                });
        }
    }
}
