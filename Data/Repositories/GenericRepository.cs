using System.Linq.Expressions;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class GenericRepository<T> : BaseRepository, IGenericRepository<T> where T : class
    {
        protected readonly WeatherStationDbContext _dbContext;
        private readonly DbSet<T> _entitySet;


        public GenericRepository(WeatherStationDbContext dbContext, IConfiguration configuration) : base(configuration)
        {
            _dbContext = dbContext;
            _entitySet = _dbContext.Set<T>();
        }


        public void Add(T entity) 
            => _dbContext.Add(entity);


        public async Task AddAsync(T entity, CancellationToken cancellationToken = default) 
            => await _dbContext.AddAsync(entity, cancellationToken);


        public void AddRange(IEnumerable<T> entities) 
            => _dbContext.AddRange(entities);


        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default) 
            => await _dbContext.AddRangeAsync(entities, cancellationToken);


        public T Get(Expression<Func<T, bool>> expression) 
            => _entitySet.FirstOrDefault(expression);


        public IEnumerable<T> GetAll() 
            => _entitySet.AsEnumerable();


        public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression) 
            => _entitySet.Where(expression).AsEnumerable();


        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default) 
            => await _entitySet.ToListAsync(cancellationToken);


        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default) 
            => await _entitySet.Where(expression).ToListAsync(cancellationToken);


        public async Task<T> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default) 
            => await _entitySet.FirstOrDefaultAsync(expression, cancellationToken);


        public void Remove(T entity) 
            => _dbContext.Remove(entity);


        public void RemoveRange(IEnumerable<T> entities) 
            => _dbContext.RemoveRange(entities);


        public void Update(T entity) 
            => _dbContext.Update(entity);


        public void UpdateRange(IEnumerable<T> entities) 
            => _dbContext.UpdateRange(entities);
    }
