using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ArthyFoundation.Data;
using ArthyFoundation.Models;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ArthyFoundation.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ArthyDbContext _context;
        private readonly IWebHostEnvironment _env;

        public IndexModel(ILogger<IndexModel> logger, ArthyDbContext context, IWebHostEnvironment env)
        {
            _logger = logger;
            _context = context;
            _env = env;
        }

        // Form properties for Contact database mapping
        [BindProperty]
        public string ContactName { get; set; } = string.Empty;

        [BindProperty]
        public string ContactPhone { get; set; } = string.Empty;

        [BindProperty]
        public string ContactEmail { get; set; } = string.Empty;

        [BindProperty]
        public string ContactMessage { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        // Authentication Admin handler
        public IActionResult OnPostAdminLogin([FromForm] string AdminUsername, [FromForm] string AdminPassword)
        {
            if (AdminUsername == "admin" && AdminPassword == "ArthyPass2026")
            {
                HttpContext.Session.SetString("IsAdmin", "true");
                TempData["SuccessMessage"] = "Authentication Successful! Admin Panel is now unlocked.";
            }
            else
            {
                TempData["SuccessMessage"] = "Error: Invalid Administrator credentials.";
            }
            return RedirectToPage("/Index");
        }

        public IActionResult OnPostAdminLogout()
        {
            HttpContext.Session.Remove("IsAdmin");
            TempData["SuccessMessage"] = "Admin session ended successfully.";
            return RedirectToPage("/Index");
        }

        // Dynamic Overwrite for Services Chart (uploaded services.png)
        public async Task<IActionResult> OnPostUploadServicesImageAsync(IFormFile ServicesFile)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return Challenge();
            }

            if (ServicesFile != null && ServicesFile.Length > 0)
            {
                // Local output folder validation
                var uploadPath = Path.Combine(_env.WebRootPath, "images");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var targetFilePath = Path.Combine(uploadPath, "services.png");

                // Atomically overwrite the existing file
                using (var stream = new FileStream(targetFilePath, FileMode.Create))
                {
                    await ServicesFile.CopyToAsync(stream);
                }

                _logger.LogInformation("Successfully uploaded and replaced services.png via Admin panel.");
                TempData["SuccessMessage"] = "Success: The Weekly Priorities Chart has been overwritten and updated on the site!";
            }
            else
            {
                TempData["SuccessMessage"] = "Error: Please upload a valid PNG/JPG image file.";
            }

            return RedirectToPage("/Index");
        }

        // Save incoming queries directly to SQLite Database using EF Core
        public async Task<IActionResult> OnPostSendMessageAsync()
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(ContactName) || string.IsNullOrEmpty(ContactMessage))
            {
                TempData["SuccessMessage"] = "Error: Please complete all required fields on the inquiry form.";
                return RedirectToPage("/Index");
            }

            var inquiry = new Inquiry
            {
                Name = ContactName,
                Phone = ContactPhone,
                Email = ContactEmail,
                Message = ContactMessage
            };

            _context.Inquiries.Add(inquiry);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Saved inquiry to DB from {ContactName}");
            TempData["SuccessMessage"] = $"Thank you, {ContactName}! Your support query was saved to the database. West Tambaram office desk will call you shortly.";

            return RedirectToPage("/Index");
        }
    }
}