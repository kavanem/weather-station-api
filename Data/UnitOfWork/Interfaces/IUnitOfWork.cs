public interface IUnitOfWork
{
    IWeatherStationRepository WeatherStationRepository { get; }
    void Commit();
    void Rollback();
    Task CommitAsync();
    Task RollbackAsync();
}