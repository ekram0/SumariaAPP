using EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFCore.Data
{
    public class SamuriaContext : DbContext
    {
        private readonly DbContextOptions<SamuriaContext> dbContext;
        private DbContextOptions options;

        public SamuriaContext()
        {
        }

        public SamuriaContext(DbContextOptions<SamuriaContext> dbContext)
            : base(dbContext)
        {
            //when dealing w/ api=> for fast trip
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public SamuriaContext(DbContextOptions options)
        {
            this.options = options;
        }

        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Clan> Clans { get; set; }
        public DbSet<Battle> Battles { get; set; }
        public DbSet<SamuraiBattleStat> SamuraiBattleStats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder
            //    .UseLoggerFactory(ConsoleLoggerFactory)
            //    .EnableSensitiveDataLogging(true)
            //    //to override maxBatchSize ,,option=>option.MaxBatchSize(Num)
            //    .UseSqlServer("Server=. ;Database=SamuriaApp; Trusted_Connection=True; MultipleActiveResultSets=true",
            //    option => option.MaxBatchSize(100));

            // // for testing
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                  .UseSqlServer("Server=. ;Database=TestSamuriaApp; Trusted_Connection=True; MultipleActiveResultSets=true",
                  option => option.MaxBatchSize(100));

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SamuraiBattle>()
                .HasKey(s => new { s.BattleID, s.SamuriaID });

            modelBuilder.Entity<Horse>().ToTable("Horses");

            modelBuilder.Entity<SamuraiBattleStat>()
                .HasNoKey().ToView("SamuraiBattleStats");

        }

        public static ILoggerFactory ConsoleLoggerFactory
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
