namespace BeeFat.Interfaces;

public interface IBaseRepository<T>
{
    bool Create(T entity);

    T Get(Guid id);

    IEnumerable<T> Where(Func<T, bool> selector);

    bool Delete(T entity);

    void Save();
}