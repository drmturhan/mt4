
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
namespace DrMturhan.Server.Extensions
{

    public class LogInfo
    {
        public object IlkDeger { get; set; }
        public object SonDeger { get; set; }
        public LogInfo()
        {
        }
        public LogInfo(object ilkDeger, object sonDeger)
        {
            this.IlkDeger = IlkDeger;
            this.SonDeger = SonDeger;
        }
    }

    //Durum-->EntityAdi-->EntityKadi-->PropertyKaydi
    public class OzellikKaydi : Dictionary<string, LogInfo>
    {

    }
    public class EntityKaydi : SortedList<int, OzellikKaydi>
    {

    }
    public class EntityListesi : SortedList<string, EntityKaydi>
    {

    }

    public class DurumListesi : SortedList<string, EntityListesi>
    {
        Dictionary<string, int> sayaclar = new Dictionary<string, int>();
        private List<EntityEntry> sorgu;

        // EntityLsitesi->Ozellikler
        public DurumListesi(List<EntityEntry> sorgu)
        {
            this.sorgu = sorgu;
        }
        internal DurumListesi ListeyiYarat()
        {
            foreach (EntityEntry entity in sorgu)
            {
                string entityAdi = entity.Metadata.ClrType.Name;
                bool vazgecilsin = false;
                bool yeniOlarakEklensin = false;
                bool silindiOlarakEklensin = false;

                if (vazgecilsin) continue;

                string durumListesiAdi = DurumAl(entity.State);
                EntityListesi entityListesi = DurumaGoreListeyiAl(durumListesiAdi);
                EntityKaydi entityKaydi = EntityKaydiniAl(entityListesi, entityAdi);//Entity Instanci listeyse birden fazla olacak
                OzellikKaydi ozellikKaydi = new OzellikKaydi();
                entityKaydi.Add(EntityAdinaGoreSayacNumarasiAl(entityAdi), ozellikKaydi);
                foreach (var prop in entity.Properties)
                {
                    switch (entity.State)
                    {
                        case EntityState.Added:
                            yeniOlarakEklensin = true;
                            break;
                        case EntityState.Deleted:
                            silindiOlarakEklensin = true;
                            break;
                    }

                    LogInfo ozellik = new LogInfo();
                    if (!silindiOlarakEklensin)
                        ozellik.IlkDeger = prop.OriginalValue;
                    if (!yeniOlarakEklensin)
                    {
                        ozellik.SonDeger = prop.CurrentValue;
                    }
                    if (yeniOlarakEklensin)
                    {
                        if (ozellik.IlkDeger == null) continue;
                        object defaultValue = GetDefaultTypeValue(ozellik.IlkDeger);
                        if (ozellik.IlkDeger == defaultValue) continue;
                    }
                    if (!yeniOlarakEklensin && !silindiOlarakEklensin && !prop.IsModified) continue;
                    ozellikKaydi.Add(prop.Metadata.Name, ozellik);
                }
            }
            return this;
        }

        int EntityAdinaGoreSayacNumarasiAl(string entityAdi)
        {
            if (sayaclar.ContainsKey(entityAdi))
            {
                sayaclar[entityAdi] += 1;
                return sayaclar[entityAdi];
            }
            else
            {
                sayaclar.Add(entityAdi, 1);
                return 1;
            }
        }
        void EntitySayaciniSifirla(string entityAdi)
        {
            if (sayaclar.ContainsKey(entityAdi))
                sayaclar[entityAdi] = 0;
        }


        private string DurumAl(EntityState state)
        {
            switch (state)
            {
                case EntityState.Deleted:
                    return "Silinen Kayýtlar";

                case EntityState.Modified:
                    return "Deðiþen Kayýtlar";

                case EntityState.Added:
                    return "Eklenen Kayýtlar";
            }
            return null;
        }

        private LogInfo OzellikBilgisiniAl(Dictionary<string, Dictionary<string, LogInfo>> durumListesi, Dictionary<string, LogInfo> entityListesi, EntityEntry entity, PropertyEntry prop)
        {
            Dictionary<string, LogInfo> eklenecekListe = null;
            if (entityListesi.ContainsKey(prop.Metadata.Name))
            {
                eklenecekListe = new Dictionary<string, LogInfo>();
                var ad = $"{entity.Metadata.ClrType.Name}-{entityListesi.Count + 1}";
                durumListesi.Add(ad, eklenecekListe);
            }
            else
                eklenecekListe = entityListesi;
            LogInfo logInfo = new LogInfo();
            eklenecekListe.Add(prop.Metadata.Name, logInfo);
            return logInfo;
        }

