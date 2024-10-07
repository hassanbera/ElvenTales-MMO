using Microsoft.AspNetCore.Mvc;
using ElvenTales.Data;
using System.Linq;
namespace ElvenTales.Controllers
{
	public class ProfileController : Controller
	{
		private readonly ElvenTalesDbContext _context;
		public ProfileController(ElvenTalesDbContext context)
		{
			_context = context;
		}
		// GET: /Profile/
		public IActionResult Index()
		{
			//Assuming user data is stored in TempData or Session (adjust this part depending on how you manage user authentication)
			var username = TempData["Username"]?.ToString();
			if (string.IsNullOrWhiteSpace(username))
			{

				return RedirectToAction("Index", "Login"); // Redirect to login if user is not logged in
			}
			// Get user data from the database
			var user= _context.Users.FirstOrDefault(u=> u.Username == username);

			if (user == null) {
				return RedirectToAction("Index", "Login"); // If user is not found, redirect to login 
		}
			return View(user);  // Pass user object to the Profile View
		}
	}
}
