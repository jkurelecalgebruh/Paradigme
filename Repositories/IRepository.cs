namespace Back.Repositories
{
    public interface IRepository<T>
    {
        T Find(int id);
    }
}
