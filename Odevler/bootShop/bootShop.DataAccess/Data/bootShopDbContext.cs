using bootShop.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace bootShop.DataAccess
{
    public class bootShopDbContext : DbContext
    {
        public bootShopDbContext(DbContextOptions<bootShopDbContext> opt) : base(opt)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Telefonlar",
                },
                new Category
                {
                    Id = 2,
                    Name = "Bilgisayarlar",
                },
                new Category
                {
                    Id = 3,
                    Name = "Konsollar",
                }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product {
                    Id= 1,
                    Name="IPhone 13",
                    Price= 1200,
                    Discount=0.15,
                    CategoryId= 1,
                    CreatedDate=DateTime.Now,
                    ImageUrl= "https://productimages.hepsiburada.net/s/49/550/10986386784306.jpg/format:webp"
                },
                new Product
                {
                    Id = 2,
                    Name = "Samsung S22",
                    Price = 1200,
                    Discount = 0.15,
                    CategoryId = 1,
                    CreatedDate = DateTime.Now,
                    ImageUrl = "https://productimages.hepsiburada.net/s/49/550/10986386784306.jpg/format:webp"
                },
                new Product
                {
                    Id = 3,
                    Name = "Xiaomi",
                    Price = 1200,
                    Discount = 0.15,
                    CategoryId = 1,
                    CreatedDate = DateTime.Now,
                    ImageUrl = "https://productimages.hepsiburada.net/s/49/550/10986386784306.jpg/format:webp"
                },
                new Product
                {
                    Id = 4,
                    Name = "Apple Mac",
                    Price = 2000,
                    Discount = 0.15,
                    CategoryId = 2,
                    CreatedDate = DateTime.Now,
                    ImageUrl = "https://productimages.hepsiburada.net/s/49/550/10986386784306.jpg/format:webp"
                },
                new Product
                {
                    Id = 5,
                    Name = "Lenovo",
                    Price = 1200,
                    Discount = 0.15,
                    CategoryId = 2,
                    CreatedDate = DateTime.Now,
                    ImageUrl = "https://productimages.hepsiburada.net/s/49/550/10986386784306.jpg/format:webp"
                },
                new Product
                {
                    Id = 6,
                    Name = "XBox",
                    Price = 1200,
                    Discount = 0.15,
                    CategoryId = 3,
                    CreatedDate = DateTime.Now,
                    ImageUrl = "https://productimages.hepsiburada.net/s/49/550/10986386784306.jpg/format:webp"
                }
                );
        }
    }
}
