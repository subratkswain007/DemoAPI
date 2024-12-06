using DAL.Repository;

namespace DAL.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        Task<int> CompleteAsync(); // Commit changes to DB
    }

}
