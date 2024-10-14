namespace ElvenTales.Models
{
	public class Character
	{
		public int Id { get; set; }
		public string CharName { get; set; }
		public int Level {  get; set; }
		public string Class {  get; set; }
		public int Honour {  get; set; }
		public int Ranking {  get; set; }
		public int UserId {  get; set; } // Foreign key to the user table
		public User user { get; set; } // Navigation property to user
	}
}
