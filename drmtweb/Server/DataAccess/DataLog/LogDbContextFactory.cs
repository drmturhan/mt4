using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace DrMturhan.Server.DataAccess.DataLog
{
    public class LogDbContextFactory : IDbContextFactory<LogDbContext>
    {
        const string CON_STR_APP_SETTINGS_NAME = "Data:SqlServerConnectionString";

        public LogDbContext Create(DbContextFactoryOptions options)
        {
            return Create();
        }

        public static LogDbContext Create()
        {
            var path = Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder()
                          .SetBasePath(path)
                          .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            var baglantiSatiri = Startup.Configuration[CON_STR_APP_SETTINGS_NAME];
            return Create(baglantiSatiri);
        }

        private static LogDbContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException(
                $"{nameof(connectionString)} is null or empty.",
                nameof(connectionString));

            var optionsBuilder =
             new DbContextOptionsBuilder<LogDbContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return new LogDbContext(optionsBuilder.Options);
        }

    }
}