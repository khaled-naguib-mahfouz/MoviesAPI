using Microsoft.EntityFrameworkCore;

namespace Movies.Models
{
	public class Context :DbContext
	{
        public Context(DbContextOptions<Context> options): base(options)

        {
            
        }
        public DbSet<Category> categories { get; set; }
        public DbSet<Movie> movies { get; set; }
    }
}
