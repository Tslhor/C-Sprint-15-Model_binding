using System.Collections.Generic;
using ProductsValidation.Models;

namespace ProductsValidation.Services
{
    public interface IUserService
    {
        IReadOnlyList<User> GetUsers();
        User GetUser(int id);
        void AddUser(User user);
    }
}
