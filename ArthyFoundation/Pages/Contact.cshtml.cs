using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ArthyFoundation.Data;
using ArthyFoundation.Models;
using System.ComponentModel.DataAnnotations;

namespace ArthyFoundation.Pages
{
    public class ContactModel : PageModel
    {
        private readonly ArthyDbContext _dbContext;
        private readonly ILogger<ContactModel> _logger;

        public ContactModel(ArthyDbContext dbContext, ILogger<ContactModel> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [BindProperty]
        [Required(ErrorMessage = "Please provide your Name.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string ContactName { get; set; } = string.Empty;

        [BindProperty]
        [Phone(ErrorMessage = "Invalid phone format.")]
        public string ContactPhone { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Please provide your Email Address.")]
        [EmailAddress(ErrorMessage = "Invalid email formatting.")]
        public string ContactEmail { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Please enter your message or question.")]
        [StringLength(1000, ErrorMessage = "Message must be under 1000 characters.")]
        public string ContactMessage { get; set; } = string.Empty;

        public void OnGet()
        {
            // Optional: Auto-fill forms if user is logged in
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _logger.LogInformation($"Contact submission received from: {ContactName} ({ContactEmail})");

            // Build our database schema object
            var inquiry = new Inquiry
            {
                Name = ContactName,
                Phone = ContactPhone,
                Email = ContactEmail,
                Message = ContactMessage,
                SubmittedAt = DateTime.UtcNow
            };

            // Programmatic Hook: Save message inquiry to local SQLite database
            try
            {
                _dbContext.Inquiries.Add(inquiry);
                await _dbContext.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Thank you, {ContactName}! Your message was successfully stored in the Arthy Foundation SQLite Database. Our Tambaram desk will verify and email you back at {ContactEmail} soon.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save contact inquiry to database.");
                TempData["SuccessMessage"] = $"Thank you, {ContactName}! Your message was processed, but database connectivity had an exception. Our team will read your log directly.";
            }

            return RedirectToPage("/Contact");
        }
    }
}