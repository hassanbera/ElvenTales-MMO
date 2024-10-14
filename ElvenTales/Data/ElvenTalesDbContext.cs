using Microsoft.EntityFrameworkCore;
using ElvenTales.Models;
using System.Collections.Generic;

namespace ElvenTales.Data


{
    public class ElvenTalesDbContext : DbContext
    {
        public ElvenTalesDbContext(DbContextOptions<ElvenTalesDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
		public DbSet<Character> Characters { get; set; }



	}


}
