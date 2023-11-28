using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        if (Username == "IAmRajababu" && Password == "MyPassword")
        {
            // Redirect to a success page or perform other actions
            return RedirectToPage("/Index");
        }
        else
        {
            // Display an error message or handle authentication failure
            ViewData["ErrorMessage"] = "Invalid username or passworddddd.";//you can start with those invalid 
            return Page();
        }
    }
}
