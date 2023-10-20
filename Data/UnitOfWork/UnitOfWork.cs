using Data.Repositories;
using Microsoft.Extensions.Configuration;

public class UnitOfWork : IUnitOfWork
{
    private readonly WeatherStationDbContext _dbContext;
    private readonly IConfiguration _configuration;
    private IWeatherStationRepository _weatherStationRepository;


    public UnitOfWork(WeatherStationDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }


    public IWeatherStationRepository WeatherStationRepository
    {
        get { return _weatherStationRepository = _weatherStationRepository ?? new WeatherStationRepository(_dbContext, _configuration); }
    }

    public void Commit()
        => _dbContext.SaveChanges();


    public async Task CommitAsync()
        => await _dbContext.SaveChangesAsync();


    public void Rollback()
        => _dbContext.Dispose();


    public async Task RollbackAsync()
        => await _dbContext.DisposeAsync();
}