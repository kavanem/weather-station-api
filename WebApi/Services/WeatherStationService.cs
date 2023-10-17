public class WeatherStationService
{
    private readonly IWeatherStationRepository _weatherStationRepository;

    public WeatherStationService(IWeatherStationRepository weatherStationRepository)
    {
        _weatherStationRepository = weatherStationRepository;
    }

    public ICollection<WeatherStationWithLatestWeatherVariable> GetWeatherStationWithLatestWeatherVariables() 
    {
        var weatherStationsWithLatestWeatherVariables = _weatherStationRepository.GetWeatherStationsWithLatestVariable();

        return weatherStationsWithLatestWeatherVariables;
    }
}