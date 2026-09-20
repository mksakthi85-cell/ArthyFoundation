using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ArthyFoundation.Migrations
{
    /// <inheritdoc />
    public partial class InitialArthyDbMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TransactionId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DonorName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DonorEmail = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    SelectedCause = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ContributedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ProofImageName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    ProofDescription = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    IsVerified = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inquiries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Message = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inquiries", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "Amount", "ContributedAt", "DonorEmail", "DonorName", "IsVerified", "ProofDescription", "ProofImageName", "SelectedCause", "TransactionId" },
                values: new object[,]
                {
                    { 1, 3000.00m, new DateTime(2026, 8, 25, 10, 0, 0, 0, DateTimeKind.Utc), "amit.sharma@example.com", "Amit Sharma", true, "Rural Girls Education Kit & Uniform Distribution in West Tambaram", "shiksha-education.jpg", "Child Education Support", "TXN101" },
                    { 2, 1000.00m, new DateTime(2026, 8, 28, 14, 30, 0, 0, DateTimeKind.Utc), "sneha.k@example.com", "Sneha Krishnan", true, "V.P. Silver grocery bag distribution to local single parent nomad camps", "aahar-hunger.jpg", "Grassroots Grocery & Grain Seva", "TXN102" },
                    { 3, 4000.00m, new DateTime(2026, 9, 1, 11, 15, 0, 0, DateTimeKind.Utc), "ravi.kumar@example.com", "Ravi Kumar", true, "Heavy duty yellow waterproof tarps & bed mats handed over for monsoon shelter support", "sahaara-shelter.jpg", "Waterproof Shelter & Tarps", "TXN103" },
                    { 4, 5000.00m, new DateTime(2026, 9, 3, 16, 45, 0, 0, DateTimeKind.Utc), "rajesh.g@example.com", "Rajesh G", true, "Community food and basic amenities distribution camp inside tribal settlements", "jeevitha-community.jpg", "Tribal Community Development", "TXN104" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donations");

            migrationBuilder.DropTable(
                name: "Inquiries");
        }
    }
}
