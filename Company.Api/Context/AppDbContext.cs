using Microsoft.EntityFrameworkCore;

namespace Company.Api.Context;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Entities.Company> Companies => Set<Entities.Company>();
}