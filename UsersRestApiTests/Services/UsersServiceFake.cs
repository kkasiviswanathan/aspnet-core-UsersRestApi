using System;
using System.Collections.Generic;
using System.Linq;
using UsersRestApi.Models;
using UsersRestApi.Services;

namespace UsersRestApiTests.Services
{
  public class UsersServiceFake: IUserService
  {
    private readonly List<User> _userList;

    public UsersServiceFake()
    {
      _userList = GetUserList();
    }

    public User AddUser(User newUser)
    {
      int newUserId = _userList.Select(u => u.Id).Max() + 1;
      newUser.Id = newUserId;
      _userList.Add(newUser);
      return newUser;
    }

    public bool CheckIfUserExists(int id)
    {
      return _userList.Where(u => u.Id == id).Any();
    }

    public IEnumerable<User> GetAllUsers()
    {
      return _userList;
    }

    public User GetUserById(int id)
    {
      return _userList.Where(u => u.Id == id).SingleOrDefault();
    }

    public IEnumerable<User> GetUsersByName(string name)
    {
      return _userList.Where(u => u.Firstname.Contains(name, StringComparison.CurrentCultureIgnoreCase) || u.Lastname.Contains(name, StringComparison.CurrentCultureIgnoreCase)).ToList();
    }

    public void RemoveUser(int id)
    {
      var userToDelete = GetUserById(id);
      if(userToDelete != null)
      {
        _userList.Remove(userToDelete);
      }
    }

    public User UpdateUser(int id, User user)
    {
      var userToUpdate = GetUserById(id);
      if (userToUpdate != null)
      {
        var idx = _userList.IndexOf(userToUpdate);
        _userList.Remove(userToUpdate);
        _userList.Insert(idx, user);
      }
      return GetUserById(id);
    }

    private List<User> GetUserList()
    {
      return new List<User>()
      {
        new User(){ Id = 1, Firstname = "John", Lastname = "Doe", Address = "1234 Millrock Dr", City = "Salt Lake City", State = "UT", Zipcode = "84121", Age = 25, Interests = "Skiing, Golf" },
        new User(){ Id = 2, Firstname = "Winter", Lastname = "Kabel", Address = "876 130th Rd", City = "Ponca City", State = "OK", Zipcode = "74601", Age = 33, Interests = "Photography, Sky diving" },
        new User(){ Id = 3, Firstname = "Lebron", Lastname = "Wu", Address = "1816 Hickory Ct Ft", City = "Augusta", State = "GA", Zipcode = "30905", Age = 55, Interests = "Pickle ball, Painting"},
        new User(){ Id = 4, Firstname = "Josh", Lastname = "Burford", Address = "111 Zingaro Dr", City = "Roxana", State = "IL", Zipcode = "62084", Age = 18, Interests = "Hiking, Video Games"},
        new User(){ Id = 5, Firstname = "Samara", Lastname = "Cutsforth", Address = "2665 Trenley Ct", City = "Simi Valley", State = "CA", Zipcode = "93063", Age = 63, Interests = "Tennis, Books" }
      };
    }
  }
}
