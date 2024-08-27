using Back.Repositories;

namespace Back.Services
{
    public interface IService<T>
    {
        T Find(int id);
    }
}
