using DAL.Models;
using DAL.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DemoDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(DemoDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            // Resolve the repository from the service provider
            return _serviceProvider.GetRequiredService<IRepository<T>>();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }


}
