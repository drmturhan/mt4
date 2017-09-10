using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace drmtRandevu.Server.Migrations.DataLog
{
    public partial class DataLog_Baslangic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Log");

            migrationBuilder.CreateTable(
                name: "Datalogs",
                schema: "Log",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnahtarAlanAdi = table.Column<string>(nullable: true),
                    AnahtarAlanDegeri = table.Column<int>(nullable: false),
                    DegisimVerisi = table.Column<string>(nullable: true),
                    Islem = table.Column<string>(maxLength: 1, nullable: true),
                    KullaniciNo = table.Column<int>(nullable: false),
                    TabloAdi = table.Column<string>(nullable: true),
                    Tarih = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Datalogs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Datalogs_AnahtarAlanAdi",
                schema: "Log",
                table: "Datalogs",
                column: "AnahtarAlanAdi");

            migrationBuilder.CreateIndex(
                name: "IX_Datalogs_KullaniciNo",
                schema: "Log",
                table: "Datalogs",
                column: "KullaniciNo");

            migrationBuilder.CreateIndex(
                name: "IX_Datalogs_TabloAdi",
                schema: "Log",
                table: "Datalogs",
                column: "TabloAdi");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Datalogs",
                schema: "Log");
        }
    }
}
