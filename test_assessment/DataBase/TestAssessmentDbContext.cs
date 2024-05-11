
using Microsoft.EntityFrameworkCore;
using test_assessment.DataBase.Entities;

namespace test_assessment.DataBase
{
    public class TestAssessmentDbContext : DbContext
    {
        public const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=test_assessment_db;Integrated Security=True;Connect Timeout=30;";

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trip>()
                .HasKey(x => x.TripID);

            modelBuilder.Entity<Trip>()
                .HasOne(t => t.PickupLocation)
                .WithMany(l => l.TripsAsPickupLocation)
                .HasForeignKey(t => t.PULocationID);

            modelBuilder.Entity<Trip>()
                .HasOne(t => t.DropoffLocation)
                .WithMany(l => l.TripsAsDropoffLocation)
                .HasForeignKey(t => t.DOLocationID);

            modelBuilder.Entity<Trip>()
                .Property(t => t.PickupDatetime)
                .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            );

            modelBuilder.Entity<Trip>()
                .Property(t => t.DropoffDatetime)
                .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            );
        }
    }
}
