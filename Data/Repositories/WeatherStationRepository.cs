using Dapper;
using Data.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

public class WeatherStationRepository : GenericRepository<WeatherStationEntity>, IWeatherStationRepository
{
    public WeatherStationRepository(WeatherStationDbContext dbContext, IConfiguration configuration) : base(dbContext, configuration)
    {
    }

    public async Task<ICollection<WeatherStationWithLatestWeatherVariable>> GetWeatherStationsWithLatestVariable(string state = null)
    {
        try
        {
            using var connection = new SqlConnection(ConnectionString);

            var query = @"
            SELECT *
                WS.Name [WeatherStationName]
                WS.State [WeatherStationState],
                WS.Site [WeatherStationSite],
                WS.Portfolio [WeatherStationPortfolio],
                D.Timestamp [TimeStamp]
                V.LongName [VariableLongName],
                V.Unit [Unit]
            FROM Data D
            INNER JOIN WeatherStations WS
            ON WS.Id = D.WeatherStationId
            INNER JOIN Variables V
            ON V.Id = D.VariableId 
            GROUP BY D.WeatherStationId, D.Timestamp
            ORDER BY D.WeatherStationId, D.Timestamp DESC
            

            ";
            // TODO: Filter by state      

            var weatherStationsWithLatestWeatherVariable = new List<WeatherStationWithLatestWeatherVariable>();

            //TODO: do a dynamic read and assign to type
            using (var multi = await connection.QueryMultipleAsync(query))
            {
                var weatherStations = multi.Read<WeatherStationEntity>();
                var weatherVariables = multi.Read<VariableEntity>();

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
                            LongName = matchingLatestWeatherVariable.LongName,
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