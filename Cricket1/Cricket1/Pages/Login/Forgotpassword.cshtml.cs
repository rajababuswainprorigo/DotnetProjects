using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Cricket1.Pages.Login
{
    public class ForgotpasswordModel : PageModel
    {
        public void OnGet()
        {
            // Handle GET requests if needed
        }

        // This is where you would handle the POST request to initiate the password reset
        public IActionResult OnPostReset([FromBody] ResetPasswordModel resetModel)
        {
            try
            {
                // Implement your password reset logic here
                // For simplicity, this example generates a random temporary password
                string temporaryPassword = GenerateRandomPassword();

                // In a real application, you would typically send an email to the user
                // with instructions on how to use the temporary password.

                // Example response:
                return new JsonResult(new { success = true, message = "Password reset initiated successfully. Check your email for further instructions." });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return new JsonResult(new { success = false, errorMessage = "Failed to initiate password reset." });
            }
        }

        private string GenerateRandomPassword()
        {
            // For simplicity, this example generates a random 8-character password
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] password = new char[8];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                byte[] data = new byte[8];
                crypto.GetBytes(data);
                foreach (byte b in data)
                {
                    password.Append(chars[b % (chars.Length)]);
                }
            }
            return new string(password);
        }
    }

    public class ResetPasswordModel
    {
        public string Email { get; set; }
    }
}
