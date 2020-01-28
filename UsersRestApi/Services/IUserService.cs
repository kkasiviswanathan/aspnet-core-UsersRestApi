using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Models;

namespace UsersRestApi.Services
{
  public interface IUserService
  {
    bool CheckIfUserExists(int id);

    IEnumerable<User> GetAllUsers();

    IEnumerable<User> GetUsersByName(string name);

    User GetUserById(int id);

    User AddUser(User newUser);

    User UpdateUser(int id, User user);

    void RemoveUser(int id);
  }
}
