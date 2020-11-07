namespace AutoSync.EFC.Infrastructure
{
    public interface IUnitOfWork
    {
        int Commit();
        Repository<T> GetRepository<T>() where T : class;
    }
}
