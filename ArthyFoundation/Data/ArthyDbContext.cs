using Microsoft.EntityFrameworkCore;
using ArthyFoundation.Models;

namespace ArthyFoundation.Data
{
    public class ArthyDbContext : DbContext
    {
        public ArthyDbContext(DbContextOptions<ArthyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Inquiry> Inquiries { get; set; }
        public DbSet<DonationRecord> Donations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed default data matching your real-world images for immediate mock testing
            modelBuilder.Entity<DonationRecord>().HasData(
                new DonationRecord
                {
                    Id = 1,
                    TransactionId = "TXN101",
                    DonorName = "Amit Sharma",
                    DonorEmail = "amit.sharma@example.com",
                    Amount = 3000.00m,
                    SelectedCause = "Child Education Support",
                    ContributedAt = new DateTime(2026, 8, 25, 10, 0, 0, DateTimeKind.Utc),
                    ProofImageName = "shiksha-education.jpg",
                    ProofDescription = "Rural Girls Education Kit & Uniform Distribution in West Tambaram",
                    IsVerified = true
                },
                new DonationRecord
                {
                    Id = 2,
                    TransactionId = "TXN102",
                    DonorName = "Sneha Krishnan",
                    DonorEmail = "sneha.k@example.com",
                    Amount = 1000.00m,
                    SelectedCause = "Grassroots Grocery & Grain Seva",
                    ContributedAt = new DateTime(2026, 8, 28, 14, 30, 0, DateTimeKind.Utc),
                    ProofImageName = "aahar-hunger.jpg",
                    ProofDescription = "V.P. Silver grocery bag distribution to local single parent nomad camps",
                    IsVerified = true
                },
                new DonationRecord
                {
                    Id = 3,
                    TransactionId = "TXN103",
                    DonorName = "Ravi Kumar",
                    DonorEmail = "ravi.kumar@example.com",
                    Amount = 4000.00m,
                    SelectedCause = "Waterproof Shelter & Tarps",
                    ContributedAt = new DateTime(2026, 9, 1, 11, 15, 0, DateTimeKind.Utc),
                    ProofImageName = "sahaara-shelter.jpg",
                    ProofDescription = "Heavy duty yellow waterproof tarps & bed mats handed over for monsoon shelter support",
                    IsVerified = true
                },
                new DonationRecord
                {
                    Id = 4,
                    TransactionId = "TXN104",
                    DonorName = "Rajesh G",
                    DonorEmail = "rajesh.g@example.com",
                    Amount = 5000.00m,
                    SelectedCause = "Tribal Community Development",
                    ContributedAt = new DateTime(2026, 9, 3, 16, 45, 0, DateTimeKind.Utc),
                    ProofImageName = "jeevitha-community.jpg",
                    ProofDescription = "Community food and basic amenities distribution camp inside tribal settlements",
                    IsVerified = true
                }
            );
        }
    }
}