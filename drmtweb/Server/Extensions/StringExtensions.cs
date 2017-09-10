using System;
using System.Text;

namespace DrMturhan.Server.Extensions
{
    public static class StringExtensions
    {
        public static string TaniOlarakDuzenle(this string taniCumlesi)
        {
            if (string.IsNullOrEmpty(taniCumlesi)) return "?";
            string[] tanilar = taniCumlesi.Split(new string[] { "\n", "\r\n", ",", }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder();
            foreach (string tani in tanilar)
            {
                sb.AppendFormat($"{tani}, ");
            }
            return $"{sb.ToString().TrimEnd(' ').TrimEnd(',')}";
        }
    }
}
