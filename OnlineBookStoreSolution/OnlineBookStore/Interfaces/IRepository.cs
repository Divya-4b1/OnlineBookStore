namespace OnlineBookStore.Interfaces
{
    public interface IRepository<K,T>
    {
        IList<T> GetAll();
        T GetById(K Key);
        T Add(T entity);
        T Update(T entity);
        T Delete(K Key);
    }
}
