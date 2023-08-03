using Microsoft.EntityFrameworkCore;
using Web_API_ASSIGNMENT.Model;

namespace Web_API_ASSIGNMENT.Data
{
    public class WebApiDbContext : DbContext
    {
        public WebApiDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Barcode> Barcodes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
