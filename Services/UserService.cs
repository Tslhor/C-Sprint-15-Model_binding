using System.Collections.Generic;
using System.Linq;
using ProductsValidation.Models;

namespace ProductsValidation.Services
{
    public class UserService : IUserService
    {
        private readonly object syncRoot = new object();
        private readonly Data data;

        public UserService(Data data)
        {
            this.data = data;
        }

        public IReadOnlyList<User> GetUsers()
        {
            lock (syncRoot)
            {
                return data.Users.Select(u => u.ToCopy()).ToList();
            }
        }

        public User GetUser(int id)
        {
            lock (syncRoot)
            {
                var found = data.Users.FirstOrDefault(u => u.Id == id);
                return found.ToCopy();
            }
        }

        public void AddUser(User user)
        {
            lock (syncRoot)
            {
                data.Users.Add(user.ToCopy());
            }
        }
    }
}
