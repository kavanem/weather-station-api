using Microsoft.Extensions.Configuration;

public abstract class BaseRepository
{
    protected readonly string ConnectionString;
    public BaseRepository(IConfiguration configuration)
    {
        ConnectionString = configuration.GetConnectionString("local");
    }
}