namespace ElvenTales.Models
{
    public class User
    {
        public int Id { get; set; } // Primary Key
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime DateCreated { get; set; }

        public List<Character> Characters { get; set; }= new List<Character>();
    }
}
