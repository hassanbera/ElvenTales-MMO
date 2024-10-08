using Microsoft.AspNetCore.Mvc;
using ElvenTales.Data;
using System.Linq;
using ElvenTales.Models;
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
		public IActionResult Edit()
		{
			// Retrieve the logged-in user's username from TempData or Session(you could use either)
			var username= TempData["username"]?.ToString(); // This line is assuming TempData holds the username
			// If no username is found in TempData, redirect the user to the Login page
			if(string.IsNullOrEmpty(username)) {
				return RedirectToAction("Index", "Login"); // Redirect to login if not logged in
		}
			// Look up the user in the database using the retrieved username 
			var user=_context.Users.FirstOrDefault(u=>u.Username == username);
			//If the user is not found in the database, redirect to login again
			if(user == null) {
				return RedirectToAction("Index", "Login");
	}
			// If the user is found, return the Edit view, passing the user object to the view
			return View(user);

		}
		// Action to handle form submissions for editing the user's profile(POST request)
		[HttpPost]  // Indicates this method will handle POST form submissions
		public IActionResult Edit(User updatedUser)
		{
			// Find the user in the database by their ID (ID comes from the form in the view)
			var user = _context.Users.FirstOrDefault(u => u.Id == updatedUser.Id);

			// If the user is found in the database, update their username and email with the new data
			if (user != null)
			{
				user.Username = updatedUser.Username;  // Update the username
				user.Email = updatedUser.Email;        // Update the email

				// Save changes to the database
				_context.SaveChanges();  // Commits the updates to the database
			}

			// After saving changes, redirect the user back to the profile page
			return RedirectToAction("Index");
		}
	}
}