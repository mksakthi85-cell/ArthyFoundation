using System;
using System.ComponentModel.DataAnnotations;

namespace ArthyFoundation.Models
{
    public class DonationRecord
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string TransactionId { get; set; } = string.Empty; // e.g., TXN101, TXN102

        [Required]
        [StringLength(100)]
        public string DonorName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string DonorEmail { get; set; } = string.Empty;

        [Required]
        [Range(1.0, 1000000.0)]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(100)]
        public string SelectedCause { get; set; } = string.Empty;

        public DateTime ContributedAt { get; set; } = DateTime.UtcNow;

        // Visual Proof System matching your actual field pictures
        [StringLength(150)]
        public string ProofImageName { get; set; } = string.Empty; // e.g., "shiksha-education.jpg"

        [StringLength(200)]
        public string ProofDescription { get; set; } = string.Empty; // e.g., "Handing school supplies to students in West Tambaram"

        public bool IsVerified { get; set; } = true;
    }
}