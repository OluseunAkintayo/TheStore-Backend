using Microsoft.EntityFrameworkCore;
using TheStore.Models;
using TheStore.Models.CategoryModel;
using TheStore.Models.StockModel;
using TheStore.Models.Users;

namespace TheStore.Services;

public class RepoService : DbContext {
  public RepoService(DbContextOptions options) : base(options) {}

  public DbSet<Product> Products { get; set; }
  public DbSet<Brand> Brands { get; set; }
  public DbSet<Manufacturer> Manufacturers { get; set; }
  public DbSet<Category> Categories { get; set; }
  public DbSet<Stock> Stocks { get; set; }
  public DbSet<User> Users { get; set; }
  public DbSet<Picture> Pictures { get; set; }
}