        private EntityKaydi EntityKaydiniAl(EntityListesi entityListesi, string entityAdi)
        {
            if (entityListesi.ContainsKey(entityAdi)) return entityListesi[entityAdi];
            EntityKaydi yeniListe = new EntityKaydi();
            entityListesi.Add(entityAdi, yeniListe);
            return yeniListe;
        }

        private EntityListesi DurumaGoreListeyiAl(string entityState)
        {
            if (ContainsKey(entityState)) return this[entityState];
            else
            {
                EntityListesi yeniListe = new EntityListesi();
                Add(entityState, yeniListe);
                return yeniListe;
            }
        }
        public static object GetDefaultTypeValue(object nesne)
        {
            if (nesne == null) return null;
            Type tip = nesne.GetType();
            if (tip.GetTypeInfo().IsValueType)
            {
                switch (tip.GetTypeInfo().Name)
                {
                    case "sbyte":
                        return sbyte.MinValue;
                    case "SByte":
                        return SByte.MinValue;
                    case "byte":
                        return byte.MinValue;
                    case "Byte":
                        return Byte.MinValue;
                    case "short":
                        return short.MinValue;
                    case "ushort":
                        return ushort.MinValue;
                    case "int":
                        return int.MinValue;
                    case "Int16":
                        return Int16.MinValue;
                    case "Int32":
                        return Int32.MinValue;
                    case "Int64":
                        return Int64.MinValue;
                    case "uint":
                        return uint.MinValue;
                    case "UInt16":
                        return UInt16.MinValue;
                    case "UInt32":
                        return UInt32.MinValue;
                    case "UInt64":
                        return UInt64.MinValue;
                    case "long":
                        return long.MinValue;
                    case "ulong":
                        return ulong.MinValue;
                    case "boolean":
                    case "Boolean":
                        return false;
                    case "char":
                        return char.MinValue;
                    case "DateTime": return DateTime.MinValue;
                }
            }
            return null;
        }


    }

    public static class DbContextExtensions
    {

        public static DurumListesi DegisenleriModelle(this DbContext db)
        {


            var sorgu = db.ChangeTracker.Entries().Where(e => e.State != EntityState.Detached && e.State != EntityState.Unchanged).OrderBy(s => s.State).ToList();

            DurumListesi degisenler = new DurumListesi(sorgu);
            degisenler.ListeyiYarat();
            return degisenler;
        }

        //public static IQueryable<T> SiralamayiAyarla<T>(this IQueryable<T> sorgu, ISiralama siralama, Dictionary<string, Expression<Func<T, object>>> columnsMap)
        //{
        //    if (string.IsNullOrWhiteSpace(siralama.SiralamaAlani) || !columnsMap.ContainsKey(siralama.SiralamaAlani)) return sorgu;
        //    if (siralama.AsagidanYukariya)
        //        sorgu = sorgu.OrderBy(columnsMap[siralama.SiralamaAlani]);
        //    else
        //        sorgu = sorgu.OrderByDescending(columnsMap[siralama.SiralamaAlani]);
        //    return sorgu;
        //}
        //public static IQueryable<T> SayfalamayiAyarla<T>(this IQueryable<T> sorgu, ISayfalama sayfalama)
        //{
        //    if (sayfalama.Sayfa <= 0)
        //        sayfalama.Sayfa = 1;
        //    if (sayfalama.SayfaBuyuklugu == 0)
        //        sayfalama.SayfaBuyuklugu = 10;
        //    if (sayfalama.SayfaBuyuklugu > 500)
        //        sayfalama.SayfaBuyuklugu = 500;

        //    return sorgu.Skip((sayfalama.Sayfa - 1) * sayfalama.SayfaBuyuklugu).Take(sayfalama.SayfaBuyuklugu);
        //}

        public static IEnumerable<TSource> Distinct<TSource, TCompare>(
    this IEnumerable<TSource> source, Func<TSource, TCompare> selector)
        {
            return source.Distinct(new LambdaEqualityComparer<TSource, TCompare>(selector));
        }
        public static void Seed(this UygulamaDbContext context, IWebHost host)
        {
            if (context.AllMigrationsApplied())
            {
                var seed = new SeedDbData(host, context);
            }
        }

        public static bool AllMigrationsApplied(this UygulamaDbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }
    }
    public class LambdaEqualityComparer<TSource, TDest> :
    IEqualityComparer<TSource>
    {
        private Func<TSource, TDest> _selector;

        public LambdaEqualityComparer(Func<TSource, TDest> selector)
        {
            _selector = selector;
        }

        public bool Equals(TSource obj, TSource other)
        {
            return _selector(obj).Equals(_selector(other));
        }

        public int GetHashCode(TSource obj)
        {
            return _selector(obj).GetHashCode();
        }
    }

}
