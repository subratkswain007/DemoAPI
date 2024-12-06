using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DemoDbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly ILogger<Repository<T>> _logger;
        public Repository(DemoDbContext context, ILogger<Repository<T>> logger)
        {
            _context = context;
            _dbSet = _context.Set<T>();  // Initialize DbSet for the given entity type
            _logger = logger;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            _logger.LogInformation("Entry Repository");
            return await _dbSet.ToListAsync();
        }
        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task<T> AddAsync(T entity)
        {
            // Add the entity to the DbSet
            var insertedRecord = await _dbSet.AddAsync(entity);

            // Save changes to persist the entity and generate the ID
            await _context.SaveChangesAsync();

            // Return the inserted entity (with the generated ID)
            return insertedRecord.Entity;
        }
        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null) _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
