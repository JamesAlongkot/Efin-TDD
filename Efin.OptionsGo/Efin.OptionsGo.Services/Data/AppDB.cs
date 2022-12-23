using Efin.OptionsGo.Models;
using Microsoft.EntityFrameworkCore;

namespace Efin.OptionsGo.Services.Data
{
    public class AppDB : DbContext
    {
        public AppDB(DbContextOptions<AppDB> options): base(options) 
        { 

        }

        public DbSet<Portfolio> Portfolios { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
    }
}
