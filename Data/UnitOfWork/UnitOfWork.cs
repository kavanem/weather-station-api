using Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly WeatherStationDbContext _dbContext;
    private IWeatherStationRepository _bookRepository;


    public UnitOfWork(WeatherStationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public IWeatherStationRepository BookRepository
    {
        get { return _bookRepository = _bookRepository ?? new WeatherStationRepository(_dbContext); }
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