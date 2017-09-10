using DrMturhan.Server.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DrMturhan.Server
{
    public class UygulamaDbContext : IdentityDbContext<Kullanici, ApplicationRole, int>
    {
        public DbSet<Kullanici> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<Kisi> Kisiler { get; set; }
        public DbSet<Cinsiyet> Cinsiyetler { get; set; }
        public DbSet<MedeniHal> MedeniHaller { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Content> Content { get; set; }
        public DbSet<ContentText> ContentText { get; set; }
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Uyelik");
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            modelBuilder.Entity<Kullanici>(entity =>
            {
                entity.HasKey(k => k.Id);
                entity.HasOne(kullanici => kullanici.Kisi).WithMany(kisi => kisi.Kullanicilari).HasForeignKey(fk => fk.KisiNo);
            });

            modelBuilder.Entity<Kisi>(entity =>
            {
                entity.ToTable("Kisiler");
                entity.HasKey(kisi => kisi.KisiId);
                entity.HasIndex(kisi => new { kisi.Ad, kisi.Soyad, kisi.DogumTarihi }).IsUnique(true);
                entity.Property(kisi => kisi.Ad).HasMaxLength(100).IsRequired();
                entity.Property(kisi => kisi.Soyad).HasMaxLength(100).IsRequired();
                entity.Property(kisi => kisi.DogumTarihi).IsRequired();
                entity.Property(kisi => kisi.IkinciAd).HasMaxLength(100);
                entity.Property(kisi => kisi.Unvan).HasMaxLength(100);
                entity.HasMany(kisi => kisi.Kullanicilari).WithOne(kullanici => kullanici.Kisi).HasForeignKey(fk => fk.KisiNo);

            });
            modelBuilder.Entity<Cinsiyet>(entity =>
            {
                entity.ToTable("Cinsiyetler");
                entity.HasKey(kisi => kisi.CinsiyetId);
                entity.HasIndex(kisi => new { kisi.CinsiyetAdi }).IsUnique(true);
                entity.HasMany(cinsiyet => cinsiyet.Kisiler).WithOne(kisi => kisi.Cinsiyet).HasForeignKey(fk => fk.CinsiyetNo);
            });
            modelBuilder.Entity<MedeniHal>(entity =>
            {
                entity.ToTable("MedeniHaller");
                entity.HasKey(kisi => kisi.MedeniHalId);
                entity.HasIndex(kisi => new { kisi.MedeniHalAdi }).IsUnique(true);
                entity.HasMany(MedeniHal => MedeniHal.Kisiler).WithOne(kisi => kisi.MedeniHal).HasForeignKey(fk => fk.MedeniHalNo);
            });

            modelBuilder.Entity<ApplicationRole>(entity =>
            {
                entity.HasKey(k => k.Id);

            });
            modelBuilder.Entity<Language>(entity =>
            {
                entity.HasKey(k => k.Id);
                entity.Property(p => p.Locale).IsRequired().HasMaxLength(7);
                entity.Property(p => p.Description).HasMaxLength(50);
                entity.HasMany(m => m.ContentTexts).WithOne(l => l.Language).HasForeignKey(fk => fk.LanguageId);
            });
            modelBuilder.Entity<Content>(entity =>
            {
                entity.HasKey(k => k.Id);
                entity.Property(p => p.Key).IsRequired().HasMaxLength(250);
                entity.HasMany(m => m.ContentTexts).WithOne(l => l.Content).HasForeignKey(fk => fk.ContentId);
            });
            modelBuilder.Entity<ContentText>(entity =>
            {
                entity.HasKey(k => k.Id);
                entity.Property(p => p.Text).IsRequired().HasMaxLength(2048);
                entity.HasOne(p => p.Content).WithMany(m => m.ContentTexts).HasForeignKey(p => p.ContentId);
                entity.HasOne(p => p.Language).WithMany(m => m.ContentTexts).HasForeignKey(p => p.LanguageId);
            });
        }
    }
}
