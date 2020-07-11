using EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EFCore.Data
{
    public class SamuriaContextNoTracking : DbContext
    {

        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Clan> Clans { get; set; }
        public DbSet<Battle> Battles { get; set; }

        public SamuriaContextNoTracking() 
            => ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(ConsoleLoggerFactory)
                .EnableSensitiveDataLogging(true)
                //to override maxBatchSize ,,option=>option.MaxBatchSize(Num)
                .UseSqlServer("Server=. ;Database=SamuriaApp; Trusted_Connection=True; MultipleActiveResultSets=true",
                option=>option.MaxBatchSize(100)); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SamuraiBattle>()
                .HasKey(s => new { s.BattleID , s.SamuriaID });

            modelBuilder.Entity<Horse>().ToTable("Horses");

        }

        public static  ILoggerFactory ConsoleLoggerFactory
            => LoggerFactory.Create(builder =>
            {
                builder
                .AddFilter((category, level) =>
                   category == DbLoggerCategory.Database.Command.Name
                   && level == LogLevel.Information)
                .AddConsole();
            });


    }
}
