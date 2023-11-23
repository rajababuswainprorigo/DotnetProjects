using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cricket1.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public void OnGet()
        {
            // Handle GET requests
        }

        public IActionResult OnPost()
        {
            // Basic login logic (replace with your authentication logic)
            if (Username == "IAmUser" && Password == "MyPassword")
            {
                // Redirect to a success page or perform other actions
                return RedirectToPage("/Index");
            }
            else
            {
                // Display an error message or handle authentication failure
                ViewData["ErrorMessage"] = "Invalid username or passworddddd.";
                return Page();
            }
        }
    }
}