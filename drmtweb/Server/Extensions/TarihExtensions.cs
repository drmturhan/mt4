using System;
using System.Globalization;

namespace DrMturhan.Server.Extensions
{
    public static class TarihExtensions
    {
        public static string TarihtenSaatiAyir(this DateTime tarih)
        {
            return tarih.ToString("HH:mm:ss");
        }
        public static DateTime SaatleBirlestir(this DateTime tarih, string saatCumlesi)
        {
            int saat = 0;
            int dakika = 0;
            int saniye = 0;
            string[] saatDilimleri = saatCumlesi.Split(':');
            if (saatDilimleri.Length == 0) return tarih;
            switch (saatDilimleri.Length)
            {
                case 1:
                    saat = Convert.ToInt32(saatDilimleri[0]);
                    break;
                case 2:
                    saat = Convert.ToInt32(saatDilimleri[0]);
                    dakika = Convert.ToInt32(saatDilimleri[1]);
                    break;
                default:
                    saat = Convert.ToInt32(saatDilimleri[0]);
                    dakika = Convert.ToInt32(saatDilimleri[1]);
                    saniye = Convert.ToInt32(saatDilimleri[2]);
                    break;
            }
            return new DateTime(tarih.Year, tarih.Month, tarih.Day, saat, dakika, saniye);
        }

        public static DateTime GunBasi(this DateTime tarih)
        {
            return new DateTime(tarih.Year, tarih.Month, tarih.Day, 0, 0, 0);
        }
        public static DateTime GunSonu(this DateTime tarih)
        {

            return new DateTime(tarih.Year, tarih.Month, tarih.Day, 23, 59, 59);
        }
        public static DateTime HaftaBasi(this DateTime dt)
        {
            CultureInfo ci = CultureInfo.CurrentCulture;
            DayOfWeek fdow = ci.DateTimeFormat.FirstDayOfWeek;
            DayOfWeek today = DateTime.Now.DayOfWeek;
            DateTime sow = DateTime.Now.AddDays(-(today - fdow)).Date;
            return sow.GunBasi();
        }
        public static DateTime HaftaSonu(this DateTime tarih)
        {
            return tarih.HaftaBasi().GunSonu().AddDays(6); ;
        }
        public static DateTime AyBasi(this DateTime tarih)
        {
            return new DateTime(tarih.Year, tarih.Month, 1).GunBasi();
        }
        public static DateTime AySonu(this DateTime tarih)
        {
            return new DateTime(tarih.Year, tarih.Month, DateTime.DaysInMonth(tarih.Year, tarih.Month)).GunSonu();
        }

    }
}