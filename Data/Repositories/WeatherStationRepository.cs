using Dapper;
using Data.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

public class WeatherStationRepository : GenericRepository<WeatherStationEntity>, IWeatherStationRepository
{
    public WeatherStationRepository(WeatherStationDbContext dbContext, IConfiguration configuration) : base(dbContext, configuration)
    {
    }

    public async Task<ICollection<WeatherStationWithLatestWeatherVariable>> GetWeatherStationsWithLatestVariable()
    {
        try
        {
            using var connection = new SqlConnection(ConnectionString);

            var query = @"
            DECLARE TABLE @WeatherStationIds (Id INT)

            SELECT INTO @WeatherStationIds (Id) 
            VALUES(WS.Id [WeatherStationId])
            FROM WeatherStations WS
            WHERE WS.IsActive = 1

            SELECT TOP 1 WSV.*
            FROM Variables WSV
            INNER JOIN @WeatherStationIds WSI
            ON WSV.WeatherStationId = WSI.Id
            ORDER BY WSI.Timestamp DESC";

            var weatherStationsWithLatestWeatherVariable = new List<WeatherStationWithLatestWeatherVariable>();
            using (var multi = await connection.QueryMultipleAsync(query))
            {
                var weatherStations = multi.Read<WeatherStationEntity>();
                var weatherVariables = multi.Read<WeatherVariableEntity>();

                foreach (var weatherStation in weatherStations)
                {
                    var matchingLatestWeatherVariable = weatherVariables.Where(x => x.WeatherStationId == weatherStation.Id)?.FirstOrDefault();

                    if (matchingLatestWeatherVariable == null)
                        continue;

                    weatherStationsWithLatestWeatherVariable.Add(new WeatherStationWithLatestWeatherVariable
                    {
                        Id = weatherStation.Id,
                        Name = weatherStation.Name,
                        Site = weatherStation.Site,
                        Portfolio = weatherStation.Portfolio,
                        LatestWeatherVariable = new WeatherVariable 
                        {
                            Id = matchingLatestWeatherVariable.Id,
                            LongName = matchingLatestWeatherVariable.LongName
                            Unit = matchingLatestWeatherVariable.Unit,
                            Value = matchingLatestWeatherVariable.Value,
                            Timestamp = matchingLatestWeatherVariable.Timestamp,
                        }
                    });
                }
            }

            return weatherStationsWithLatestWeatherVariable;
        }
        catch (System.Exception)
        {
            return null;
        }
    }
}