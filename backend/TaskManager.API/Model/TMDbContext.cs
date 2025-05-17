using Microsoft.EntityFrameworkCore;

namespace TaskManager.API.Model
{
    public class TMDbContext : DbContext
    {
        public TMDbContext(DbContextOptions<TMDbContext> options) :base(options) 
        {
        
        }
        public DbSet<Tasks> Tasks {  get; set; }
    }
}
