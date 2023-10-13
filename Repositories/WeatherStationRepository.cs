public class WeatherStationRepository : IWeatherStationRepository 
{
    public ICollection<WeatherStationWithLatestWeatherVariable> GetWeatherStationsWithLatestVariable()
    {
        // using var connection = SqlConnection


        return new List<WeatherStationWithLatestWeatherVariable>();
    }
}