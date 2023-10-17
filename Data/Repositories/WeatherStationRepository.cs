using Data.Repositories;

public class WeatherStationRepository : GenericRepository<WeatherStationEntity>, IWeatherStationRepository
{
    public WeatherStationRepository(WeatherStationDbContext dbContext) : base(dbContext)
    {
    }

    public ICollection<WeatherStationWithLatestWeatherVariable> GetWeatherStationsWithLatestVariable()
    {
        // using var connection = SqlConnection

        return new List<WeatherStationWithLatestWeatherVariable>();
    }
}