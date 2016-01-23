namespace DraftLib.DAL
{
    public interface IUnitOfWork
    {
        void Dispose();
        void Save();
        void Dispose(bool disposing);
        IRepository<T> Repository<T>() where T : EntityBase;
    }
}