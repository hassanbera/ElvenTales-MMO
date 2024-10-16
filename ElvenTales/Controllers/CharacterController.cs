using ElvenTales.Data;
using ElvenTales.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace ElvenTales.Controllers
{
	[Route("Character")]
	public class CharacterController : Controller
	{
		private readonly ElvenTalesDbContext _context;

		public CharacterController(ElvenTalesDbContext context)
		{
			_context = context;
		}

		// Display a form to craete a new charcter
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		public IActionResult Create(Character character)
		{
			//Retrieve the logged-in user's username from the session
			var username = HttpContext.Session.GetString("Username");
			if (string.IsNullOrEmpty(username))
			{
				return RedirectToAction("Index", "Login"); // Redirect to login if the user is not logged in
			}
			// Find the user by username
			var user = _context.Users.FirstOrDefault(u => u.Username == username);
			if (user == null) return NotFound(); // Handle case if the user is not found
												 // Assign the user ID the character
			character.UserId = user.Id;
			if (ModelState.IsValid)
			{
				_context.Characters.Add(character);
				_context.SaveChanges();
				return RedirectToAction("Index", "Profile");
			}
			return View(character);
		}

		// Display a form to edit an existing character
		public IActionResult Edit(int id)
		{
			var character = _context.Characters.FirstOrDefault(c => c.Id == id);
			if (character == null) return NotFound();
			return View(character);
		}
	
	public IActionResult Index()
		{
			return View();
		}



		// Handle the form submission for editing character
		[HttpPost]
		public IActionResult Edit(Character updatedCharacter) {
			if (!ModelState.IsValid)
			{
				// Logging the validation errors
				foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
					
					Console.WriteLine(error.ErrorMessage);

				return View(updatedCharacter);
			}

			var character = _context.Characters.FirstOrDefault(c=> c.Id== updatedCharacter.Id);
			if (character != null)
			{
				character.CharName = updatedCharacter.CharName;
				character.Level = updatedCharacter.Level;
				character.Class = updatedCharacter.Class;
				character.Honour = updatedCharacter.Honour;
				character.Ranking = updatedCharacter.Ranking;
				_context.SaveChanges();
				return RedirectToAction("Index", "Profile");
			}
			return View(updatedCharacter);
	}

}
}

