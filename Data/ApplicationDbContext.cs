using Microsoft.EntityFrameworkCore;
using ST3.Models;

namespace ST3.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<NationalPark> NationalParks { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
