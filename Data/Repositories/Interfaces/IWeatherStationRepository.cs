public interface IWeatherStationRepository 
{
    Task<ICollection<WeatherStationWithLatestWeatherVariable>> GetWeatherStationsWithLatestVariable();
}