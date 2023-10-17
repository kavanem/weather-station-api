using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class WeatherStationDbContext : DbContext
    {
        public WeatherStationDbContext(DbContextOptions<WeatherStationDbContext> options) : base(options) { }
        public DbSet<WeatherStationEntity> WeatherStations { get; set; }
    }
}

