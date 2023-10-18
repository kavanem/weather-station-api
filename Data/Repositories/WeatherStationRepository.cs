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
            SELECT WS.Id [WeatherStationId],
            FROM WeatherStations WS
            INNER JOIN WeatherStationVariables WSV
            ON WSV.WeatherStationId = WS.Id";

            // using (var multi = await connection.QueryMultipleAsync(query))
            // {
            //     var weatherStations = multi.Read<WeatherStationEntity>();
            //     var weatherStationVariables = multi.Read<WeatherStationEntity>();
            // }

            var t = connection.Query<WeatherStationWithLatestWeatherVariable>(query).ToList();

            return t;
        }
        catch (System.Exception)
        {
            return null;
        }
    }
}