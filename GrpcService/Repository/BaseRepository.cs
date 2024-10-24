using GrpcService.Repository.Interface;
using NHibernate;
using NHibernate.Linq;

namespace GrpcService.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly ISessionFactory _sessionFactory;

        public BaseRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public async Task<List<T>> GetAllAsync()
        {
            using var session = _sessionFactory.OpenSession();
            return await session.Query<T>().ToListAsync(); // Requires System.Linq
        }

        public async Task<T> GetByIdAsync(string id)
        {
            using var session = _sessionFactory.OpenSession();
            return await session.GetAsync<T>(id);
        }

        public async Task<bool> AddAsync(T entity)
        {
            using var session = _sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            try
            {
                await session.SaveAsync(entity);
                await transaction.CommitAsync();
                return true;  // Return true if added successfully
            }
            catch
            {
                // Handle exceptions as needed (e.g., logging)
                return false;  // Return false if there was a problem
            }
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            using var session = _sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();

            try
            {
                session.Update(entity);
                await transaction.CommitAsync();
                return true;  // Return true if the update was successful
            }
            catch
            {
                await transaction.RollbackAsync();  // Rollback the transaction in case of error
                return false;  // Return false if there was an error
            }
        }


        public async Task<bool> DeleteAsync(string id)
        {
            using var session = _sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                session.Delete(entity);
                await transaction.CommitAsync();
                return true;  // Return true if deleted successfully
            }
            return false;  // Return false if entity was not found
        }
    }
}
