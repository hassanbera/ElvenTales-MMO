using Microsoft.AspNetCore.Mvc;
using ElvenTales.Data;
using BCrypt.Net;
using System.Linq;

namespace ElvenTales.Controllers
{
    public class LoginController : Controller
    {
        private readonly ElvenTalesDbContext _context;
        public LoginController(ElvenTalesDbContext context)
        {
            _context = context;
        }
        //Display the login page
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        // Handle login form submission
        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            //Find user by username
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            // Check if user exists and password is correct
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
				// cookie and session place
				TempData["Username"] = user.Username;  // Store the username in TempData for the profile page
				TempData["Message"] = "Login Successful!";
                return RedirectToAction("Index", "Profile"); // Redirect to the profile page
            }


            // If login fails, show error
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();

        }

    }
    }

