using Microsoft.EntityFrameworkCore;
using backend.Domain.Entities;

namespace backend.Infrastructure.Data
{
    public class myDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public myDbContext(DbContextOptions<myDbContext> options) : base(options) { }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    }
}
