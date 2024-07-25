using Microsoft.EntityFrameworkCore;

namespace CutomerOrder.Models
{
	public class DBContext : DbContext
	{
        public DBContext(DbContextOptions<DBContext>options):base(options) 
        {
            
        }
        public DbSet<Customer>Customer{ get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Supplier> Suppliers { get; set; }
	}
}
