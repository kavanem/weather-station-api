public class WeatherStationService
{
    private readonly IWeatherStationRepository _weatherStationRepository;

    public WeatherStationService(IWeatherStationRepository weatherStationRepository)
    {
        _weatherStationRepository = weatherStationRepository;
    }

    public async Task<ICollection<WeatherStationWithLatestWeatherVariable>> GetWeatherStationWithLatestWeatherVariablesAsync() 
    {
        var weatherStationsWithLatestWeatherVariables = await _weatherStationRepository.GetWeatherStationsWithLatestVariable();

        return weatherStationsWithLatestWeatherVariables;
    }
}