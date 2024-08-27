using Back.Models;
using Back.Repositories;

namespace Back.Services
{
    public class UserService(IRepository<User> repository) : IService<User>
    {
        private readonly IRepository<User> _repository = repository;

        public User Find(int id) =>
            _repository.Find(id);
    }
}
