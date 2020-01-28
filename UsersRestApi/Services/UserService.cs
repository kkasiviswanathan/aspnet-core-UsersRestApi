using System;
using System.Collections.Generic;
using System.Linq;
using UsersRestApi.Data;
using UsersRestApi.Models;

namespace UsersRestApi.Services
{
  /// <summary>
  /// Service class to abstract the CRUD operations of the User class
  /// </summary>
  public class UserService : IUserService
  {
    private readonly UsersContext _context;

    public UserService(UsersContext context)
    {
      _context = context;
    }

    public User AddUser(User newUser)
    {
      _context.Users.Add(newUser);
      _context.SaveChanges();
      return newUser;
    }

    public bool CheckIfUserExists(int id)
    {
      return _context.Users.Where(u => u.Id == id).Any();
    }

    public IEnumerable<User> GetAllUsers()
    {
      return _context.Users.ToList();
    }

    public User GetUserById(int id)
    {
      return _context.Users.Find(id);
    }

    public IEnumerable<User> GetUsersByName(string name)
    {
      return _context.Users.Where(u => u.Firstname.Contains(name, StringComparison.CurrentCultureIgnoreCase) || u.Lastname.Contains(name, StringComparison.CurrentCultureIgnoreCase)).ToList();
    }

    public void RemoveUser(int id)
    {
      User userToDelete = GetUserById(id);
      if(userToDelete != null)
      {
        _context.Remove(userToDelete);
        _context.SaveChanges();
      }      
    }

    public User UpdateUser(int id, User user)
    {
      User userToUpdate = GetUserById(id);
      if (userToUpdate != null)
      {
        _context.Entry(userToUpdate).CurrentValues.SetValues(user);
        _context.SaveChanges();
      }
      return userToUpdate;
    }
  }
}
