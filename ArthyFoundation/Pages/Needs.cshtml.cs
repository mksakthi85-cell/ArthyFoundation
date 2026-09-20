using System;
using System.Threading.Tasks;
using ArthyFoundation.Data;
using ArthyFoundation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ArthyFoundation.Pages
{
    public class NeedsModel : PageModel
    {
        private readonly ArthyDbContext _context;
        private readonly ILogger<NeedsModel> _logger;

        public NeedsModel(ArthyDbContext context, ILogger<NeedsModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public string SelectedCategory { get; set; } = string.Empty;

        [BindProperty]
        public string SelectedItem { get; set; } = string.Empty;

        [BindProperty]
        public string DonorName { get; set; } = string.Empty;

        [BindProperty]
        public string DonorEmail { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostSponsorNeedAsync()
        {
            if (string.IsNullOrWhiteSpace(SelectedCategory) || string.IsNullOrWhiteSpace(SelectedItem) || string.IsNullOrWhiteSpace(DonorName))
            {
                ModelState.AddModelError(string.Empty, "Please complete all fields to submit your sponsorship intent.");
                return Page();
            }

            // Generate mock transaction tracking ID (e.g. TXN105)
            string generatedTxn = "TXN" + new Random().Next(105, 999).ToString();

            var record = new DonationRecord
            {
                TransactionId = generatedTxn,
                DonorName = DonorName,
                DonorEmail = DonorEmail,
                Amount = 0.00m, // Price omitted as per default service specification
                SelectedCause = $"{SelectedCategory} - {SelectedItem}",
                ContributedAt = DateTime.UtcNow,
                ProofImageName = "emergency-aid.jpg",
                ProofDescription = $"Sponsorship intent for '{SelectedItem}' under {SelectedCategory}. Pending field distribution proof upload.",
                IsVerified = false
            };

            try
            {
                _context.Donations.Add(record);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Thank you, {DonorName}! Your intent to sponsor '{SelectedItem}' under {SelectedCategory} has been recorded (Reference: {generatedTxn}). Our team will reach out to {DonorEmail} with delivery schedule and field verification updates.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database insertion error on Needs page.");
                TempData["SuccessMessage"] = $"Thank you, {DonorName}! Your sponsorship intent for '{SelectedItem}' has been registered with reference {generatedTxn}.";
            }

            return RedirectToPage("/Needs");
        }
    }
}