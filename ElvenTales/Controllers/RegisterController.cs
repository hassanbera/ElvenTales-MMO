using ElvenTales.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ElvenTales.Models;
using BCrypt.Net;

namespace ElvenTales.Controllers
{
    public class RegisterController : Controller
    {
        // The _context variable will allow us to interact with the database
        private readonly ElvenTalesDbContext _context;
        // Constructor for the RegisterController.
        // It takes DbContext as a parameter and assigns it to _context.
        // This way, every time we need to accsess the databaase, we use _context
        public RegisterController(ElvenTalesDbContext context)
        {
            _context = context;
        }
        // GET: This action is responsible for displaying the registration form.
        // When a user navigates to /Register(GET), this action will return the view.
        [HttpGet]
        public IActionResult Index()
        {
            // Returning the registration form view.
            return View();
        }
        //POST: This action is triggered when the user submits the registration form.
        // The "User user" parameter automatically binds form data to the User model.
        [HttpPost]
        public async Task<IActionResult> Index(User user)
        {
            //Check if the input data (form data) is valid.
            //Asp.Net automatically validates based on model annotations(e.g., requiered fields).
            if (ModelState.IsValid)
            {
                // Before saving, we hash the user's password for security reasons.
                // BCrypt is a common library used for hashing passwords.
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

                user.DateCreated = DateTime.Now;

                // Add the user to the Users table in the database.
                _context.Users.Add(user);
                // Save changes to the database.
                await _context.SaveChangesAsync();

                // Redirect the user to the Login page after successfully registering.
                // This means once the registers, they will be sent to the loging page.
                return RedirectToAction("Index", "Login");
            }
            //If the form data is invalid(e.g, missing fields or wrong format),
            // return the view with the form data and show validation errors.
            return View();
        }
    }
}
