using Back.Db;
using Back.Models;

namespace Back.Repositories
{
    public class UserRepository(MySqlContext db) : IRepository<User>
    {
        private readonly MySqlContext _db = db;

        public User Find(int id) =>
            _db.Users.Find(id);
    }
}
