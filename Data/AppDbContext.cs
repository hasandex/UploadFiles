using Microsoft.EntityFrameworkCore;

namespace UploadFiles.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
          
        //    modelBuilder.Entity<Category>().HasData(
        //        new Category { Id = 1, Name = "Mobile"},
        //        new Category { Id = 2, Name = "Laptop"},
        //        new Category { Id = 3, Name = "Car"},
        //        new Category { Id = 4, Name = "Motor"}
        //        );
        //}
    }
}
