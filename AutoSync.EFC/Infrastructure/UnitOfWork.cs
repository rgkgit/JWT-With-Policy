namespace AutoSync.EFC.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AutoSyncDbContext _dbContext;
        public UnitOfWork(AutoSyncDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Repository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(_dbContext);
        }
        public int Commit()
        {
            return _dbContext.SaveChanges();
        }
    }
}
