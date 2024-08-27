using Back.Models;

namespace Back.Repositories
{
    public class MockUserRepository : IRepository<User>
    {
        private static List<User> _users = [new UserBuilder().Id(26).Username("josip").Build()];

        public User Find(int id) => 
            _users.Where(x => x.Id == id).First();
    }
}
